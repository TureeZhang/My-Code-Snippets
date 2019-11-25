https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-3.0

```dockerfile
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY aspnetapp/*.csproj ./aspnetapp/
RUN dotnet restore

# copy everything else and build app
COPY aspnetapp/. ./aspnetapp/
WORKDIR /app/aspnetapp
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/aspnetapp/out ./
ENTRYPOINT ["dotnet", "aspnetapp.dll"]
```

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
