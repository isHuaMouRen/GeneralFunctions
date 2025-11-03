|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.Json` | `Json` |JSON读写工具（基于Newtonsoft.Json）|

```C
public static T ReadJson<T>(string contentOrPath)
```

读取JSON（支持文件路径或纯文本）

|参数|描述|
|-|-|
| `contentOrPath` |JSON文件路径或JSON字符串|

|返回值类型|描述|
|-|-|
| `T` |反序列化后的对象（泛型）|

|异常|描述|
|-|-|
| `Exception` |内容为空或无效JSON（包装JsonException）|

```C
public static void WriteJson<T>(string filePath, T data)
```

将对象序列化为JSON并写入文件（美化格式）

|参数|描述|
|-|-|
| `filePath` |目标文件路径（覆盖写入）|
| `data` |要序列化的对象|

**备注**  
- **依赖**：Newtonsoft.Json（JsonConvert/JsonSerializer）。  
- **读取**：  
  - 若路径存在文件 → 读取文件内容。  
  - 否则 → 视作JSON字符串直接解析。  
- **写入**：  
  - 使用**Indented**格式（4空格缩进）。  
  - UTF-8编码（StreamWriter默认）。  
- **空处理**：内容为空抛异常；反序列化失败抛包装异常。  
- **线程安全**：非线程安全（文件操作建议外部同步）。  
- **性能**：适合中小型JSON；大文件注意内存占用。  
- **需引用**：  
  ```C#
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq; // 若需JObject
  using System.IO;
  ```  
- 适用于配置读取、数据持久化、API响应解析。  

**示例代码**  
```C
using HuaZi.Library.Json;

// 定义模型
public class Config
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Hobbies { get; set; }
}

// 写入
var cfg = new Config { Name = "Alice", Age = 25, Hobbies = new List<string> { "Reading", "Coding" } };
Json.WriteJson(@"C:\config.json", cfg);

// 输出文件内容（美化）：
{
    "Name": "Alice",
    "Age": 25,
    "Hobbies": [
        "Reading",
        "Coding"
    ]
}

// 读取（文件）
Config loaded = Json.ReadJson<Config>(@"C:\config.json");

// 读取（字符串）
string jsonStr = "{\"Name\":\"Bob\",\"Age\":30,\"Hobbies\":[]}";
Config fromStr = Json.ReadJson<Config>(jsonStr);
```