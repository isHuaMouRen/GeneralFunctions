# MemoryLib

内存操作

---

``` C#
public static void Allocate(int megaBytes)
```
无意义的占用内存，程序结束后释放

|参数|类型|默认值|描述|
|-|-|-|-|
|`megaBytes`|int|*None*|占用大小，单位MB|

|返回值类型|描述|
|-|-|
|`void`|*无返回值*|

---

``` C#
public static long GetTotalMemoryMB()
```
获取当前占用的内存

|返回值类型|描述|
|-|-|
|`long`|占用的内存，单位MB|