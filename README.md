# General Functions

自己写程序常用的函数之类的，分享出来。也当做是自己的一个备份了

## 概述

### PosSelectorLib

坐标选择器

---

``` C#
public static Point Show(double opacity = 0.2)
```
显示坐标选择器窗口，点击后返回点击时的鼠标坐标

|参数|类型|默认值|描述|
|-|-|-|-|
|`opacity`|double|0.2|选择器窗口的透明度|

|返回值类型|描述|
|-|-|
|`Point`|坐标|

### CmdLib

命令行工具

---

``` C#
public static async Task<string> RunCmdAsync(string command, bool showWindow = false, bool closeAfter = true, string workingDirectory = "")
```
异步使用cmd运行命令，返回运行结果

|参数|类型|默认值|描述|
|-|-|-|-|
|`command`|string|*None*|命令|
|`showWindow`|bool|false|是否显示窗口|
|`closeAfter`|bool|true|运行完毕后是否关闭窗口|
|`workingDirctory`|string|*空字符串*|运行目录|

|返回值类型|描述|
|-|-|
|`Task<string>`|命令运行结果|

## HashLib

哈希转换

---

``` C#
public static string MD5(string text)
```
字符串转MD5

|参数|类型|默认值|描述|
|-|-|-|-|
|`text`|string|*None*|字符串|

|返回值类型|描述|
|-|-|
|`string`|MD5数据|

---

``` C#
public static string SHA1(string text)
```
字符串转SHA1

|参数|类型|默认值|描述|
|-|-|-|-|
|`text`|string|*None*|字符串|

|返回值类型|描述|
|-|-|
|`string`|SHA1数据|

---

``` C#
public static string SHA256(string text)
```
字符串转SHA256

|参数|类型|默认值|描述|
|-|-|-|-|
|`text`|string|*None*|字符串|

|返回值类型|描述|
|-|-|
|`string`|SHA256数据|

---

``` C#
public static string SHA512(string text)
```
字符串转SHA512

|参数|类型|默认值|描述|
|-|-|-|-|
|`text`|string|*None*|字符串|

|返回值类型|描述|
|-|-|
|`string`|SHA512数据|

### HexLib

文件十六进制数据

---

``` C#
public static async Task<string[]> ReadHexAsync(string filePath, long offset, int count = 16)
```
读取一个文件的十六进制数据

|参数|类型|默认值|描述|
|-|-|-|-|
|`filePath`|string|*None*|文件路径|
|`offset`|long|*None*|起始位置(从0开始)|
|`count`|int|16|读取数量|

|返回值类型|描述|
|-|-|
|`Task<string[]>`|十六进制字符串数组|

---

``` C#
public static async Task ModifyBytesAsync(string filePath, long offset, byte[] newBytes)
```
修改一个文件的数据

|参数|类型|默认值|描述|
|-|-|-|-|
|`filePath`|string|*None*|文件路径|
|`offset`|long|*None*|起始位置(从0开始)|
|`newBytes`|byte[]|*None*|新的数据(覆盖原有数据)|

|返回值类型|描述|
|-|-|
|`Task`|*无返回值*|

---

``` C#
public static async Task InsertBytesAsync(string filePath, long offset, byte[] bytesToInsert)
```
插入一个文件的数据

|参数|类型|默认值|描述|
|-|-|-|-|
|`filePath`|string|*None*|文件路径|
|`offset`|long|*None*|起始位置(从0开始)|
|`bytesToInsert`|byte[]|*None*|要插入的数据(后方数据整体后移)|

|返回值类型|描述|
|-|-|
|`Task`|*无返回值*|

---

``` C#
public static async Task DeleteBytesAsync(string filePath, long offset, int length)
```
删除一个文件的数据

|参数|类型|默认值|描述|
|-|-|-|-|
|`filePath`|string|*None*|文件路径|
|`offset`|long|*None*|起始位置(从0开始)|
|`length`|int|*None*|要删除的数量|

|返回值类型|描述|
|-|-|
|`Task`|*无返回值*|

---

``` C#
public static byte[] HexStringArrayToBytes(string[] hexArray)
```
十六进制数据字符串数组转字节集

|参数|类型|默认值|描述|
|-|-|-|-|
|`hexArray`|string[]|*None*|十六进制数据字符串数组|

|返回值类型|描述|
|-|-|
|`byte[]`|字节集数据|
