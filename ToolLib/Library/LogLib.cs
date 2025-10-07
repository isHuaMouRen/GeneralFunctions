using System;
using System.IO;
using System.Runtime.CompilerServices;

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

        static Log()
        {
            if (!Directory.Exists(LogDir))
                Directory.CreateDirectory(LogDir);
        }

        /// <summary>
        /// 获取当天日志文件路径，若重名则自动添加(1)、(2)...标号
        /// </summary>
        private static string GetAvailableLogFile()
        {
            string dateName = DateTime.Now.ToString("yyyy-MM-dd");
            string basePath = Path.Combine(LogDir, $"{dateName}.log");
            if (!File.Exists(basePath)) return basePath;

            int i = 1;
            string newPath;
            do
            {
                newPath = Path.Combine(LogDir, $"{dateName}({i}).log");
                i++;
            } while (File.Exists(newPath));

            return newPath;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        public static void Write(string message, LogLevel level = LogLevel.Info,
            [CallerFilePath] string callerFilePath = "")
        {
            string logTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string fileName = Path.GetFileName(callerFilePath);
            string log = $"[{logTime}] [{level}] [{fileName}] {message}";

            // 输出到控制台
            switch (level)
            {
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
            }
            Console.WriteLine(log);
            Console.ResetColor();

            // 写入日志文件
            try
            {
                string logFile = GetAvailableLogFile();
                File.AppendAllText(logFile, log + Environment.NewLine);
            }
            catch
            {
                // 防止日志写入异常影响主程序
            }
        }

        // 快捷方法们
        public static void Info(string message) => Write(message, LogLevel.Info);
        public static void Warn(string message) => Write(message, LogLevel.Warn);
        public static void Error(string message) => Write(message, LogLevel.Error);
        public static void Debug(string message) => Write(message, LogLevel.Debug);
    }
}
