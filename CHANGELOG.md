# Change Log

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