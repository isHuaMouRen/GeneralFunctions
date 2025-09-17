# CmdLib

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