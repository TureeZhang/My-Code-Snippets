```cs
static object lockObjStatic = new object();
object lockObj = new object();
public  void test()
{
    object lockObjtemp = new object();
    //lockObjtemp  无论是否同一个对象,锁都不起作用
    //lockObj  同一个对象下锁起作用,不通的对象下锁不起作用
    //lockObjStatic 不管同一个对象还是非同一个对象锁都起作用
    lock (lockObjStatic) //正常用锁的时候,一定要锁定私有静态字段
    {
        add();
    }
}
```

其实就是去同一个地方，如果你们去的都是 static object 同一个地方，则都要排队。

如果你们去的不是同一个地方，还排什么队。
