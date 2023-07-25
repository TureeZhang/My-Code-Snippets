# 清理Docker容器日志

```sh
logs=$(find /var/lib/docker/containers/ -name *-json.log)

for log in $logs; 
do
    echo "clean logs:" 
    echo $log
    cat /dev/null>$log
    rm -rf $log
done
```

将以上脚本创建一个 temp.sh ，然后粘贴进去。

```sh
chmod +x temp.sh
```

为此脚本增加可执行权限。

```sh
./temp.sh
```

最后执行上述命令先让脚本里的 cat /dev/null>$log 解除链接占用，然后 rm -rf $log 移除掉日志文件。

**see also: [知乎](https://zhuanlan.zhihu.com/p/522796949)**
