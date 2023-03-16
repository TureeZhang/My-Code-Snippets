重定向日志输出：

```cs
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    //日志记录
    optionsBuilder.LogTo(LogHelper.WriteSql,LogLevel.Information);
    base.OnConfiguring(optionsBuilder);
}
```

过滤IsDeleted 要想查询带上删除的 就加个IgnoreQueryFilters()

```cs
foreach (var entityType in modelBuilder.Model.GetEntityTypes()
  .Where(e => typeof(ISoftDleteBaseEntity).IsAssignableFrom(e.ClrType)))
{
    modelBuilder.Entity(entityType.ClrType).Property<Boolean>("IsDeleted");
    var parameter = Expression.Parameter(entityType.ClrType, "e");
    var body = Expression.Equal(
        Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant("IsDeleted")),
    Expression.Constant(false));
    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
}
```

统一添加前缀

```cs
foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
{
    entity.SetTableName("Automated_" + entity.GetTableName());
}
```
