# LogLib

日志操作

---

``` C#
public enum LogLevel
{
    Info,
    Warn,
    Error,
    Debug
}
```
日志等级

---

``` C#
public static void Write(string message, LogLevel level = LogLevel.Info,[CallerFilePath] string callerFilePath = "",[CallerLineNumber] int callerLineNumber = 0)
```
写日志

|参数|类型|默认值|描述|
|-|-|-|-|
|`message`|string|*None*|日志信息|
|`level`|LogLevel|LogLevel.Info|日志等级|
|`callerFilePath`|string||自动获取调用文件|
|`callerLineNumber`|int|0|获取调用行号|


|返回值类型|描述|
|-|-|
|`void`|*无返回值*|