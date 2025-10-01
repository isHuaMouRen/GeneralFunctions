# AutoStartLib

开机自启

---

``` C#
public static void Enable(string appName)
```
为此程序启用开机自动运行

|参数|类型|默认值|描述|
|-|-|-|-|
|`appName`|string|*None*|此应用名称|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static void Disable(string appName)
```
为此程序禁用开机自动运行

|参数|类型|默认值|描述|
|-|-|-|-|
|`appName`|string|*None*|此应用名称，需与启用时设置的相同|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static bool IsEnabled(string appName)
```
检测此应用是否为开机自动运行

|参数|类型|默认值|描述|
|-|-|-|-|
|`appName`|string|*None*|此应用名称，需与启用时设置的相同|

|返回值类型|描述|
|-|-|
|`bool`|是否开机运行|