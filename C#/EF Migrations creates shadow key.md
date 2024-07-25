https://github.com/dotnet/efcore/issues/27032#issuecomment-1004773740

Your relationship configuration is this:

```cs
builder.HasOne ( c => c.Blog ).WithOne ().HasForeignKey<Post> ( c => c.Blog_RID );
```

This creates a 1:1 relationship with a navigation from Post to Blog, but no navigation from Blog to Post (because WithOne() is empty). This means that the Blog.Posts navigation is being explicitly excluded from this relationship, and hence is configured by convention as a navigation for a separate relationship, which in turn means that it needs another foreign key.

You probably want this instead:

```cs
builder.HasOne(c => c.Blog).WithMany(c => c.Posts).HasForeignKey(c => c.Blog_RID);
```

This creates a 1:* relationship with navigations in both directions. An additional foreign key is not needed, since both navigations are now part of the same relationship.
