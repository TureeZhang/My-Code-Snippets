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


## 查看 docker 占用的磁盘空间

```sh
[root@node1 ~]# docker system df
TYPE            TOTAL     ACTIVE    SIZE      RECLAIMABLE
Images          39        39        11.12GB   1.415GB (12%)
Containers      54        34        2.257GB   1.224GB (54%)
Local Volumes   21        21        5.093GB   0B (0%)
Build Cache     63        0         2.621MB   2.621MB
```

```sh
[root@node1 ~]# docker ps -a --size
CONTAINER ID   IMAGE                                               COMMAND                  CREATED          STATUS                       PORTS                                                                                                                          NAMES                                                                                               SIZE
f8e86746aeb0   registry.k8s.io/pause:3.6                           "/pause"                 1 second ago     Up Less than a second                                                                                                                                       k8s_POD_coredns-c676cc86f-c8w6p_kube-system_0c0aceed-26a7-410c-91cb-b649e7bb5386_17384314           0B (virtual 683kB)
```
