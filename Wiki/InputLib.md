# InputLib

模拟键鼠输入

## Mouse类

---

``` C#
public static Point GetMousePosition()
```
获取当前鼠标指针坐标

|返回值类型|描述|
|-|-|
|`Point`|鼠标的坐标|

---

``` C#
public static void SetMousePosition(int x, int y);

public static void SetMousePosition(Point pos)
```
设置鼠标坐标

|参数|类型|默认值|描述|
|-|-|-|-|
|`x`|int|*None*|x坐标|
|`y`|int|*None*|y坐标|
|`pos`|Point|*None*|Point类型坐标|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static class LeftButton
{
    public static void Click();
    public static void Down();
    public static void Up()

}
```
鼠标左键操作

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static class RightButton
{
    public static void Click();
    public static void Down();
    public static void Up()

}
```
鼠标右键操作

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static class MiddleButton
{
    public static void Click();
    public static void Down();
    public static void Up()

}
```
鼠标中键操作

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static class Wheel
{
    public static void ScrollUp(int amount = 120);
    public static void ScrollDown(int amount = 120);
    public static void ScrollRight(int amount = 120);
    public static void ScrollLeft(int amount = 120);
}
```
鼠标滚轮操作

|参数|类型|默认值|描述|
|-|-|-|-|
|`amount`|int|120|滚动距离|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

## Keyboard类

---

``` C#
public static void KeyDown(Keys key);
public static void KeyUp(Keys key);
public async static void KeyPress(Keys key, int delay = 50);
```
键盘操作

|参数|类型|默认值|描述|
|-|-|-|-|
|`key`|Keys|*None*|按键|
|`delay`|int|50|按下与抬起之间的间隔|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|