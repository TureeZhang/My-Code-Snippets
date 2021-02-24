# vb.net

```
'转换为枚举类型
DirectCast([Enum].Parse(GetType(EnumiOSDevices), item), EnumiOSDevices)

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

'指定被序列化时的名称
Imports Newtonsoft.Json
Public Class TreeCustomerDataModel
    ''' <summary>
    ''' ID
    ''' </summary>
    <JsonProperty(PropertyName:="id")>
    Public Id As String

    ''' <summary>
    ''' 是否选中
    ''' </summary>
    <JsonProperty(PropertyName:="isselcted")>
    Public IsSelected As Boolean

    ''' <summary>
    ''' 标题
    ''' </summary>
    <JsonProperty(PropertyName:="title")>
    Public Title As String

    ''' <summary>
    ''' 包含的树
    ''' </summary>
    <JsonProperty(PropertyName:="subArray")>
    Public SubArray As List(Of TreeCustomerDataModel)
End Class

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

'比较属性类型并为对象的属性动态赋值
    Private Function ExportProjectItems(projectNumber As String, exportItemIdDic As Dictionary(Of ProjectItemExportTypeEnum, String), itemExporterDic As Dictionary(Of ProjectItemExportTypeEnum, IProjectItemExporter(Of Object))) As ProjectExportPackage

        'validate
        If String.IsNullOrEmpty(projectNumber) Then Throw New ArgumentNullException(NameOf(projectNumber))
        If IsNothing(exportItemIdDic) OrElse exportItemIdDic.Count <= 0 Then Throw New ArgumentNullException(NameOf(exportItemIdDic))

        'export-process
        Dim result As ProjectExportPackage = New ProjectExportPackage()
        result.ProjectInfo = New ProjectInfoExpoter().Export(projectNumber)
        For Each item As KeyValuePair(Of ProjectItemExportTypeEnum, String) In exportItemIdDic

            Dim itemType As ProjectItemExportTypeEnum = item.Key
            Dim itemId As String = item.Value
            If Not itemExporterDic.ContainsKey(itemType) Then Throw New Exception($"不存在目标类型的导出器：{itemType.ToString()}")

            Dim exporter As IProjectItemExporter(Of Object) = itemExporterDic(itemType)
            Dim exportResult As Object = exporter.ExportItems(projectNumber, itemId)

            For Each prop As PropertyInfo In result.GetType().GetProperties()
                If Type.Equals(prop.PropertyType, exportResult.GetType()) Then
                    prop.SetValue(result, exportResult, Nothing)
                End If
            Next

        Next

        'result
        Return result

    End Function
```

## 泛型约束必须具有无参构造函数

```vb.net
    Public Function AutoMapFromEntityBase(Of TResult As New)(sourceInstance As EntityBase) As TResult
        Dim result As TResult = New TResult()
        For Each prop As PropertyInfo In result.GetType().GetProperties()
            '...
        Next
    End Function
```

## 多种泛型约束

```vb.net
Public Class thisClass(Of t As {IComparable, IDisposable, Class, New})
    ' Insert code that defines class members. 
End Class
```

## '委托、事件、传值

```vb.net
public static void Test(string userId, Action<string, string> callback)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "receive",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                callback(userId, message);
            };
            channel.BasicConsume(queue: "receive",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
    Public Sub RegisterConsumer(userId As String, callback As Action(Of String, String))
        Dim consumer As EventingBasicConsumer = New EventingBasicConsumer(Me._channel)
        AddHandler(consumer.Received), Sub(model, ea) callback(userId, Encoding.UTF8.GetString(ea.Body))
        Me._channel.BasicConsume(queue:=userId,
                                 autoAck:=True,
                                 consumer:=consumer)
    End Sub
'不产生返回值的lamda表达式，关键字不用 Function() 而用 Sub
```

## Views

```xml
//c#
@Model MyViewModel;
//vb
@ModelType MyViewModel
```

## 代码片段

```vb.net
<?xml version="1.0" encoding="UTF-8"?>
<CodeSnippets xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
  <CodeSnippet Format="1.0.0">
    <Header>
      <Title>DirectCast([Enum]... 语句</Title>
      <Author>Turee.Zhang</Author>
      <Description>插入 DirectCast([Enum]... 语句。</Description>
      <Shortcut>EnumParse</Shortcut>
    </Header>
    <Snippet>
      <Imports>
         <Import>
          <Namespace>System</Namespace>
        </Import>
      </Imports>
      <Declarations>
        <Object>
          <ID>EnumType</ID>
          <Type>Enum</Type>
          <ToolTip>替换为要转换的枚举类型。</ToolTip>
          <Default>EnumType</Default>
        </Object>
        <Object>
          <ID>EnumString</ID>
          <Type>String</Type>
          <ToolTip>替换为需要转换为枚举的字符串。</ToolTip>
          <Default>"EnumString"</Default>
        </Object>
      </Declarations>
      <Code Language="VB" Kind="method body"><![CDATA[DirectCast([Enum].Parse(GetType($EnumType$), $EnumString$), $EnumType$)]]></Code>
    </Snippet>
  </CodeSnippet>
</CodeSnippets>
```




