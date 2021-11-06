/*拷贝一张表的几列数据到另一张表，并插入创建时间*/
insert into wikipassageviewerscounts(wikipassageid,viewerscount,CreateDate,LastModifyDate) select Id as wikipassageid,TotalViewsCount as viewscount,curtime() as CreateDate ,curtime() as LastModifyDate from wikipassages;
select * from wikipassageviewerscounts;
