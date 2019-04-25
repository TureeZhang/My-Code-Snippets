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
