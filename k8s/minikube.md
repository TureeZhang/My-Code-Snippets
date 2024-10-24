# Minikube

此文档描述如何搭建 minikube 单机集群。

## 下载

执行以下命令下载并安装 minikube 到 Linux 。

```bash
curl -LO https://storage.googleapis.com/minikube/releases/latest/minikube-linux-arm64
sudo install minikube-linux-arm64 /usr/local/bin/minikube && rm minikube-linux-arm64
```

## 部署命令

```bash
export http_proxy=http://192.168.22.78:10811
export https_proxy=http://192.168.22.78:10811
export NO_PROXY=localhost,127.0.0.1,10.96.0.0/12,192.168.59.0/24,192.168.49.0/24,192.168.39.0/24,10.119.220.101

minikube start --force --insecure-registry "192.168.130.80:30080" --listen-address=0.0.0.0 --memory=max --cpus=max
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
    # certificate-authority: ./ca.crt  # ← 这里注释掉使用 CA 证书
    extensions:
    - extension:
        last-update: Wed, 11 Sep 2024 14:47:47 CST
        provider: minikube.sigs.k8s.io
        version: v1.34.0
      name: cluster_info
    server: https://10.119.220.101:32789
    insecure-skip-tls-verify: true  # ← 这里加上忽略 TLS ，允许不安全链接
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

## 宿主机 Nginx 暴露 minikube 端口

```bash
docker run --name minikube-nginx -d -v /root/nginx/config/nginx.conf:/etc/nginx/nginx.conf -v /root/nginx/templates:/etc/nginx/templates -v /root/nginx/conf.d/default.conf:/etc/nginx/conf.d/default.conf --network host 192.168.130.80:30080/docker-hub-storage/nginx:latest
```

默认 nginx 启动使用 80 端口，但通常都被占用。挂载 /etc/nginx/conf.d/default.conf 并修改默认端口可以解决此问题：

```
server {
    listen      10080;  # 此处默认是 80，修改为其他端口即可
    listen  [::]:10080;  # 此处默认是 80，修改为其他端口即可
    server_name  localhost;

    #access_log  /var/log/nginx/host.access.log  main;

    location / {
        root   /usr/share/nginx/html;
        index  index.html index.htm;
    }

    #error_page  404              /404.html;

    # redirect server error pages to the static page /50x.html
    #
    error_page   500 502 503 504  /50x.html;
    location = /50x.html {
        root   /usr/share/nginx/html;
    }

    # proxy the PHP scripts to Apache listening on 127.0.0.1:80
    #
    #location ~ \.php$ {
    #    proxy_pass   http://127.0.0.1;
    #}

    # pass the PHP scripts to FastCGI server listening on 127.0.0.1:9000
    #
    #location ~ \.php$ {
    #    root           html;
    #    fastcgi_pass   127.0.0.1:9000;
    #    fastcgi_index  index.php;
    #    fastcgi_param  SCRIPT_FILENAME  /scripts$fastcgi_script_name;
    #    include        fastcgi_params;
    #}

    # deny access to .htaccess files, if Apache's document root
    # concurs with nginx's one
    #
    #location ~ /\.ht {
    #    deny  all;
    #}
}
```

添加一行，让 /etc/nginx/nginx.conf 包含其他路由规则模板。

```
user  nginx;
worker_processes  auto;

error_log  /var/log/nginx/error.log notice;
pid        /var/run/nginx.pid;


events {
    worker_connections  1024;
}


http {
    include       /etc/nginx/mime.types;
    default_type  application/octet-stream;

    # 注意这行：加载 /etc/nginx/templates/ 目录下所有以 .conf 结尾的文件
    include /etc/nginx/templates/*.conf;

    log_format  main  '$remote_addr - $remote_user [$time_local] "$request" '
                      '$status $body_bytes_sent "$http_referer" '
                      '"$http_user_agent" "$http_x_forwarded_for"';

    access_log  /var/log/nginx/access.log  main;

    sendfile        on;
    #tcp_nopush     on;

    keepalive_timeout  65;

    #gzip  on;

    include /etc/nginx/conf.d/*.conf;
}
```

使用以下映射 yakd-dashboard 的模板：

```
[root@localhost nginx]# cat templates/minikube.conf 
server {
    listen 30080;

    location / {
        proxy_pass http://192.168.49.2:30080;  # 转发到后端服务
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

如果需要自己创建 Service，以 NodePort 形式暴露接口：

```yml
---
apiVersion: v1
kind: Service
metadata:
  name: wingtech-authcentral-httpapi-host
  namespace: wingtech-authcentral
spec:
  selector:
    app: wingtech-authcentral  # app 的名称
  ports:
    - protocol: TCP
      port: 80  # 映射的外部端口（这是这个 Service 的端口，一般给 Ingress 用）
      targetPort: 8080  # 容器内部的端口，就是容器内网站的真端口
      nodePort: 30107  # Node 节点的端口，在 minikube 中，就是 minikube 所在虚拟机 ip 暴露的端口（minikube ip 可以查看虚拟机 ip）
  type: NodePort  # 可以根据需要调整为 ClusterIP 或 LoadBalancer
```

## Ingress

暴露 yakd-dashboard 服务：

```yml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: yakd-ingress
  namespace: yakd-dashboard
spec:
  rules:
    - host: yakd.example.com # 如果没有域名，直接省略这行 host。记得不要忘记开头的 - 给下一行 http 补上
      http:
        paths:
          - path: /
            pathType: Prefix
            backend:
              service:
                name: yakd-dashboard
                port:
                  number: 80
```

如果使用路由区分各个网站：

```yml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: yakd-ingress
  namespace: yakd-dashboard
  annotations:
    nginx.ingress.kubernetes.io/rewrite-target: /$2  # 重写路径，去掉 /yakd-dashboard 前缀
    nginx.ingress.kubernetes.io/add-base-url: "true"  # 添加此注解以在响应中保留路径前缀
    nginx.ingress.kubernetes.io/base-url: "/yakd-dashboard"  # 指定基础路径前缀
spec:
  rules:
    - host: yakd.example.com # 如果没有域名，直接省略这行 host。记得不要忘记开头的 - 给下一行 http 补上
      http:
        paths:
          - path: /yakd-dashboard(/|$)(.*)  # 匹配 /yakd-dashboard 后的路径
            pathType: Prefix
            backend:
              service:
                name: yakd-dashboard
                port:
                  number: 80
```
