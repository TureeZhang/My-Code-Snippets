存放在 /etc/systemd/system 下。

```yaml
[Unit]
Description=Cities:Skylines China Wikipedia .NET Web API App running on Ubuntu

[Service]
WorkingDirectory=/root/cslcn
ExecStart=/usr/bin/dotnet /root/cslcn/HanJie.CSLCN.WebApp.dll
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
Restart=always
KillSignal=SIGINT
# 每运行 12 小时重启 1 次。仅 systemd 229 以上支持，systemctl --version 查看版本
RuntimeMaxSec=43200
SyslogIdentifier=dotnet-cslcn-webapi-app
User=root
Environment=ASPNETCORE_ENVIRONMENT=Release
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```
