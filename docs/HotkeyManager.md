|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.HotkeyManager` | `HotkeyManager` |系统级全局热键管理器（无需窗口句柄）|

```C#
public void RegisterHotkey(Modifiers modifier, int keyCode, Action callback)
```

注册全局热键

|参数|描述|
|-|-|
| `modifier` |修饰键组合（支持 Alt、Control、Shift、Win，多选可位或）|
| `keyCode` |虚拟键码（VK_CODE，范围 0-255，详见 [Virtual-Key Codes](https://learn.microsoft.com/zh-cn/windows/win32/inputdev/virtual-key-codes)）|
| `callback` |热键触发时的无参回调委托|

|异常|描述|
|-|-|
| `InvalidOperationException` |注册失败（热键已被占用或系统限制）|

```C#
public void UnregisterHotkey(int id)
```

注销指定ID的热键

|参数|描述|
|-|-|
| `id` |注册时分配的热键ID|

```C#
public void Dispose()
```

注销所有热键并清理资源（实现 IDisposable）

**枚举**  
```C#
[Flags]
public enum Modifiers : uint
```
|值|描述|
|-|-|
| `None = 0` |无修饰键|
| `Alt = 1` |Alt 键|
| `Control = 2` |Ctrl 键|
| `Shift = 4` |Shift 键|
| `Win = 8` |Win 键|
> 支持组合：`Modifiers.Control | Modifiers.Alt`

**备注**  
- 使用 **RegisterHotKey(IntPtr.Zero)** 实现**全局热键**，无需窗口句柄（系统直接处理 WM_HOTKEY 消息）。  
- 热键全局生效，即使程序最小化或无焦点。  
- 每个热键分配唯一 ID（自增），用于注销和管理。  
- **不支持**重复注册相同热键（系统会失败）。  
- 需在有**消息泵**的线程中使用（如 WinForms/WPF 主线程），否则热键消息无法接收。  
- 实际接收热键需重写窗口的 `WndProc` 或使用消息循环处理 `WM_HOTKEY`（0x0312）。  
- **缺失功能**：当前类仅注册热键，未提供自动消息处理。需配合以下代码捕获触发：  

**消息处理扩展（必须添加）**  
```C#
// 在 Form 或有 WndProc 的类中
private const int WM_HOTKEY = 0x0312;
private HotkeyManager _hotkeyManager = new HotkeyManager();

protected override void WndProc(ref Message m)
{
    if (m.Msg == WM_HOTKEY)
    {
        int id = m.WParam.ToInt32();
        if (_hotkeyManager._hotkeyActions.TryGetValue(id, out var action))
            action?.Invoke();
    }
    base.WndProc(ref m);
}
```

**需添加引用**  
```C#
using System.Runtime.InteropServices;
using System.Collections.Generic;  // 若无其他 using
```

**示例代码**  
```C#
// 注册 Ctrl + Shift + F12
_hotkeyManager.RegisterHotkey(
    Modifiers.Control | Modifiers.Shift,
    0x7B,  // VK_F12
    () => Console.WriteLine("热键触发！")
);

// 程序退出时自动清理
using var mgr = new HotkeyManager();  // 或手动 Dispose()
```

**注意**  
- 某些键（如 PrintScreen）可能被系统保留。  
- 热键冲突时抛异常，建议 try-catch 包裹注册。  
- 适用于快速启动、截图、切换工具等全局快捷键场景。  
- 兼容 .NET Framework / .NET Core / .NET 5+。