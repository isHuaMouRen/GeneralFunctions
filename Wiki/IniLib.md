# IniLib

INI配置文件操作

---

``` C#
public static void WriteIni(string filePath, string section, string key, string value)
```
写Ini配置文件

|参数|类型|默认值|描述|
|-|-|-|-|
|`filePath`|string|*None*|文件路径|
|`section`|string|*None*|节|
|`key`|string|*None*|键|
|`value`|string|*None*|值|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static string ReadIni(string filePath, string section, string key)
```
读Ini配置文件

|参数|类型|默认值|描述|
|-|-|-|-|
|`filePath`|string|*None*|文件路径|
|`section`|string|*None*|节|
|`key`|string|*None*|键|

|返回值类型|描述|
|-|-|
|`string`|读取到的值|