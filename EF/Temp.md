重定向日志输出：

```cs
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    //日志记录
    optionsBuilder.LogTo(LogHelper.WriteSql,LogLevel.Information);
    base.OnConfiguring(optionsBuilder);
}
```
