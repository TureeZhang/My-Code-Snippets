////Clean solution use FilestreamResult !!

[HttpGet]
public async Task<IActionResult> Get()
{
    var image = System.IO.File.OpenRead("C:\\test\random_image.jpeg");
    return File(image, "image/jpeg");
}
//Explanation:

//In ASP.NET Core you have to use the built-in File() method inside the Controller. This will allow you to manually set the content type.

//Don't create and return HttpResponseMessage, like you were used to using in ASP.NET Web API 2. It doesn't do anything, not even throwing errors!!
