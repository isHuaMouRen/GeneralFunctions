|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.Xml` | `Xml` |XML序列化/反序列化工具（支持文件或字符串）|

```C#
public static T ReadXml<T>(string contentOrPath)
```

读取XML（支持文件路径或纯文本）

|参数|描述|
|-|-|
| `contentOrPath` |XML文件路径或XML字符串|

|返回值类型|描述|
|-|-|
| `T` |反序列化后的对象（泛型）|

|异常|描述|
|-|-|
| `Exception` |内容为空或无效XML（包装实际序列化异常）|

```C#
public static string WriteXml<T>(T obj, string path = null)
```

将对象序列化为XML（可选写入文件）

|参数|描述|
|-|-|
| `obj` |要序列化的对象|
| `path` |目标文件路径（null时仅返回字符串）|

|返回值类型|描述|
|-|-|
| `string` |序列化的XML字符串|

**备注**  
- **序列化**：使用 `System.Xml.Serialization.XmlSerializer`。  
- **读取**：  
  - 若路径存在 → 读取文件。  
  - 否则 → 视作XML字符串。  
- **写入**：  
  - 无命名空间（`xmlns=""` 已移除）。  
  - UTF-8编码。  
  - 若提供`path` → 覆盖写入文件。  
- **格式**：标准XML，无缩进（紧凑）。  
- **要求**：  
  - 类型`T`必须有**无参构造函数**。  
  - 需用 `[XmlRoot]`、`[XmlElement]`、`[XmlAttribute]` 等特性标注（或public字段/属性）。  
- **线程安全**：每次创建新`XmlSerializer`，适合频繁调用。  
- **性能**：适合中小型对象；大XML注意内存。  
- **需引用**：  
  ```C#
  using System.Text;
  using System.Xml.Serialization;
  using System.IO;
  ```  
- 适用于配置存储、数据交换、旧系统集成。  

**示例代码**  
```C#
using HuaZi.Library.Xml;

// 定义模型
[XmlRoot("Person")]
public class Person
{
    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlElement("Age")]
    public int Age { get; set; }

    [XmlArray("Hobbies")]
    [XmlArrayItem("Hobby")]
    public List<string> Hobbies { get; set; }
}

// 写入
var p = new Person 
{ 
    Name = "Alice", 
    Age = 30, 
    Hobbies = new List<string> { "Reading", "Coding" } 
};

string xml = Xml.WriteXml(p, @"C:\person.xml");
// 或仅获取字符串：Xml.WriteXml(p);

// 输出XML：
<?xml version="1.0" encoding="utf-16"?>
<Person>
  <Name>Alice</Name>
  <Age>30</Age>
  <Hobbies>
    <Hobby>Reading</Hobby>
    <Hobby>Coding</Hobby>
  </Hobbies>
</Person>

// 读取（文件）
Person loaded = Xml.ReadXml<Person>(@"C:\person.xml");

// 读取（字符串）
string xmlStr = File.ReadAllText(@"C:\person.xml");
Person fromStr = Xml.ReadXml<Person>(xmlStr);
```

**常见特性**  
```C#
[XmlRoot("RootName")]         // 根元素名
[XmlElement("Elem")]         // 子元素
[XmlAttribute("attr")]       // 属性
[XmlIgnore]                  // 忽略成员
[XmlArray("Items")]          // 数组元素
[XmlArrayItem("Item")]
```