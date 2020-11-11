**删除文件**

``
rm
``

- -r 递归删除，用于删除非空文件夹
- -f 无需提示，用于强制删除

**解压文件**

``
unzip [package-name]
``

**切换工作目录**

``
cd
``

- www 转到www目录； 
- .. 转到上一级目录； 
- \- 返回到上次的目录，类似windows返回； 
- / 回到根目录。

**后台运行**

- & 最经常被用到 这个用在一个命令的最后，可以把这个命令放到后台执行
- ctrl + z 可以将一个正在前台执行的命令放到后台，并且暂停
- jobs 查看当前有多少在后台运行的命令
- fg 将后台中的命令调至前台继续运行
- bg 将一个在后台暂停的命令，变成继续执行
- [nohup](https://www.cnblogs.com/jinxiao-pu/p/9131057.html) 命令行 > log.out 2>&1 & 忽视挂断信号，账号注销后仍继续运行命令

**其他**

- su 一次性在当前会话中获取 root 权限，不再 sudo 即可执行高风险命令。
- copy -r cslcn/. cslcn.bak 拷贝 cslcn 下的所有文件到 cslcn.bak ，同时不包含 cslcn 文件夹本夹
- find /(查找范围) -name callback.txt -print
- top 查看计算机性能统计
- sed -i 's@Nuget.Project.Template.1.0.0.nupkg@'"Nuget.Project.Template.$CI_COMMIT_TAG.nupkg"'@g' $CI_PROJECT_DIR/my-scripts/Dockerfile 替换文本字符串并在其中使用环境变量
- mount -t nfs 10.0.1.24:/k8sprd <本地目录> 挂载 NFS 
