|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.GdiTool` | `GdiTool` |GDI+ 屏幕绘图工具（直接在桌面绘制）|

```C#
public static void DrawText(string text, Point p, Color color)
```

在屏幕上绘制文本

|参数|描述|
|-|-|
| `text` |要绘制的文本内容|
| `p` |绘制起始位置（左上角坐标）|
| `color` |文本颜色|

```C#
public static void DrawRectangle(Rectangle rect, Color border, Color fill)
```

在屏幕上绘制矩形

|参数|描述|
|-|-|
| `rect` |矩形区域|
| `border` |边框颜色|
| `fill` |填充颜色（透明请使用 Color.Transparent）|

```C#
public static void DrawRoundRectangle(Rectangle rect, int roundWidth, int roundHeight, Color border, Color fill)
```

在屏幕上绘制圆角矩形

|参数|描述|
|-|-|
| `rect` |矩形区域|
| `roundWidth` |圆角椭圆宽度|
| `roundHeight` |圆角椭圆高度|
| `border` |边框颜色|
| `fill` |填充颜色|

```C#
public static void DrawEllipse(Rectangle rect, Color border, Color fill)
```

在屏幕上绘制椭圆

|参数|描述|
|-|-|
| `rect` |椭圆边界矩形|
| `border` |边框颜色|
| `fill` |填充颜色|

```C#
public static void DrawLine(Point p1, Point p2, Color color)
```

在屏幕上绘制直线

|参数|描述|
|-|-|
| `p1` |起点坐标|
| `p2` |终点坐标|
| `color` |线条颜色|

```C#
public static void DrawPoint(Point p, Color color)
```

在屏幕上绘制单个像素点

|参数|描述|
|-|-|
| `p` |像素点坐标|
| `color` |像素颜色|

**备注**  
- 所有绘制操作直接作用于**屏幕 DC**（桌面），无需窗口句柄，无需 Graphics 对象。  
- 使用 Win32 GDI API 实现，性能高，适用于桌面特效、标注工具、屏幕绘画等场景。  
- 边框固定为 1 像素宽（PS_SOLID 样式）。  
- 支持透明填充（Color.Transparent 对应无填充）。  
- 颜色转换使用 RGB 顺序：`R | (G << 8) | (B << 16)`。  
- 全局共享屏幕 DC（`GetDC(IntPtr.Zero)`），无需手动释放。  
- 需添加引用：`using System.Runtime.InteropServices;` 和 `using System.Drawing;`。  
- **注意**：绘制内容不会持久化，重绘桌面（如切换窗口、最小化）后会消失。  
- 适用于临时标注、游戏外挂、自动化演示等场景。  

**示例代码**  
```C#
//画矩形
GdiTool.DrawRectangle(new Rectangle(100, 100, 300, 200), Color.Red, Color.FromArgb(100, 0, 255, 0));
//写文本
GdiTool.DrawText("Hello GDI", new Point(150, 150), Color.Yellow);
//画线
GdiTool.DrawLine(new Point(100, 100), new Point(400, 300), Color.Blue);
//画椭圆
GdiTool.DrawEllipse(new Rectangle(500, 100, 200, 200), Color.Green, Color.Transparent);
```