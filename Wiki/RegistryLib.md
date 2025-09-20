# RegistryLib

注册表操作

---

``` C#
public static bool WriteRegistry(RegistryKey rootKey, string subKeyPath, string valueName, object value, RegistryValueKind valueKind)
```
写入注册表项

|参数|类型|默认值|描述|
|-|-|-|-|
|`rootKey`|RegistryKey|*None*|根键|
|`subKeyPath`|string|*None*|路径，前后无需添加反斜杠|
|`valueName`|string|*None*|键名|
|`value`|object|*None*|值|
|`valueKind`|RegistryValueKind|*None*|值类型|

|返回值类型|描述|
|-|-|
|`bool`|操作是否成功|

---

``` C#
public static object ReadRegistryValue(RegistryKey rootKey, string subKeyPath, string valueName, object defaultValue = null)
```
获取当前占用的内存

|参数|类型|默认值|描述|
|-|-|-|-|
|`rootKey`|RegistryKey|*None*|根键|
|`subKeyPath`|string|*None*|路径，前后无需添加反斜杠|
|`valueName`|string|*None*|键名|
|`defaultValue`|object|*null*|如无内容，返回的值|

|返回值类型|描述|
|-|-|
|`object`|读取到的值|