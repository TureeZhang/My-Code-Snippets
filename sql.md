# SQL

```sql
SELECT *
FROM L_SolderPaste_DY;

if(@@ROWCOUNT>=1)
BEGIN
    --SET noexec ON;
    PRINT('')
END

SELECT 1;

SET noexec OFF;
SELECT 'end';
```


```sql
/*拷贝一张表的几列数据到另一张表，并插入创建时间*/
insert into wikipassageviewerscounts(wikipassageid,viewerscount,CreateDate,LastModifyDate) select Id as wikipassageid,TotalViewsCount as viewscount,curtime() as CreateDate ,curtime() as LastModifyDate from wikipassages;
select * from wikipassageviewerscounts;
```
