|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.Cmd` | `Cmd` |控制台操作|

```C#
public static async Task<string> RunCmdAsync(string command, bool showWindow = false, bool closeAfter = true, string workingDirectory = "")
```

异步执行控制台命令

|参数|描述|
|-|-|
| `command` |要执行的命令|
| `showWindow` |是否显示 cmd 窗口（默认为 false）|
| `closeAfter` |执行完成后是否关闭窗口（默认为 true）|
| `workingDirectory` |可选，指定工作目录（默认为空字符串）|

|返回值类型|描述|
|-|-|
| `Task<string>` |命令输出的字符串（仅在隐藏窗口时有效）；如果有错误输出，会附加错误信息；显示窗口时返回空字符串。|
