有时 ef 会出现 DataReader State is connection ,cannot change 的问题。

``原因是连接字符串为支持 MultipleActiveResultSets=true; 同时各 Service 请求数据库时使用了同一个 DbContext 实例。而应当为每一个不同的请求注入按 scope 注册的 Service 对象，并每次创建 Service 都 new 一个新的 DbContext 实例。``

```cs
//连接字符串
MultipleActiveResultSets=true;
//依赖注入 scope 注册
service.RegisterScoped<UserInfoService>();
```
