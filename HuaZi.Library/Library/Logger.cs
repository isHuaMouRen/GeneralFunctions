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
    /// 线程安全、每日分文件、带调用位置的日志记录器
    /// </summary>
    public class Logger : IDisposable
    {
        private string _logDir;
        private string _logFilePath;
        private StreamWriter _writer;
        private readonly object _lock = new object();

        
        public bool ShowCallerInfo { get; set; } = true;
        public string LogDirectory => _logDir;
        public string CurrentLogFile => _logFilePath;

        /// <summary>
        /// 创建一个新的日志实例
        /// </summary>
        /// <param name="directory">日志目录，留空则使用程序根目录下的 Logs 文件夹</param>
        public Logger(string directory = null!)
        {
            _logDir = string.IsNullOrWhiteSpace(directory)
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs")
                : directory.Trim();

            if (!Directory.Exists(_logDir))
                Directory.CreateDirectory(_logDir);

            _logFilePath = CreateUniqueLogFilePathForToday();

            var fs = new FileStream(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
            _writer = new StreamWriter(fs, Encoding.UTF8) { AutoFlush = true };

            AppDomain.CurrentDomain.ProcessExit += (s, e) => Dispose();
        }

        private string CreateUniqueLogFilePathForToday()
        {
            string dateName = DateTime.Now.ToString("yyyy-MM-dd");
            int index = 0;
            while (true)
            {
                string candidate = index == 0
                    ? Path.Combine(_logDir, $"{dateName}.log")
                    : Path.Combine(_logDir, $"{dateName}({index}).log");

                try
                {
                    using (var fs = new FileStream(candidate, FileMode.CreateNew, FileAccess.Write, FileShare.Read)) { }
                    return candidate;
                }
                catch (IOException)
                {
                    index++;
                }
                catch
                {
                    return candidate;
                }
            }
        }

        /// <summary>
        /// 更改日志目录（会自动创建并切换）
        /// </summary>
        public void SetDirectory(string newDir)
        {
            if (string.IsNullOrWhiteSpace(newDir)) return;

            lock (_lock)
            {
                Dispose();
                _logDir = newDir.Trim();
                if (!Directory.Exists(_logDir))
                    Directory.CreateDirectory(_logDir);
                _logFilePath = CreateUniqueLogFilePathForToday();
                var fs = new FileStream(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
                _writer = new StreamWriter(fs, Encoding.UTF8) { AutoFlush = true };
            }
        }

        /// <summary>
        /// 核心写入方法
        /// </summary>
        public void Write(
            string message,
            LogLevel level = LogLevel.Info,
            [CallerMemberName] string callerMember = "",
            [CallerFilePath] string callerFile = "",
            [CallerLineNumber] int callerLine = 0)
        {
            string logTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string location = "";

            if (ShowCallerInfo)
            {
                string fileName = Path.GetFileName(callerFile);
                location = $"{fileName}:{callerLine} in {callerMember}";
            }

            string log = string.IsNullOrEmpty(location)
                ? $"[{logTime}] [{level}]: {message}"
                : $"[{logTime}] [{location}] [{level}]: {message}";

            // 控制台彩色输出
            try
            {
                ConsoleColor prev = Console.ForegroundColor;
                switch (level)
                {
                    case LogLevel.Info: Console.ForegroundColor = ConsoleColor.White; break;
                    case LogLevel.Warn: Console.ForegroundColor = ConsoleColor.Yellow; break;
                    case LogLevel.Error: Console.ForegroundColor = ConsoleColor.Red; break;
                    case LogLevel.Debug: Console.ForegroundColor = ConsoleColor.Cyan; break;
                    case LogLevel.Fatal: Console.ForegroundColor = ConsoleColor.Magenta; break;
                }
                Console.WriteLine(log);
                Console.ForegroundColor = prev;
            }
            catch { }

            // 写入文件
            lock (_lock)
            {
                try
                {
                    _writer?.WriteLine(log);
                }
                catch
                {
                    try
                    {
                        File.AppendAllText(_logFilePath, log + Environment.NewLine, Encoding.UTF8);
                    }
                    catch { }
                }
            }
        }

        // 快捷方法
        public void Info(string msg,
            [CallerMemberName] string m = "",
            [CallerFilePath] string f = "",
            [CallerLineNumber] int l = 0) => Write(msg, LogLevel.Info, m, f, l);

        public void Warn(string msg,
            [CallerMemberName] string m = "",
            [CallerFilePath] string f = "",
            [CallerLineNumber] int l = 0) => Write(msg, LogLevel.Warn, m, f, l);

        public void Error(string msg,
            [CallerMemberName] string m = "",
            [CallerFilePath] string f = "",
            [CallerLineNumber] int l = 0) => Write(msg, LogLevel.Error, m, f, l);

        public void Debug(string msg,
            [CallerMemberName] string m = "",
            [CallerFilePath] string f = "",
            [CallerLineNumber] int l = 0) => Write(msg, LogLevel.Debug, m, f, l);

        public void Fatal(string msg,
            [CallerMemberName] string m = "",
            [CallerFilePath] string f = "",
            [CallerLineNumber] int l = 0) => Write(msg, LogLevel.Fatal, m, f, l);

        public void Dispose()
        {
            lock (_lock)
            {
                try
                {
                    _writer?.Flush();
                    _writer?.Dispose();
                    _writer = null!;
                }
                catch { }
            }
        }
    }
}