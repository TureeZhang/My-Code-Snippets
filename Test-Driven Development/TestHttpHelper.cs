using XApiBusiness.Helpers;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace XApiTest.Helpers
{
    public class TestHttpHelper
    {
        [Fact]
        public void Post_Form_ShouldReturnHeaderBodys()
        {
            const string url = "http://address.com";
            const string username = "username";
            const string password = "password";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> formValues = new Dictionary<string, string>() {
                { "username" , username },
                { "password" , password }
            };

            Mock<HttpClientHandler> mock = new Mock<HttpClientHandler>();
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Headers.Add("Authorization", "token123456");
            mock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(response);

            HttpHelper httpHelper = new HttpHelper(mock.Object);
            HttpResponse result = httpHelper.PostForm(url, headers, formValues);

            Assert.NotNull(result.Headers["Authorization"]);
        }
    }
}
