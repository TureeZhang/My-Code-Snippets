# await 与 .Wait() 的区别

实际上，当方法返回 Task<object> 类型的对象时，不应被立即调用 int a = await MethodAsync(); 获取值。

而应该是：

```C#
    public static async Task Main()
    {
        Task<int> downloading = DownloadDocsMainPageAsync();
        Console.WriteLine($"{nameof(Main)}: Launched downloading.");

        int bytesLoaded = await downloading;
        Console.WriteLine($"{nameof(Main)}: Downloaded {bytesLoaded} bytes.");
    }
```

所以，实际上 await 会在尽可能需要用到 Task 的值时，才做等待。而 .Wait() 则会在调用的时候就立即等待返回值。例如：

```c#
    private static async Task<int> DownloadDocsMainPageAsync()
    {
        Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: About to start downloading.");

        var client = new HttpClient();
        byte[] content = await client.GetByteArrayAsync("https://docs.microsoft.com/en-us/");

        Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: Finished downloading.");
        return content.Length;
    }
```

参见微软文档： <https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/operators/await>
