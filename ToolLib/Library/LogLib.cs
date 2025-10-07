using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace ToolLib.LogLib
{
    public enum LogLevel
    {
        Info,
        Warn,
        Error,
        Debug
    }

    public static class Log
    {
        // 日志输出文件夹
        private static readonly string LogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        // 用于写入的 StreamWriter（整个进程只打开一次）
        private static StreamWriter _writer;
        private static readonly object _lock = new object();

        // 当前选定的日志文件路径（程序运行期间固定）
        public static readonly string CurrentLogFilePath;

        static Log()
        {
            try
            {
                if (!Directory.Exists(LogDir))
                    Directory.CreateDirectory(LogDir);

                // 原子地创建可用的新日志文件：如果名字被占用就尝试带 (1),(2)... 的名字
                CurrentLogFilePath = CreateUniqueLogFilePathForToday();

                // 打开 StreamWriter，允许其他进程读取（FileShare.Read）
                var fs = new FileStream(CurrentLogFilePath,
                    FileMode.Append, FileAccess.Write, FileShare.Read);
                _writer = new StreamWriter(fs, Encoding.UTF8) { AutoFlush = true };

                // 在进程退出时清理
                AppDomain.CurrentDomain.ProcessExit += (s, e) => DisposeWriter();
            }
            catch
            {
                // 构造函数中不要抛出异常，避免影响主程序
                _writer = null;
                CurrentLogFilePath = Path.Combine(LogDir, DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            }
        }

        private static string CreateUniqueLogFilePathForToday()
        {
            string dateName = DateTime.Now.ToString("yyyy-MM-dd");
            int index = 0;
            while (true)
            {
                string candidate = index == 0
                    ? Path.Combine(LogDir, $"{dateName}.log")
                    : Path.Combine(LogDir, $"{dateName}({index}).log");

                // 使用 CreateNew 保证原子性：若文件已存在会抛出 IOException
                try
                {
                    using (var fs = new FileStream(candidate, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                    {
                        // 创建成功，空文件已生成，关闭并返回路径
                    }
                    return candidate;
                }
                catch (IOException)
                {
                    // 文件已存在，尝试下一个编号
                    index++;
                }
                catch
                {
                    // 其它异常（磁盘权限等），退回到不创建仅返回候选路径（最终仍会使用 Append）
                    return candidate;
                }
            }
        }

        private static void DisposeWriter()
        {
            lock (_lock)
            {
                try
                {
                    _writer?.Flush();
                    _writer?.Dispose();
                    _writer = null;
                }
                catch { }
            }
        }

        /// <summary>
        /// 写日志（已去掉行号，只保留调用文件名）
        /// </summary>
        public static void Write(string message, LogLevel level = LogLevel.Info,
            [CallerFilePath] string callerFilePath = "")
        {
            string logTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string fileName = Path.GetFileName(callerFilePath);
            string log = $"[{logTime}] [{level}] [{fileName}] {message}";

            // 控制台着色输出（非阻塞）
            try
            {
                ConsoleColor prev = Console.ForegroundColor;
                switch (level)
                {
                    case LogLevel.Info:
                        Console.ForegroundColor = ConsoleColor.White; break;
                    case LogLevel.Warn:
                        Console.ForegroundColor = ConsoleColor.Yellow; break;
                    case LogLevel.Error:
                        Console.ForegroundColor = ConsoleColor.Red; break;
                    case LogLevel.Debug:
                        Console.ForegroundColor = ConsoleColor.Cyan; break;
                }
                Console.WriteLine(log);
                Console.ForegroundColor = prev;
            }
            catch { /* 控制台输出不能影响主程序 */ }

            // 写入文件（线程安全）
            try
            {
                if (_writer != null)
                {
                    lock (_lock)
                    {
                        _writer.WriteLine(log);
                    }
                }
                else
                {
                    // 如果_writer不可用，做一次性追加（退路）
                    File.AppendAllText(CurrentLogFilePath, log + Environment.NewLine, Encoding.UTF8);
                }
            }
            catch
            {
                // 防止日志写入异常影响主程序
            }
        }

        // 快捷方法
        public static void Info(string message) => Write(message, LogLevel.Info);
        public static void Warn(string message) => Write(message, LogLevel.Warn);
        public static void Error(string message) => Write(message, LogLevel.Error);
        public static void Debug(string message) => Write(message, LogLevel.Debug);
    }
}
