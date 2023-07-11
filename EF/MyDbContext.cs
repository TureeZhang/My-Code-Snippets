using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimedJob.WinService.Service.Config;

namespace TimedJob.WinService.Data
{
    public class TimedJobDbContext : DbContext
    {
        public DbSet<ConfigEntity> Configs { get; set; }

        public TimedJobDbContext(string connStr)
            : base(connStr)
        {
            Database.SetInitializer<TimedJobDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
