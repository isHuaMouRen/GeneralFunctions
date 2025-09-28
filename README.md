# General Functions

自己写程序常用的函数之类的，分享出来。也当做是自己的一个备份了

## 如何使用

- 前往Github仓库的[Release界面](https://github.com/isHuaMouRen/GeneralFunctions/releases)下载 `Nuget包` 或者 `.dll` 文件

- 命令行或Visual Studio控制台下载Nuget包
``` powershell
Install-Package HuaZisToolLib
```


## 工具列表

|工具名|类名|描述|详细|备注|
|-|-|-|-|-|
|AreaSelectorLib|AreaSelector|区域选择器|正在开发...|测试版，请等待稳定后再使用|
|ErrorReportBoxLib|ErrorReportBox|错误报告提示框|正在开发...||
|PosSelectorLib|PosSelector|坐标选择器|[跳转](/Wiki/PosSelectorLib.md)||
|AutoStartLib|AutoStart|开机自启控制|正在开发...|仅支持Forms应用|
|CmdLib|Cmd|命令行工具|[跳转](/Wiki/CmdLib.md)||
|GdiToolLib|GdiTool|绘制工具|正在开发...|测试版，请等待稳定后再使用|
|HashLib|Hash|哈希转换|[跳转](/Wiki/HashLib.md)||
|HexLib|Hex|文件十六进制数据操作|[跳转](/Wiki/HexLib.md)||
|HotkeyManagerLib|HotkeyManager|全局热键|[跳转](/Wiki/HotkeyManagerLib.md)||
|IniLib|Ini|INI配置文件操作|[跳转](/Wiki/IniLib.md)||
|InputLib|Mouse Keyboard|模拟键鼠操作|[跳转](/Wiki/InputLib.md)||
|JsonLib|Json|Json文件读写|[跳转](/Wiki/JsonLib.md)||
|LogLib|Log|写日志|[跳转](/Wiki/LogLib.md)||
|MemoryLib|Memory|内存操作|[跳转](/Wiki/MemoryLib.md)||
|RegistryLib|RegistryHelper|注册表操作|[跳转](/Wiki/RegistryLib.md)|仅支持Windows操作系统|

## 全部引用

``` C#
using ToolLib.AreaSelectorLib;
using ToolLib.ErrorReportLib;
using ToolLib.PosSelectorLib;
using ToolLib.AutoStartLib;
using ToolLib.CmdLib;
using ToolLib.GdiToolLib;
using ToolLib.HashLib;
using ToolLib.HexLib;
using ToolLib.HotkeyManagerLib;
using ToolLib.IniLib;
using ToolLib.InputLib;
using ToolLib.JsonLib;
using ToolLib.LogLib;
using ToolLib.MemoryLib;
using ToolLib.RegistryLib;
```