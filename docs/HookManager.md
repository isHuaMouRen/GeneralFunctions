|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.HookManager` | `HookManager` |全局键盘/鼠标低级钩子管理器（Low-Level Hook）|

### 键盘钩子

```C#
public class KeyboardHook : IDisposable
```

全局键盘钩子类，支持捕获按键按下/释放事件

**事件**  
- `KeyDownEvent`：按键按下时触发  
- `KeyUpEvent`：按键释放时触发  

**事件参数**  
```C#
public class KeyboardHookEventArgs : EventArgs
```

|属性|描述|
|-|-|
| `Key` | 虚拟键码（byte，范围 0-255）|
| `Handled` | 设置为 true 可阻止事件传递（吞噬按键）|

**属性**  
|名称|描述|
|-|-|
| `KeysPressed` | 当前按下的所有键（byte[]）|

**方法**  
```C#
public void Dispose()
```
卸载钩子，释放资源

---

### 鼠标钩子

```C#
public class MouseHook : IDisposable
```

全局鼠标钩子类，支持捕获左/中/右键按下/释放

**事件**  
- `MouseDownEvent`：鼠标按下时触发  
- `MouseUpEvent`：鼠标释放时触发  

**事件参数**  
```C#
public class MouseHookEventArgs : EventArgs
```

|属性|描述|
|-|-|
| `Button` | 鼠标按键（Left/Right/Middle）|
| `Handled` | 设置为 true 可阻止事件传递（吞噬点击）|

**枚举**  
```C#
public enum MouseButtons : byte
```
- `None = 0`  
- `Left = 1`  
- `Right = 2`  
- `Middle = 3`

**属性**  
|名称|描述|
|-|-|
| `ButtonsPressed` | 当前按下的所有鼠标键（MouseButtons[]）|

**方法**  
```C#
public void Dispose()
```
卸载钩子，释放资源

**备注**  
- 使用 **WH_KEYBOARD_LL** 和 **WH_MOUSE_LL** 低级钩子，全局生效（包括后台）。  
- 支持**多键同时按下**检测（如 Ctrl+Alt）。  
- `Handled = true` 可**屏蔽系统接收按键/点击**（需谨慎，防止锁死输入）。  
- 键码为 **VK_CODE**（虚拟键码），如 VK_A = 65，VK_SHIFT = 16（详见 [Virtual-Key Codes](https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes)）。  
- 自动获取当前进程模块句柄，无需指定 DLL。  
- 需在 **STA 线程** 或有消息泵的线程中使用（WinForms/WPF 推荐在主线程创建）。  
- 需添加引用：  
  ```C#
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Runtime.InteropServices;
  ```  
- 适用于热键监听、宏录制、输入监控、游戏辅助等场景。  
- **注意**：低级钩子有性能开销，程序退出必须调用 `Dispose()`，否则可能导致系统输入异常。  

**示例代码**  
```C#
// 键盘：屏蔽 F12 并检测 Ctrl+C
using var kb = new HookManager.KeyboardHook();
kb.KeyDownEvent += (s, e) =>
{
    if (e.Key == 0x7B) // VK_F12
    {
        e.Handled = true; // 屏蔽 F12
        Console.WriteLine("F12 被屏蔽！");
    }
    Console.WriteLine($"按下: {(System.Windows.Forms.Keys)e.Key}");
};
kb.KeyUpEvent += (s, e) => Console.WriteLine($"释放: {(System.Windows.Forms.Keys)e.Key}");

// 鼠标：检测右键按下
using var mouse = new HookManager.MouseHook();
mouse.MouseDownEvent += (s, e) =>
{
    if (e.Button == HookManager.MouseButtons.Right)
        Console.WriteLine("右键按下！");
};

// 保持程序运行
Console.WriteLine("按 Enter 退出...");
Console.ReadLine();
```

**当前按键查询示例**  
```C#
byte[] pressed = kb.KeysPressed;
Console.WriteLine("当前按下键数: " + pressed.Length);
```