using Microsoft.Extensions.DependencyInjection; //需要安装 Microsoft.Extensions.DependencyInjection 扩展包才可获得 serviceProvider.GetService<T>() 根据泛型取得服务的方法。

class Program
{
    static void Main(string[] args)
    {
        IServiceProvider serviceProvider = new ServiceCollection().BuildServiceProvider();
        Debug.Assert(object.ReferenceEquals(serviceProvider, serviceProvider.GetService<IServiceProvider>()));
        Debug.Assert(object.ReferenceEquals(serviceProvider, serviceProvider.GetServices<IServiceProvider>().Single()));
    }
}
