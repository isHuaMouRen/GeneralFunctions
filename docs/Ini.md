|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.Ini` | `Ini` |INI配置文件读写工具（纯C#实现，无需WinAPI）|

```C#
public static void WriteIni(string filePath, string section, string key, string value)
```

写入或更新INI配置项（自动创建文件/节/键）

|参数|描述|
|-|-|
| `filePath` |INI文件路径（不存在时创建）|
| `section` |节名（null 或 "" 表示默认无节区）|
| `key` |键名（自动Trim）|
| `value` |值（字符串，支持空）|

```C#
public static string ReadIni(string filePath, string section, string key)
```

读取INI配置项

|参数|描述|
|-|-|
| `filePath` |INI文件路径|
| `section` |节名（null 或 "" 表示默认区）|
| `key` |键名|

|返回值类型|描述|
|-|-|
| `string` |配置值；不存在返回 `null`|

|异常|描述|
|-|-|
| `Exception` |文件不存在（注意：路径为空会抛，未检查）|

```C#
public static void DeleteIni(string filePath, string section, string key)
```

删除指定键

|参数|描述|
|-|-|
| `filePath` |INI文件路径|
| `section` |节名（null 或 "" 表示默认区）|
| `key` |键名|

|异常|描述|
|-|-|
| `ArgumentException` |键名为空|

```C#
public static void DeleteSection(string filePath, string section)
```

删除整个节（包括所有键）

|参数|描述|
|-|-|
| `filePath` |INI文件路径|
| `section` |节名（null 或 "" 表示默认区）|

**备注**  
- **编码**：始终使用 **UTF-8** 读写（支持中文）。  
- **格式**：  
  - 节按字母顺序排序。  
  - 每个节前添加空行分隔。  
  - 无节内容置于文件顶部。  
  - 键值对格式：`key=value`（无空格环绕，等号两侧Trim）。  
- **忽略**：注释（; 或 # 开头）、空行、纯空格行。  
- **大小写**：节名和键名**不区分大小写**（使用 OrdinalIgnoreCase）。  
- **线程安全**：非线程安全（文件读写时加锁推荐外部处理）。  
- **性能**：中小型INI文件（<10KB）适用；大文件会一次性加载内存。  
- **自动清理**：删除键/节后若节为空则移除整个节。  
- 需添加引用：  
  ```C#
  using System.Text;
  using System.IO;      // File, List, Dictionary
  using System.Collections.Generic;
  using System.Linq;
  ```  
- 适用于桌面应用配置、游戏设置、轻量级数据存储。  

**示例INI内容**  
```ini
Name=Alice
Age=30

[Database]
Host=localhost
Port=3306

[Features]
Debug=True
```

**示例代码**  
```C#
using HuaZi.Library.Ini;

// 写入
Ini.WriteIni(@"C:\config.ini", "User", "Name", "Bob");
Ini.WriteIni(@"C:\config.ini", null, "Version", "1.0"); // 默认区

// 读取
string name = Ini.ReadIni(@"C:\config.ini", "User", "Name"); // "Bob"
string port = Ini.ReadIni(@"C:\config.ini", "Database", "Port"); // "3306"

// 删除
Ini.DeleteIni(@"C:\config.ini", "Features", "Debug");
Ini.DeleteSection(@"C:\config.ini", "Database");
```