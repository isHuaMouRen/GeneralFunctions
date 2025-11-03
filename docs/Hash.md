|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.Hash` | `Hash` |哈希转换工具类|

```C#
public static string MD5(string text)
```

字符串转MD5（小写十六进制）

|参数|描述|
|-|-|
| `text` |输入字符串（不可为 null）|

|返回值类型|描述|
|-|-|
| `string` |32位小写MD5哈希值|

```C#
public static string SHA1(string text)
```

字符串转SHA1（小写十六进制）

|参数|描述|
|-|-|
| `text` |输入字符串（不可为 null）|

|返回值类型|描述|
|-|-|
| `string` |40位小写SHA1哈希值|

```C#
public static string SHA256(string text)
```

字符串转SHA256（小写十六进制）

|参数|描述|
|-|-|
| `text` |输入字符串（不可为 null）|

|返回值类型|描述|
|-|-|
| `string` |64位小写SHA256哈希值|

```C#
public static string SHA512(string text)
```

字符串转SHA512（小写十六进制）

|参数|描述|
|-|-|
| `text` |输入字符串（不可为 null）|

|返回值类型|描述|
|-|-|
| `string` |128位小写SHA512哈希值|

```C#
public static string CRC32(string text)
```

字符串转CRC32（小写十六进制，简单校验用）

|参数|描述|
|-|-|
| `text` |输入字符串（不可为 null）|

|返回值类型|描述|
|-|-|
| `string` |8位小写CRC32校验值（非安全哈希）|

```C#
public static string RandomString(string[] dict, long length)
```

从字典中生成指定长度的随机字符串

|参数|描述|
|-|-|
| `dict` |字符源数组（每个元素为一个可选字符或子串）|
| `length` |生成字符串的长度（>=0）|

|返回值类型|描述|
|-|-|
| `string` |长度为 length 的随机字符串|

**备注**  
- 所有哈希方法均使用 **UTF-8** 编码输入字符串。  
- 输出均为**小写十六进制**字符串。  
- 输入为 `null` 时抛出 `Exception`（消息：“字符串不可为空”）。  
- CRC32 为自定义实现，使用标准多项式 `0xEDB88320`，适用于文件校验、非加密场景。  
- `RandomString` 使用 `System.Random`（非加密安全），适合一般随机生成，不建议用于密码/令牌。  
- 需添加引用：`using System;` 和 `using System.Text;`。  
- Version: 2025-9-14 10:52  

**示例代码**  
```C#
using HuaZi.Library.Hash;

Console.WriteLine(Hash.MD5("hello")); 
// 输出: 5d41402abc4b2a76b9719d911017c592

Console.WriteLine(Hash.SHA256("hello")); 
// 输出: 2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824

Console.WriteLine(Hash.CRC32("hello")); 
// 输出: 3610a686

string[] dict = { "A", "B", "C", "1", "2", "3" };
Console.WriteLine(Hash.RandomString(dict, 10)); 
// 示例输出: A1B3C2A2B1（每次不同）
```