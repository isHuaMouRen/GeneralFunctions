using System.Runtime.CompilerServices;
using System.Text;

namespace HuaZi.Library.Logger
{
    public enum LogLevel
    {
        Info,
        Warn,
        Error,
        Debug,
        Fatal
    }

    /// <summary>
    /// 线程安全、每日自动分文件、支持调用位置信息的高性能日志记录器
    /// 支持对象初始化器完整配置
    /// </summary>
    public sealed class Logger : IDisposable
    {
        // ──────────────────────────────────────────────────
        // 可配置属性（支持对象初始化器）
        // ──────────────────────────────────────────────────
        public string? LogDirectory { get; init; }
        public bool ShowCallerInfo { get; init; } = true;
        public bool ShowDate { get; init; } = true;
        public bool ShowTime { get; init; } = true;
        public Encoding Encoding { get; init; } = Encoding.UTF8;

        // ──────────────────────────────────────────────────
        // 只读运行时信息
        // ──────────────────────────────────────────────────
        public string CurrentLogDirectory { get; private set; } = null!;
        public string CurrentLogFilePath { get; private set; } = null!;

        // ──────────────────────────────────────────────────
        // 私有字段
        // ──────────────────────────────────────────────────
        private StreamWriter? _writer;
        private readonly object _lock = new();
        private bool _disposed;

        // ──────────────────────────────────────────────────
        // 构造函数
        // ──────────────────────────────────────────────────
        public Logger()
        {
            Initialize();
            AppDomain.CurrentDomain.ProcessExit += (_, _) => Dispose();
        }

        private void Initialize()
        {
            string directory = LogDirectory ??
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            directory = directory.Trim();

            CurrentLogDirectory = directory;
            if (!Directory.Exists(CurrentLogDirectory))
                Directory.CreateDirectory(CurrentLogDirectory);

            CurrentLogFilePath = GenerateLogFilePath(CurrentLogDirectory);

            var stream = new FileStream(CurrentLogFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
            _writer = new StreamWriter(stream, Encoding) { AutoFlush = true };
        }

        private static string GenerateLogFilePath(string directory)
        {
            string datePrefix = DateTime.Today.ToString("yyyy-MM-dd");
            int index = 0;

            while (true)
            {
                string fileName = index == 0
                    ? $"{datePrefix}.log"
                    : $"{datePrefix}({index}).log";

                string path = Path.Combine(directory, fileName);

                try
                {
                    using var fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                    return path;
                }
                catch (IOException)
                {
                    index++;
                }
            }
        }

        // ──────────────────────────────────────────────────
        // 核心日志方法
        // ──────────────────────────────────────────────────
        public void Log(string message, LogLevel level = LogLevel.Info,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            string timestamp = BuildTimestamp();
            string location = ShowCallerInfo
                ? $"{Path.GetFileName(filePath)}:{lineNumber} {memberName}"
                : "";

            string line = string.IsNullOrEmpty(location)
                ? $"[{timestamp}] [{level}]: {message}"
                : $"[{timestamp}] [{location}] [{level}]: {message}";

            WriteToConsole(line, level);
            WriteToFile(line);
        }

        /// <summary>
        /// 根据 ShowDate / ShowTime 配置动态构建时间戳
        /// </summary>
        private string BuildTimestamp()
        {
            if (ShowDate && ShowTime)
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            if (ShowDate)
                return DateTime.Today.ToString("yyyy-MM-dd");

            if (ShowTime)
                return DateTime.Now.ToString("HH:mm:ss.fff");

            return ""; // 都不显示时返回空字符串
        }

        private static void WriteToConsole(string line, LogLevel level)
        {
            try
            {
                ConsoleColor color = level switch
                {
                    LogLevel.Info => ConsoleColor.White,
                    LogLevel.Warn => ConsoleColor.Yellow,
                    LogLevel.Error => ConsoleColor.Red,
                    LogLevel.Debug => ConsoleColor.Cyan,
                    LogLevel.Fatal => ConsoleColor.Magenta,
                    _ => ConsoleColor.Gray
                };

                ConsoleColor previous = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(line);
                Console.ForegroundColor = previous;
            }
            catch { }
        }

        private void WriteToFile(string line)
        {
            lock (_lock)
            {
                if (_disposed || _writer is null)
                    return;

                try
                {
                    _writer.WriteLine(line);
                }
                catch
                {
                    try
                    {
                        File.AppendAllText(CurrentLogFilePath, line + Environment.NewLine, Encoding);
                    }
                    catch { }
                }
            }
        }

        // ──────────────────────────────────────────────────
        // 便捷方法
        // ──────────────────────────────────────────────────
        public void Info(string msg, [CallerMemberName] string m = "", [CallerFilePath] string f = "", [CallerLineNumber] int l = 0)
            => Log(msg, LogLevel.Info, m, f, l);

        public void Warn(string msg, [CallerMemberName] string m = "", [CallerFilePath] string f = "", [CallerLineNumber] int l = 0)
            => Log(msg, LogLevel.Warn, m, f, l);

        public void Error(string msg, [CallerMemberName] string m = "", [CallerFilePath] string f = "", [CallerLineNumber] int l = 0)
            => Log(msg, LogLevel.Error, m, f, l);

        public void Debug(string msg, [CallerMemberName] string m = "", [CallerFilePath] string f = "", [CallerLineNumber] int l = 0)
            => Log(msg, LogLevel.Debug, m, f, l);

        public void Fatal(string msg, [CallerMemberName] string m = "", [CallerFilePath] string f = "", [CallerLineNumber] int l = 0)
            => Log(msg, LogLevel.Fatal, m, f, l);

        // ──────────────────────────────────────────────────
        // 运行时切换目录
        // ──────────────────────────────────────────────────
        public void ChangeDirectory(string newDirectory)
        {
            if (string.IsNullOrWhiteSpace(newDirectory))
                return;

            string target = newDirectory.Trim();

            lock (_lock)
            {
                if (string.Equals(CurrentLogDirectory, target, StringComparison.OrdinalIgnoreCase))
                    return;

                DisposeWriter();

                CurrentLogDirectory = target;
                if (!Directory.Exists(CurrentLogDirectory))
                    Directory.CreateDirectory(CurrentLogDirectory);

                CurrentLogFilePath = GenerateLogFilePath(CurrentLogDirectory);

                var stream = new FileStream(CurrentLogFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
                _writer = new StreamWriter(stream, Encoding) { AutoFlush = true };
            }
        }

        // ──────────────────────────────────────────────────
        // IDisposable
        // ──────────────────────────────────────────────────
        public void Dispose()
        {
            if (_disposed) return;

            lock (_lock)
            {
                DisposeWriter();
                _disposed = true;
            }
        }

        private void DisposeWriter()
        {
            try
            {
                _writer?.Flush();
                _writer?.Dispose();
            }
            catch { }
            _writer = null;
        }
    }
}