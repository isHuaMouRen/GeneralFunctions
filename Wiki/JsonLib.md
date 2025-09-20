# JsonLib

Json读写

---

``` C#
public static T ReadJson<T>(string contentOrPath)
```
读取Json

|参数|类型|默认值|描述|
|-|-|-|-|
|`ontentOrPath`|string|*None*|Json文本或文件路径|

|返回值类型|描述|
|-|-|
|`T`|读取后返回传入的Json类|

---

``` C#
public static void WriteJson<T>(string filePath, T data)
```
写Json文件

|参数|类型|默认值|描述|
|-|-|-|-|
|`filePath`|string|*None*|文件路径|
|`data`|T|*None*|要写入的数据|


|返回值类型|描述|
|-|-|
|`void`|*无返回值*|