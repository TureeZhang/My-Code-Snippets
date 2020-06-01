**Startup.cs:**

```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Beflam.App.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //增加跨域
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            
            services.AddSpaStaticFiles((config) =>
            {
                config.RootPath = System.IO.Path.GetFullPath("../../frontend/ClientApp/dist/ClientApp");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            //调用跨域
            app.UseCors(MyAllowSpecificOrigins);
            //app.UseAuthorization();

            app.Map("/app1", app1 =>
            {
                app1.UseSpa(spa =>
                {
                    spa.Options.DefaultPage = "/index.html";
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}

```

**index.html:**

```html
<!doctype html>
<html lang="zh-cn">
<head>
  <meta charset="utf-8">
  <title>Beflamapp</title>
  <base href="./app1">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="icon" type="image/x-icon" href="favicon.ico">
  <link rel="stylesheet" href="../styles.b9cadf26bdcf4e67b585.css">
</head>
<body>
  <app-root></app-root>
  <script src="../runtime.3f34a6cdf7a7ee253e4b.js" defer></script>
  <script src="../polyfills.44bc37f38af03addc173.js" defer></script>
  <script src="../main.7a99ee9327edfce07aae.js" defer></script>
</body>
</html>
```

**注意：**

应用内 ``<a routerLink="/wiki-passage/1">`` 类型的路由，不能由 ``<a routerLink="./wiki-passage/1">`` 相对路径构成，必须不以点开头。因为此路径是由 Angular 自行解析的，如果注册路由时候使用的就是 "wiki-passage"，所以解析时候也是按 "/wiki-passage" 来的。





