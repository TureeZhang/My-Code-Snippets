//条件是必须为 virtual 方法才可被伪装。
[TestMethod]
public void TestAPIClientAuthorize()
{
    APIClientAuthorizeAttribute attribute = new APIClientAuthorizeAttribute();
    var mockRepo = new Mock<ActionExecutingContext>();
    mockRepo.Setup(repo => repo.HttpContext.Request.Headers[HttpRequestHeader.Authorization.ToString()])
        .Returns("service xxxx");

    attribute.OnActionExecuting(mockRepo.Object);
}
