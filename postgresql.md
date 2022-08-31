# PostgreSql

## 大小写 <https://www.cnblogs.com/LiuwayLi/p/11445019.html>

1. PostgreSQL的数据库内核对大小写敏感。数据库名，数据表名，列名区分大小写。

2. 在PostgreSQL中，执行SQL语句时，会把所有表示关键字，库名，表名，列名的字符串转换成小写。所以又说PostgreSQL不区分大小写的。

3. 在书写SQL时，为了便于理解，默认：关键字大写，表名首字母大写，列名全部小写。

示例：

CREATE DATABASE Contact;    /*存在一个名字叫contact的数据库，不存在Contact数据库*/

CREATE DATABASE contact;   /*报错，数据库contact已经存在*/

CREATE DATABASE "Contact"  /*ok,加双引号告诉PostgreSQL，不要转换成小写*/

总结：

### 一.对象名：如库名，表名，字段名

- 数据库内核是区分大小写的。
- 只是为了方便使用，数据库在分析SQL脚本时，对不加双引号的所有对象名转化为小写字母。
- 除非你在对象名加上双引号。
- 
所以

1. 从建表到应用，要么都加双引号，要么都不要加。
2. 如果以上这点做不到，所有的对象名给我写小写字母。
3. 字符串要用单引号括起来，双引号用来明确告诉数据库不要转换成小写，本次要区分大小写。


### 二.数据

- 区分大小写
- 假如LIKE '%a%' ,别指望A会出来
