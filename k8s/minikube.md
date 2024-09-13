# Minikube

此文档描述如何搭建 minikube 单机集群。

## 部署命令

```bash
export http_proxy=http://192.168.22.78:10811
export https_proxy=http://192.168.22.78:10811
export NO_PROXY=localhost,127.0.0.1,10.96.0.0/12,192.168.59.0/24,192.168.49.0/24,192.168.39.0/24,10.119.220.101

minikube start --force --insecure-registry "192.168.130.80:30080" --image-mirror-country='cn'
```

- 开头 export 是设置网络代理，一定记得忽略 minikube 相关的代理
- --insecure-registry 是设置私有 Docker 镜像仓库
- --image-mirror-country='cn' 会让 minikube 在中国大陆运行时尝试从阿里获取镜像，避免墙的问题
