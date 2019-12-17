- 配置文件

为 ``/etc/mysql/mysql.conf.d/mysqld.cnf``

或 ``/etc/my.cnf``

视版本而定

- 忽视大小写

``lower_case_table_names=1``

- 新增用户

  - ``update user set host=’%’ where user=’root’;``
  - ``SHOW VARIABLES LIKE 'validate_password%';``
  - ``set global validate_password.policy=LOW;``
  - ``CREATE USER username IDENTIFIED BY 'password';``
  - ``grant all on *.* to 'username'@'%';``
  - ``FLUSH PRIVILEGES;``
