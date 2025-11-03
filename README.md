# General Functions

自己写程序常用的函数之类的，分享出来。也当做是自己的一个备份了

## 如何使用

- 前往Github仓库的[Release界面](https://github.com/isHuaMouRen/GeneralFunctions/releases)下载 `Nuget包` 或者 `.dll` 文件

- 命令行或Visual Studio控制台下载[Nuget包](https://www.nuget.org/packages/HuaZisHuazi)
``` powershell
Install-Package HuaZisHuazi
```


## 工具列表

|命名空间|类名|描述|详细|
|-|-|-|-|
|AutoStartLib       |AutoStart      |开机自启控制       |[跳转](/Wiki/AutoStartLib.md)|
|CmdLib             |Cmd            |命令行工具         |[跳转](/Wiki/CmdLib.md)|
|DownloaderLib      |Downloader     |下载器             |[跳转](/Wiki/DownloaderLib.md)|
|GdiHuazi         |GdiTool        |绘制工具           |[跳转](/Wiki/GdiHuazi.md)|
|HashLib            |Hash           |哈希转换           |[跳转](/Wiki/HashLib.md)|
|HexLib             |Hex            |文件十六进制数据操作 |[跳转](/Wiki/HexLib.md)|
|HookLib            |HookManager    |全局钩子       |[跳转](/Wiki/KeyboardHookLib.md)|
|HotkeyManagerLib   |HotkeyManager  |全局热键           |[跳转](/Wiki/HotkeyManagerLib.md)|
|IniLib             |Ini            |INI配置文件操作    |[跳转](/Wiki/IniLib.md)|
|InputLib           |Mouse Keyboard |模拟键鼠操作       |[跳转](/Wiki/InputLib.md)|
|JsonLib            |Json           |Json文件读写       |[跳转](/Wiki/JsonLib.md)|
|LogLib             |Log            |写日志             |[跳转](/Wiki/LogLib.md)|
|MemoryLib          |Memory         |内存操作           |[跳转](/Wiki/MemoryLib.md)|
|XmlLib             |Xml            |读写Xml            |正在开发...|

## 全部引用

``` C#
using Huazi.Library.AutoStart;
using Huazi.Library.Cmd;
using Huazi.Library.Downloader;
using Huazi.Library.GdiTool;
using Huazi.Library.Hash;
using Huazi.Library.Hex;
using Huazi.Library.HookManager;
using Huazi.Library.HotkeyManager;
using Huazi.Library.Ini;
using Huazi.Library.InputManager;
using Huazi.Library.Json;
using Huazi.Library.Logger;
using Huazi.Library.Memory;
using Huazi.Library.Xml;
```