Redis 获取字符串有双引号

使用
```cs
_db.Get<string>(string key)
```
 取值，而非
 
 ```
 _db.StringGet(string key) 方法。
 ```
