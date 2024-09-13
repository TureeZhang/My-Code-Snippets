# Minikube

此文档描述如何搭建 minikube 单机集群。

## 部署命令

```bash
export http_proxy=http://192.168.22.78:10811
export https_proxy=http://192.168.22.78:10811
export NO_PROXY=localhost,127.0.0.1,10.96.0.0/12,192.168.59.0/24,192.168.49.0/24,192.168.39.0/24,10.119.220.101

minikube start --force --insecure-registry "192.168.130.80:30080" --image-mirror-country='cn' --listen-address=0.0.0.0
```

- 开头 export 是设置网络代理，一定记得忽略 minikube 相关的代理
- --insecure-registry 是设置私有 Docker 镜像仓库
- --image-mirror-country='cn' 会让 minikube 在中国大陆运行时尝试从阿里获取镜像，避免墙的问题
- --listen-address=0.0.0.0 允许 minikube 被从远程连接

## Lens 远程连接

使用 Lens 远程连接时记得允许不安全链接的配置。

```yml
apiVersion: v1
clusters:
- cluster:
    # certificate-authority: ./ca.crt
    extensions:
    - extension:
        last-update: Wed, 11 Sep 2024 14:47:47 CST
        provider: minikube.sigs.k8s.io
        version: v1.34.0
      name: cluster_info
    server: https://10.119.220.101:32789
    insecure-skip-tls-verify: true
  name: minikube
contexts:
- context:
    cluster: minikube
    extensions:
    - extension:
        last-update: Wed, 11 Sep 2024 14:47:47 CST
        provider: minikube.sigs.k8s.io
        version: v1.34.0
      name: context_info
    namespace: default
    user: minikube
  name: minikube
current-context: minikube
kind: Config
preferences: {}
users:
- name: minikube
  user:
    client-certificate: ./client.crt
    client-key: ./client.key
```

远程服务器的端口 32789 要看 Docker 里映射 8443 的端口。
