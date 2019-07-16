```code
git config --global http.https://github.com.proxy socks5://127.0.0.1:1080
```

如果要恢复/移除上面设置的git代理，使用如下命令

```code
git config --global --unset http.proxy

git config --global --unset http.https://github.com.proxy
```

> 转载
- 作者：Neo陈
- 链接：https://www.jianshu.com/p/22cd1653f666
- 来源：简书
- 简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。
