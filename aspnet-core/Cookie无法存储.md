> https://blog.csdn.net/weixin_34252686/article/details/86362345

**GDPR**

```cs
services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies 
    // is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
```
