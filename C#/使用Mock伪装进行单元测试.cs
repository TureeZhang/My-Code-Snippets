//条件是必须为 virtual 方法才可被伪装。引用的测试用库是 Moq -> https://www.nuget.org/packages/Moq/
[TestMethod]
public void TestAPIClientAuthorize()
{
    APIClientAuthorizeAttribute attribute = new APIClientAuthorizeAttribute();
    var mockRepo = new Mock<ActionExecutingContext>();
    mockRepo.Setup(repo => repo.HttpContext.Request.Headers[HttpRequestHeader.Authorization.ToString()])
        .Returns("service xxxx");

    attribute.OnActionExecuting(mockRepo.Object);
}
