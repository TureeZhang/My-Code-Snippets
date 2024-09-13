# Minikube

此文档描述如何搭建 minikube 单机集群。

## 部署命令

```bash
export http_proxy=http://192.168.22.78:10811
export https_proxy=http://192.168.22.78:10811
export NO_PROXY=localhost,127.0.0.1,10.96.0.0/12,192.168.59.0/24,192.168.49.0/24,192.168.39.0/24,10.119.220.101

minikube start --force --insecure-registry "192.168.130.80:30080" --listen-address=0.0.0.0
```

- 开头 export 是设置网络代理，一定记得忽略 minikube 相关的代理
- --insecure-registry 是设置私有 Docker 镜像仓库
- --listen-address=0.0.0.0 允许 minikube 被从远程连接
- 有时，可以指定 --image-mirror-country='cn' 会让 minikube 在中国大陆运行时尝试从阿里获取镜像，避免墙的问题。但现在 Docker 被墙了，源也没了所以这个选项没有意义了。

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

- 远程服务器的端口 32789 要看 Docker 里映射 8443 的端口。
- 需要的几个证书 ca.crt client.crt client.key 可以从 cat ~/.kube/config 里面指定的路径拿。

## minikube 内的 Docker 代理

```bash
minikube ssh

sudo su -
mkdir /etc/systemd/system/docker.service.d/
vi /etc/systemd/system/docker.service.d/http-proxy.conf
```

```json
[Service]
Environment="HTTP_PROXY=http://192.168.22.78:10811"
Environment="HTTPS_PROXY=http://192.168.22.78:10811"
Environment="NO_PROXY=192.168.130.80:30080,10.10.10.10,*.example.com"
```

```bash
systemctl daemon-reload
systemctl restart docker
```

注意，minikube 内的 Docker 环境是与宿主机上的环境隔离的，因此为宿主机设置了代理时仍旧无法让 minikube 内使用代理。反之，内部设置了代理，外部 docker pull 不会受到影响。

同时，重启 minikube 内的 Docker 不会导致外部 Docker 重启。

## coredns 会部署失败

```yml
# 编辑 coredns 的 Deployment 文件，在 spec.template.containers.securityContext 下增加 runAsUser: 0

securityContext:
    runAsUser: 0
    capabilities:
      add:
        - NET_BIND_SERVICE
      drop:
        - ALL
    readOnlyRootFilesystem: true
    allowPrivilegeEscalation: false
```

## Minikube addons

这将启用一些有用的插件。

```bash
# Dashboard
minikube addons enable yakd

# Ingress
minikube addons enable ingress

# 性能分析（含安装helm）
minikube addons enable metrics-server
curl https://raw.githubusercontent.com/helm/helm/master/scripts/get-helm-3 | bash
helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
helm repo update
helm install prometheus prometheus-community/kube-prometheus-stack --namespace monitoring --create-namespace
```
