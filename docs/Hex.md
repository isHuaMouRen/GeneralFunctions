|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.Hex` | `Hex` |文件十六进制读写工具（异步操作）|

```C
public static async Task<string[]> ReadHexAsync(string filePath, long offset, int count = 16)
```

读取文件指定位置的十六进制数据

|参数|描述|
|-|-|
| `filePath` |文件路径|
| `offset` |起始偏移（从0开始）|
| `count` |读取字节数（默认16，若文件剩余不足则读到末尾）|

|返回值类型|描述|
|-|-|
| `Task<string[]>` |每个字节的大写十六进制字符串数组（如 ["A1", "FF"]）|

```C
public static async Task ModifyBytesAsync(string filePath, long offset, byte[] newBytes)
```

修改文件指定位置的字节数据（覆盖写入）

|参数|描述|
|-|-|
| `filePath` |文件路径|
| `offset` |起始偏移（从0开始）|
| `newBytes` |要写入的新字节数组|

```C
public static async Task InsertBytesAsync(string filePath, long offset, byte[] bytesToInsert)
```

在文件指定位置插入字节数据（原内容后移）

|参数|描述|
|-|-|
| `filePath` |文件路径|
| `offset` |插入位置（从0开始）|
| `bytesToInsert` |要插入的字节数组|

```C
public static async Task DeleteBytesAsync(string filePath, long offset, int length)
```

删除文件中指定范围的字节数据

|参数|描述|
|-|-|
| `filePath` |文件路径|
| `offset` |起始偏移（从0开始）|
| `length` |要删除的字节长度|

```C
public static byte[] HexStringArrayToBytes(string[] hexArray)
```

将十六进制字符串数组转换为字节数组（同步）

|参数|描述|
|-|-|
| `hexArray` |十六进制字符串数组（支持大小写）|

|返回值类型|描述|
|-|-|
| `byte[]` |转换后的字节数组|

**异常**  
- `FileNotFoundException`：文件不存在  
- `ArgumentOutOfRangeException`：偏移或范围超出文件长度  

**备注**  
- 所有文件操作均为**异步**，使用缓冲区 4096 字节，支持大文件。  
- `ReadHexAsync` 输出**大写**十六进制（如 "FF"），每字节两位。  
- `ModifyBytesAsync` 仅覆盖，不改变文件长度。  
- `InsertBytesAsync` 和 `DeleteBytesAsync` 会**重写整个文件**，适用于中小文件（<100MB 推荐）。  
- `HexStringArrayToBytes` 使用 `Convert.ToByte(..., 16)`，支持 "ff"、"FF"、"0xFF" 需手动去除前缀。  
- 无需额外 `using`（代码中已使用完整命名空间如 `System.IO.File`）。  
- 适用于十六进制编辑器、文件补丁、数据修复等场景。  

**示例代码**  
```C
// 读取前16字节
string[] hex = await Hex.ReadHexAsync(@"C:\test.bin", 0);
Console.WriteLine(string.Join(" ", hex)); // 输出: A1 B2 C3 ...

// 修改第10字节为 0xFF
await Hex.ModifyBytesAsync(@"C:\test.bin", 10, new byte[] { 0xFF });

// 插入数据
await Hex.InsertBytesAsync(@"C:\test.bin", 100, new byte[] { 0xDE, 0xAD, 0xBE, 0xEF });

// 删除10字节
await Hex.DeleteBytesAsync(@"C:\test.bin", 50, 10);

// 转换回字节
byte[] data = Hex.HexStringArrayToBytes(new string[] { "A1", "ff", "00" });
// data = [0xA1, 0xFF, 0x00]
```