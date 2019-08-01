public IActionResult ValidateCode()
{
    string validateCode = GetNewValidateCode(4);
    MemoryStream imageStream = GetImageStream(validateCode);
    imageStream.Position = 0; //记得将流位置重置为 0，否则会抛出异常 System.InvalidOperationException: Response Content-Length mismatch: too few bytes written (0 of 838).
    string clientId = Request.Cookies["client_id"];

    if (ValidateCodes.ContainsKey(clientId))
        ValidateCodes[clientId] = validateCode;
    else
        ValidateCodes.Add(clientId, validateCode);

    Response.ContentLength = imageStream.Length;
    return new FileStreamResult(imageStream, "image/bmp");
}
