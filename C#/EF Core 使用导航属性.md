# EF Core 使用导航属性

不是外键。是主表和子表互查的导航属性，例如文档查询评论列表，评论看自己在哪个文档。

```c#

    public class WikiPassage : BaseDataModel<WikiPassage, WikiPassageDto>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public List<WikiPassageComment> Comments { get; set; }
    }
    
    public class WikiPassageComment : BaseDataModel<WikiPassageComment,WikiPassageCommentDto>
    {

        [Required]
        public string Content { get; set; }

        public WikiPassage WikiPassage { get; set; }
    }

```

评论表的数据库列名为

|Id | WikiPassageId |

即可符合约定。

**查询时**,在 linq 语句中使用 .InClude(w=>w.Comments) 即可，例如：

```c#

WikiPassage wikiPassage = await CSLDbContext.WikiPassages
    .Where(wp => string.Equals(routePath, wp.RoutePath, StringComparison.OrdinalIgnoreCase))
    .Include(p => p.Comments
        .Select(c=>c.AuditStatus== AuditStatusEnum.OK))
    .AsNoTracking().FirstOrDefaultAsync();
```
