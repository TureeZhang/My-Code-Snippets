# Json 反序列化抽象类和接口

```cs
public class Program
    {
        public static void Main(string[] args)
        {
            InfoDto data = new InfoDto();
            data.CraeteDate = DateTime.Now;
            data.Persons = new List<IPerson>();
            data.Persons.Add(new Student());
            data.Persons.Add(new Teacher());
            data.Persons.Add(new Boss());
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;
            string json = JsonConvert.SerializeObject(data, Formatting.Indented, settings);

            InfoDto instance = new InfoDto();
            instance = JsonConvert.DeserializeObject<InfoDto>(json, settings);

            Console.WriteLine(instance);
        }
    }

    public class InfoDto
    {

        public InfoDto()
        {

        }

        public DateTime CraeteDate { get; set; }
        public List<IPerson> Persons { get; set; }
    }

    public interface IPerson
    {
        public string Name { get; set; }
    }

    public class Teacher : IPerson
    {
        public string Name { get; set; }

        public string ClassName { get; set; }
    }


    public class Student : IPerson
    {
        public string Name { get; set; }
        public string FriendName { get; set; }
    }

    public class Boss : IPerson
    {
        public string Name { get; set; }
        public int MoneyCount { get; set; }
    }
```

```json
{
  "$type": "WebApplication.InfoDto, WebApplication",
  "CraeteDate": "2021-05-10T15:46:21.9660705+08:00",
  "Persons": [
    {
      "$type": "WebApplication.Student, WebApplication",
      "Name": null,
      "FriendName": null
    },
    {
      "$type": "WebApplication.Teacher, WebApplication",
      "Name": null,
      "ClassName": null
    },
    {
      "$type": "WebApplication.Boss, WebApplication",
      "Name": null,
      "MoneyCount": 0
    }
  ]
}
```
