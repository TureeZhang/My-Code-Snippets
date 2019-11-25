**配置 Nginx**

若要将 Nginx 配置为反向代理以将请求转接到 ASP.NET Core 应用，请修改 /etc/nginx/sites-available/default。 在文本编辑器中打开它，并将内容替换为以下内容：


```
server {
    listen        80;
    server_name   example.com *.example.com;
    location / {
        proxy_pass         http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
}
```
