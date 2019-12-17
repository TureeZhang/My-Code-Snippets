```cmd
apt-get install mongodb
pgrep mongo -l
mongo
show dbs // 显示所有的数据库
use admin // 切换到admin
db.createUser({user:'root',pwd:'root',roles:['userAdminAnyDatabase']})
db.auth('root','root')
```
