|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.Memory` | `Memory` |手动内存占用工具（用于测试/压力模拟）|

```C#
public static void Allocate(int megaBytes)
```

无意义地占用指定大小内存（直到程序结束）

|参数|描述|
|-|-|
| `megaBytes` |要占用的内存大小（单位：MB）|

|异常|描述|
|-|-|
| `ArgumentException` |占用大小必须大于0|

```C#
public static long GetTotalMemoryMB()
```

获取当前已占用的总内存

|返回值类型|描述|
|-|-|
| `long` |已占用内存总量（单位：MB）|

**备注**  
- **用途**：模拟高内存负载、测试GC行为、压力测试系统资源。  
- **实现**：  
  - 分配 `megaBytes * 1024 * 1024` 字节的 `byte[]`。  
  - 通过**填充数据**（每4096字节写1）防止JIT优化掉数组。  
  - 使用**静态List**持有引用，防止被垃圾回收（GC）。  
- **释放**：仅在程序退出时由系统自动释放（无手动释放方法）。  
- **输出**：控制台打印分配信息和当前总占用。  
- **线程安全**：非线程安全（多线程调用可能导致竞争）。  
- **性能**：大块分配可能触发L OH（Large Object Heap），影响GC效率。  
- **无依赖**：纯C#实现，无需额外`using`。  
- **注意**：  
  - 过度使用可能导致**OutOfMemoryException**。  
  - 适用于**调试/测试环境**，生产禁用。  
  - 不适合长期内存泄漏模拟（需手动管理）。  

**示例代码**  
```C#
using HuaZi.Library.Memory;

// 占用 500 MB
Memory.Allocate(500); 
// 输出: 已分配 500 MB，总占用 500 MB

// 再占用 200 MB
Memory.Allocate(200); 
// 输出: 已分配 200 MB，总占用 700 MB

// 查询总占用
long total = Memory.GetTotalMemoryMB(); // 700
Console.WriteLine($"当前占用: {total} MB");
```

**控制台输出示例**  
```
已分配 100 MB，总占用 100 MB
已分配 256 MB，总占用 356 MB
当前占用: 356 MB
```

**适用场景**  
- 内存泄漏测试  
- GC压力测试  
- 系统资源监控验证  
- 模拟低内存环境  

> **警告**：请在受控环境中使用，避免系统崩溃！