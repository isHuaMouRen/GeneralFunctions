# GdiToolLib

绘制库

---

``` C#
public static void DrawText(string text, Point p, Color color)
```
绘制文本

|参数|类型|默认值|描述|
|-|-|-|-|
|`text`|string|*None*|文本|
|`p`|Point|*None*|坐标|
|`color`|Color|*None*|颜色|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static void DrawRectangle(Rectangle rect, Color border, Color fill)
```
绘制矩形

|参数|类型|默认值|描述|
|-|-|-|-|
|`rect`|Rectangle|*None*|矩形|
|`border`|Color|*None*|边框颜色|
|`fill`|Color|*None*|填充颜色|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static void DrawRoundRectangle(Rectangle rect, int roundWidth, int roundHeight, Color border, Color fill)
```
绘制圆角矩形

|参数|类型|默认值|描述|
|-|-|-|-|
|`rect`|Rectangle|*None*|矩形|
|`roundWidth`|int|*None*|宽度圆角|
|`roundHeight`|int|*None*|高度圆角|
|`border`|Color|*None*|边框颜色|
|`fill`|Color|*None*|填充颜色|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static void DrawEllipse(Rectangle rect, Color border, Color fill)
```
绘制椭圆

|参数|类型|默认值|描述|
|-|-|-|-|
|`rect`|Rectangle|*None*|区域|
|`border`|Color|*None*|边框颜色|
|`fill`|Color|*None*|填充颜色|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static void DrawLine(Point p1, Point p2, Color color)
```
绘制线

|参数|类型|默认值|描述|
|-|-|-|-|
|`p1`|Point|*None*|点1|
|`p2`|Point|*None*|点2|
|`color`|Color|*None*|颜色|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static void DrawPoint(Point p, Color color)
```
绘制像素

|参数|类型|默认值|描述|
|-|-|-|-|
|`p`|Point|*None*|点|
|`color`|Color|*None*|颜色|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|