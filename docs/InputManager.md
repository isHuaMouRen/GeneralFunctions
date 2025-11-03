|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.InputManager` | `InputManager` |模拟鼠标/键盘输入（WinAPI实现，全局生效）|

### 鼠标操作

```C
public static (int X, int Y) Mouse.GetMousePosition()
```

获取当前鼠标坐标

|返回值类型|描述|
|-|-|
| `(int X, int Y)` |屏幕绝对坐标（像素）|

```C
public static void Mouse.SetMousePosition(int x, int y)
```

设置鼠标坐标

|参数|描述|
|-|-|
| `x` |X坐标|
| `y` |Y坐标|

```C
public static void Mouse.SetMousePosition((int X, int Y) pos)
```

设置鼠标坐标（Tuple重载）

|参数|描述|
|-|-|
| `pos` |坐标元组|

#### 左键
```C
public static void Mouse.LeftButton.Click()
```
左键点击（当前坐标）

```C
public static void Mouse.LeftButton.Down()
```
左键按下

```C
public static void Mouse.LeftButton.Up()
```
左键释放

#### 右键
```C
public static void Mouse.RightButton.Click()
```
右键点击

```C
public static void Mouse.RightButton.Down()
```
右键按下

```C
public static void Mouse.RightButton.Up()
```
右键释放

#### 中键
```C
public static void Mouse.MiddleButton.Click()
```
中键点击

```C
public static void Mouse.MiddleButton.Down()
```
中键按下

```C
public static void Mouse.MiddleButton.Up()
```
中键释放

#### 滚轮
```C
public static void Mouse.Wheel.ScrollUp(int amount = 120)
```
向上滚动

|参数|描述|
|-|-|
| `amount` |滚动量（正值，WHEEL_DELTA=120为一格）|

```C
public static void Mouse.Wheel.ScrollDown(int amount = 120)
```
向下滚动（负值）

```C
public static void Mouse.Wheel.ScrollRight(int amount = 120)
```
向右水平滚动（部分鼠标支持）

```C
public static void Mouse.Wheel.ScrollLeft(int amount = 120)
```
向左水平滚动

### 键盘操作

```C
public static void Keyboard.KeyDown(byte key)
```

按下键（虚拟键码）

|参数|描述|
|-|-|
| `key` |VK_CODE（0-255），如 `0x41` 为 A|

```C
public static void Keyboard.KeyUp(byte key)
```

释放键

```C
public static async void Keyboard.KeyPress(byte key, int delay = 50)
```

模拟完整按键（按下 → 延迟 → 释放）

|参数|描述|
|-|-|
| `key` |虚拟键码|
| `delay` |按下后延迟（ms，默认50）|

**备注**  
- 使用 **user32.dll** 的 `mouse_event` 和 `keybd_event`，**全局模拟输入**（影响整个系统）。  
- 鼠标操作基于**当前坐标**（Click/Down/Up），无需先Set位置。  
- 坐标为**屏幕绝对像素**（多显示器支持）。  
- 滚轮：120 为一格标准滚动；HWHEEL 需要硬件支持。  
- 键盘：不支持扫描码，仅虚拟键码（详见 [Virtual-Key Codes](https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes)）。  
- `KeyPress` 为**异步**，需 `await` 或在 async 上下文使用；延迟模拟真实输入，避免过快被拦截。  
- **权限**：普通用户即可；UAC 虚拟化可能影响管理员程序。  
- **需引用**：  
  ```C#
  using System.Runtime.InteropServices;
  using System.Drawing;  // Point
  using System.Threading.Tasks;  // Task.Delay
  ```  
- 适用于自动化脚本、宏录制、游戏辅助、RPA 等场景。  
- **注意**：滥用可能触发反作弊系统；生产环境建议结合 `Task.Delay` 增加人性化间隔。  

**示例代码**  
```C
using HuaZi.Library.InputManager;

// 移动鼠标并左键点击
InputManager.Mouse.SetMousePosition(500, 300);
InputManager.Mouse.LeftButton.Click();

// 滚动页面
InputManager.Mouse.Wheel.ScrollDown(360); // 下滚3格

// 按 Ctrl+S 保存
InputManager.Keyboard.KeyDown(0x11); // Ctrl
InputManager.Keyboard.KeyDown(0x53); // S
await Task.Delay(50);
InputManager.Keyboard.KeyUp(0x53);
InputManager.Keyboard.KeyUp(0x11);

// 完整按键
await InputManager.Keyboard.KeyPress(0x0D); // Enter
```

**常用键码**  
- A-Z: `0x41` - `0x5A`  
- 0-9: `0x30` - `0x39`  
- F1-F12: `0x70` - `0x7B`  
- Enter: `0x0D`  
- Shift: `0x10` | Ctrl: `0x11` | Alt: `0x12`