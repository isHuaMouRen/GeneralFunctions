# Change Log

## 2025.10.22 - 01
### 更新
- 更新 `XmlLib` 库，用于读写xml
### 更改
- 此后的更改日志内，如没有特殊强调，如 `(此更改仅适用于Forms版本)`。默认为两个版本同步更新



## 2025.10.12 - 01
### 更新
- 为 `HookLib` 添加**全局鼠标钩子**相关内容
### 更改
- 更改 `KeyboardHookLib` 为 `HookLib`



## 2025.10.12 - 03
### 修复
- 修复.NET版的nuget无法安装的问题



## 2025.10.12 - 02
### 更改
- 将Nuget包分离为 `HuaZisToolLib(Forms版)` 与 `HuaZisToolLib.NET`



## 2025.10.12 - 01
### 通知
- 现在ToolLib将分为 `Forms` 版本与 `.NET` 版本，若想在.NET 框架使用ToolLib。那么你无需勾选兼容Forms即可使用.NET版的ToolLib
### 更新
- 将大多数通用的库从 `Forms` 移植到.NET，对于深度依赖Forms的，将使用其他方案实现Forms版的同等效果
- 为Nuget包添加图标
### 更改
- 将Forms版本项目更名为 `ToolLib.Forms`
- 引用命名空间改为 `ToolLib.Library.xxx`
- 将原始版库改为 `ToolLib.OLD` 不发布，仅作为存档
### 移除
- 移除了 `RegistryLib`
- 移除了 `PosSelectorLib` 、 `AreaSelectorLib` 、 `ErrorReportBox`



## 2025.10.07 - 03
### 通知
- 此版本将作为 `RegistryLib` **最后存在**的一个版本，下个版本将会移除此库
### 更新
- 为 `LogLib` 添加 `Fatal` 日志等级
### 更改
- 将 `LogLib` 改为实例类，不再是之前的静态类
### 移除
- 移除 `LogLib` 中 `Write()` 方法中的文件名参数，写出的日志也不再包含文件名
- 移除了类内所有的 `try-catch` 保护，需要自行捕捉错误



## 2025.10.07 - 02
### 修复
- 修复上次更新造成的 `LogLib` 每写一行就创建一个日志文件的问题



## 2025.10.07 - 01
### 更改
- 优化 `LogLib`



## 2025.10.04 - 02
### 通知
- **`2025.10.04 - 01` 版本的Nuget版包含的是旧版本Dll，不包含最新版更新的内容，不要下载GithubRelease或Nuget.org内的 `2025.10.04 - 01` 版的包**
- **`2025.10.04 - 02` 版已包含最新更新的内容，请下载此版本**



## 2025.10.04 - 01
### 更新
- 为 `HashLib` 添加 `RandomString()` 方法，用于生成随机字符串
- 更新 `DownloaderLib` 库，用于下载较大文件



## 2025.10.01 - 02
### 更新
- 为 `README.md` 文件添加Nuget包的跳转方式
### 更改
- 修复前几天的更新日志副标题等级错误问题



## 2025.10.01 - 01
### 更新
- 补全没有描述文件的库的描述文件
### 更改
- 对齐 `README.md` 中的列表，使其编辑时更加直观
### 移除
- 移除了 `README.md` 文件中的备注列



## 2025.09.29 - 01
### 更新
- 优化 `.gitignore` 文件
### 更改
- 优化 `KeyboardHookLib`
- 发布时不再携带 `Newtonsoft.Json.dll` 文件，需自行下载。Nuget包则是添加依赖项 



## 2025.09.28 - 04
### 更新
- 更新 `KeyboardHookLib` 库，全局键盘钩子



## 2025.09.28 - 03
### 更改
- 优化 `RegistryLib` 代码



## 2025.09.28 - 02
### 修复
- 修复了没有更改 `README.md` 与 `README.nuget.md` 文件的问题



## 2025.09.28 - 01
### 更新
- 更新 `AutoStartLib` 类，设置此程序是否开机自启动



## 2025.09.26 - 02
### 更新
- 添加 `Nuget-Push.bat` 文件，用于快速发布nuget包，此文件不被公开，因为包括了API Key
### 更改
- 更改此文件中的 `2025.09.25 -02` 为 `2025.09.25 - 02`
- 现在 `AreaSelectorLib` 区域选择器支持长按选择，松开完毕



## 2025.09.25 - 02
### 更新
- 此后将一同发布Nuget包，可直接在Nuget上搜索 `HuaZisToolLib` 进行下载
- 更新 `README.nuget.md` 文件，在Nuget包界面显示的自述文件



## 2025.09.25 - 01
### 更改
- 为 `ErrorReportBoxLib` 中的 `Show()` 方法添加 `exTip` 参数，用于在错误信息上方显示提示
### 更改
- 更改 `ErrorReportBoxLib` 中的详细信息文本框单行内不折行，而是添加横向滚动条
### 修复
- 修复了 `ErrorReportBoxLib` 中，窗体可以更改大小的问题



## 2025.09.24 - 01
### 更新
- 更新 `ErrorReportBoxLib` 类，用于显示一个错误报告窗口



## 2025.09.23 - 01
### 更新
- 之后发布版本之前会经过全面测试，以免错误出现
- 全面重写 `GdiToolLib` 库
### 更改
- 清理 `.gitignore` 文件
### 修复
- 修复 `AreaSelectorLib` 命名空间错误的为 `ToolLib.Library.AreaSelectorLib`
- 修复 `AreaSelectorLib` 中的 `Show()` 方法显示的窗口标签不跟随鼠标的问题



## 2025.09.22 - 01
### 更新
- 更新 `AreaSelectorLib` 库，用于选择一片区域，返回 `Rectangle` 类型
- 更新 `GdiToolLib` 库，用于在屏幕上绘制各种东西
- 自述文件更新 `AreSelectorLib` 与 `GdiToolLib` 的描述



## 2025.09.20 - 02
### 修复
- 修复自述文件和说明文件中关于 `RegistryLib` 的错误



## 2025.09.20 - 01
### 更新
- 更新 `JsonLib` 描述文件
- 更新 `LogLib` 描述文件
- 更新 `MemoryLib` 描述文件
- 更新 `RegistryLib` 描述文件
- 自述文件中的文件列表后方添加 `备注` 列
### 更改
- 更改 `RegistryLib` 的类名为 `RegistryHelper` ，而不是原来的 `RegistryLIB`



## 2025.09.19 - 02
### 更改
- 更改 `Builds` 文件夹结构



## 2025.09.19 - 01
### 更新
- 添加了 `Builds` 文件夹，存放历史版本
### 更改
- 更改 `.gitignore` 文件，屏蔽所有 `.vs` `bin` `obj` 和 `packages` 文件夹
- 现在发布的dll中会存有版本信息
- 此后的更新标题中仅包含日期与当日的第几个发布，不会包含更新类别信息
- 微调 `2025.09.17 - 01` 的更新日志
### 移除
- 移除了Demo
- 移除了部分库中未使用的 using



## 2025.09.18 - 01  更新&更改
### 更新
- 更新 `InputLib` 描述文件
- Demo更新 `PosSelectorLib` 演示
- 此后将把库编译为dll文件，方便调用。
- 此后Release将发布Demo程序与库的dll文件
### 更改
- 移除 `Functions` 文件夹，合并进 `Library` 文件夹内
- 更名库名为 `ToolLib`



## 2025.09.17 - 03 更新&更改
### 更新
- 自述文件内列出所有工具
- 更新 `HotkeyManagerLib` 描述文件
- 更新 `IniLib` 描述文件
### 更改
- 将不同的文件描述拆分为多个独立的文件，可通过自述文件索引查看



## 2025.09.17 - 02 修复
### 修复
- 将自述文件中所有参数以及返回值类型使用 **``** 框起来，以免Markdown误判



## 2025.09.17 - 01 更新
### 更新
- 为项目添加Demo，方便理解每个库的作用(未完成，仅创建项目)
- 每次更新时发布Release
- 优化自述文件，描述每个文件的作用(未完成，仅完成至HexLib)
### 更改
- 此后版本号命名规则将改为 `[YYYY].[MM].[DD] - [当日的第几个更新] [更新类别: 更新 更改 修复 移除]`