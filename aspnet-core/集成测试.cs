using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Primitives;
using Moq;
using SmoStoreWeb.Controllers;
using SmoStoreWeb.MyAttribute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class TestAuthorizeAttribute : IClassFixture<WebApplicationFactory<SmoStoreWeb.Startup>>
    {
        private readonly WebApplicationFactory<SmoStoreWeb.Startup> _factory;

        public TestAuthorizeAttribute(WebApplicationFactory<SmoStoreWeb.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async System.Threading.Tasks.Task TestUploadAPIAuthorizeAttribute_ShowReturnOkAsync()
        {
            HttpClient client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("smobilerapp", "md5strgoeshere");
            ByteArrayContent byteArrayContent = new ByteArrayContent(new byte[0]);
            HttpResponseMessage response = await client.PostAsync("https://localhost:44351/upload/ImageAsync", byteArrayContent);

            Assert.IsType<UnauthorizedResult>(response);
        }
    }
}
//测试应用程序必备组件
//测试项目必须：
//引用以下包：
//Microsoft.AspNetCore.App
//Microsoft.AspNetCore.Mvc.Testing
//在项目文件中指定 Web SDK (<Project Sdk="Microsoft.NET.Sdk.Web">)。 Web SDK 时是必需的引用Microsoft.AspNetCore.App 元包。
//这些系统必备组件中所示示例应用。 检查tests/RazorPagesProject.Tests/RazorPagesProject.Tests.csproj文件。 示例应用使用xUnit测试框架和AngleSharp分析器库，因此示例应用还引用：
//xunit
//xunit.runner.visualstudio
//AngleSharp
