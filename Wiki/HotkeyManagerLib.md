# HotkeyManagerLib

全局热键操作

---

``` C#
public void RegisterHotkey(Modifiers modifier, Keys key, Action callback)
```
注册全局热键

|参数|类型|默认值|描述|
|-|-|-|-|
|`modifier`|HotkeyManager.Modifiers|*None*|修饰键，例如Shift、Ctrl|
|`key`|Keys|*None*|主按键|
|`callback`|Action|*None*|回调函数|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public void UnregisterHotkey(int id)
```
注销全局热键

|参数|类型|默认值|描述|
|-|-|-|-|
|`id`|int|*None*|热键唯一标识ID|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|