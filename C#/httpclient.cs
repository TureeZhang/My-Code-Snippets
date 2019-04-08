
protected virtual async Task<string> SendHttpPostRequestAsync(Uri url, byte[] datas = null)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = WebRequestMethods.Http.Post;
            request.Headers = BuildHttpRequestAuthorizeHeader();
            request.ContentType = "application/json;charset=utf-8";//确保此处设置了适当的Content-type和Media-Type，否则引发Http 415 Error - Unsupport Media Type
            request.MediaType = "application/json;charset=utf-8";

            if (datas != null && datas.Length > 0)
            {
                Stream requestStream = await request.GetRequestStreamAsync();
                await requestStream.WriteAsync(datas, 0, datas.Length);
            }

            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException("SDXI: POST 请求失败，未能获得 StatusCode=200 的响应。");
            }

            Stream responseStream = response.GetResponseStream();
            return ReadResponseStreamUTF8(responseStream);
        }
