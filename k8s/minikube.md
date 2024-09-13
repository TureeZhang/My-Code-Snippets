# Minikube

此文档描述如何搭建 minikube 单机集群。

## 部署命令

```bash
minikube start --force --insecure-registry "192.168.130.80:30080" --image-mirror-country='cn'
```

- --insecure-registry 是设置私有 Docker 镜像仓库
- --image-mirror-country='cn' 会让 minikube 在中国大陆运行时尝试从阿里获取镜像，避免墙的问题
- 额外的，需要 export http_proxy=http://192.168.22.78:10811 、export https_proxy=http://192.168.22.78:10811 设置网络代理
