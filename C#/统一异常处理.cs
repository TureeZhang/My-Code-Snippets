```c#
AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandleException);

private static void CurrentDomain_UnhandleException(object sender, UnhandledExceptionEventArgs e)
{
    Console.WriteLine(e.ExceptionObject.ToString());
    //版权声明：本文为CSDN博主「小桥00流水」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
    //原文链接：https://blog.csdn.net/haikou9410/article/details/82079548
}
```
