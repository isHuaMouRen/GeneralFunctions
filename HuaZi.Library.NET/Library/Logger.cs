using System;
using System.IO;
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

    public class Logger : IDisposable
    {
        private string _logDir;
        private string _logFilePath;
        private StreamWriter _writer;
        private readonly object _lock = new object();

        public string LogDirectory => _logDir;
        public string CurrentLogFile => _logFilePath;

        /// <summary>
        /// 创建一个新的日志实例
        /// </summary>
        /// <param name="directory">日志目录，默认为程序目录下的Logs</param>
        public Logger(string directory = null!)
        {
            _logDir = string.IsNullOrWhiteSpace(directory)
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs")
                : directory;

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

        public void SetDirectory(string newDir)
        {
            if (string.IsNullOrWhiteSpace(newDir))
                return;

            lock (_lock)
            {
                Dispose();
                _logDir = newDir;
                if (!Directory.Exists(_logDir))
                    Directory.CreateDirectory(_logDir);

                _logFilePath = CreateUniqueLogFilePathForToday();

                var fs = new FileStream(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
                _writer = new StreamWriter(fs, Encoding.UTF8) { AutoFlush = true };
            }
        }

        public void Write(string message, LogLevel level = LogLevel.Info)
        {
            string logTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string log = $"[{logTime}] [{level}] {message}";

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
                    // fallback：直接追加
                    File.AppendAllText(_logFilePath, log + Environment.NewLine, Encoding.UTF8);
                }
            }
        }

        public void Info(string msg) => Write(msg, LogLevel.Info);
        public void Warn(string msg) => Write(msg, LogLevel.Warn);
        public void Error(string msg) => Write(msg, LogLevel.Error);
        public void Debug(string msg) => Write(msg, LogLevel.Debug);
        public void Fatal(string msg) => Write(msg, LogLevel.Fatal);

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
