# HexLib

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