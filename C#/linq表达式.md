# Linq 表达式

查询子表时包含 where 条件：

```c#
WikiPassage wikiPassage = await CSLDbContext.WikiPassages
                .Where(wp => string.Equals(routePath, wp.RoutePath, StringComparison.OrdinalIgnoreCase))
                .Include(p => p.Comments
                    .Select(c=>c.AuditStatus== AuditStatusEnum.OK))
                .AsNoTracking().FirstOrDefaultAsync();
```

重点在 .Include(s=>s.Comments.Select(item=>item.Status==AuditStatusEnum.OK));
