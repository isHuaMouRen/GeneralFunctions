|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.Logger` | `Logger` |线程安全日志记录器（文件 + 彩色控制台）|

```C#
public Logger(string directory = null)
```

创建日志实例

|参数|描述|
|-|-|
| `directory` |日志目录（默认: 可执行文件目录\Logs）|

**属性**  
|名称|描述|
|-|-|
| `LogDirectory` |当前日志目录|
| `CurrentLogFile` |当前日志文件路径|

```C#
public void SetDirectory(string newDir)
```

切换日志目录（自动重新初始化文件）

|参数|描述|
|-|-|
| `newDir` |新目录路径|

```C#
public void Write(string message, LogLevel level = LogLevel.Info)
```

写入日志（核心方法）

|参数|描述|
|-|-|
| `message` |日志内容|
| `level` |日志级别（默认 Info）|

**快捷方法**  
```C#
public void Info(string msg)
```  
信息日志（白色）

```C#
public void Warn(string msg)
```  
警告日志（黄色）

```C#
public void Error(string msg)
```  
错误日志（红色）

```C#
public void Debug(string msg)
```  
调试日志（青色）

```C#
public void Fatal(string msg)
```  
致命日志（品红）

```C#
public void Dispose()
```

释放资源（自动Flush并关闭文件流）

**枚举**  
```C#
public enum LogLevel
```
- `Info`  
- `Warn`  
- `Error`  
- `Debug`  
- `Fatal`

**备注**  
- **文件命名**：`yyyy-MM-dd.log` 或 `yyyy-MM-dd(1).log`（同日多实例避免冲突）。  
- **编码**：UTF-8（支持中文）。  
- **格式**：`[yyyy-MM-dd HH:mm:ss.fff] [LEVEL] 消息`  
- **控制台**：彩色输出（静默忽略无Console场景）。  
- **线程安全**：全方法加锁（_lock）。  
- **自动清理**：程序退出时自动Dispose（ProcessExit事件）。  
- **异常容错**：写入失败时fallback到 `File.AppendAllText`。  
- **每日新文件**：同一天多个实例自动编号。  
- **需引用**：  
  ```C#
  using System;
  using System.IO;
  using System.Text;
  ```  
- 适用于桌面应用、工具、长期运行服务日志记录。  

**示例日志文件**  
```
[2025-11-03 04:10:22.123] [Info] 应用启动成功
[2025-11-03 04:10:25.456] [Warn] 配置项缺失，使用默认值
[2025-11-03 04:10:30.789] [Error] 网络连接失败
```

**示例代码**  
```C#
using HuaZi.Library.Logger;

using var logger = new Logger(); // 默认 Logs 文件夹

logger.Info("程序开始运行");
logger.Warn("内存使用率高");
logger.Error("文件读取失败");
logger.Debug("变量 x = 42");
logger.Fatal("应用程序崩溃！");

// 切换目录
logger.SetDirectory(@"C:\MyApp\Logs");

// 快捷使用
logger.Info("操作完成");
```