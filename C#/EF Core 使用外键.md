# EF Core 使用导航属性

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

**查询时
