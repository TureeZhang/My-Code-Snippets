using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TimedJob.WinService.Data
{
    public class TimedJobRepository<T> where T : class, new()
    {

        /// <summary>
        /// 仓库使用的 EF 仓储基类。
        /// </summary>
        /// <remarks>此类中不使用 Async 异步方法实现，因为曾经做过，但在此版本框架下总是有异步查询后没有响应也不报错的奇怪问题发生。</remarks>
        /// <typeparam name="T"></typeparam>

        public TimedJobDbContext DbContext { get; set; }
        public TimedJobRepository(string connStr)
        {
            DbContext = DbContextProvider(connStr);

            if (DbContext != null)
                SetWithNoLock();
        }

        /// <summary>
        /// 控制 EF DbContext 按照每个请求一个的模式提供
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private TimedJobDbContext DbContextProvider(string connStr)
        {
            if (string.IsNullOrEmpty(connStr))
            {
                return null;
            }

            TimedJobDbContext context = CallContext.GetData($"context-{connStr}") as TimedJobDbContext;
            if (context is null)
            {
                context = new TimedJobDbContext(connStr);
                CallContext.SetData($"context-{connStr}", context);
            }

            return context;
        }

        public IQueryable<T> Load()
        {
            return DbContext.Set<T>().AsQueryable<T>();
        }

        public void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
            SetWithLock();
            DbContext.SaveChanges();
            SetWithNoLock();
        }
        public void Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            SetWithLock();
            DbContext.SaveChanges();
            SetWithNoLock();
        }
        public void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            SetWithLock();
            DbContext.SaveChanges();
            SetWithNoLock();
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            foreach (var entry in DbContext.ChangeTracker.Entries())
            {
                entry.Reload(); //SQL直接执行后，EF跟踪的上下文实体不会发生变化，Reload方法将触发从数据库重新获取，而不是从上下文追踪的未变化实体中获取
            }

            IQueryable<T> entities = DbContext.Set<T>().Where(predicate);
            return entities;
        }
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            T entity = DbContext.Set<T>().FirstOrDefault(predicate);

            if (entity != null)
                DbContext.Entry(entity).Reload(); //SQL直接执行后，EF跟踪的上下文实体不会发生变化，Reload方法将触发从数据库重新获取，而不是从上下文追踪的未变化实体中获取

            return entity;
        }
        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                       Func<T, bool> whereLambda, Func<T, S> orderByLambda, bool isAsc)
        {
            total = DbContext.Set<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = DbContext.Set<T>().Where(whereLambda)
                    .OrderBy<T, S>(orderByLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = DbContext.Set<T>().Where(whereLambda)
                    .OrderByDescending<T, S>(orderByLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
                return temp;
            }
        }

        /// <summary>
        ///  设置后依然可以SaveChanges(),推荐需要SaveChanges()时先调用下WithLock()
        /// </summary>
        public void SetWithNoLock()
        {
            this.DbContext.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
        }

        /// <summary>
        ///  这个是数据库默认设置。
        /// </summary>
        public void SetWithLock()
        {
            this.DbContext.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ COMMITTED;");
        }



    }

}
