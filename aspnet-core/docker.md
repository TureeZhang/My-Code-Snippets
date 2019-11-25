- 全部 docker 容器
``docker ps -a``

- 上一次运行的容器
``docker ps -l``

- 当前正在运行的容器
``docker ps``

- 移除容器
``docker rm``

- 启动镜像
``docker run --name code-server -P ``

- 删除所有容器 
`` docker rm `docker ps -a -q` ``

- 删除所有镜像 
`` docker rmi `docker images -q` ``

- 进入容器交互
``docker exec -it containerID /bin/bash``  containerID是镜像ID
