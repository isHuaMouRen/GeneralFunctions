using System;
using System.IO;
using System.Runtime.CompilerServices;

// LogLib
// 日志工具
// Version: 2025-9-14 11:49

namespace LogLib
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
        /// 写日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="level">日志等级</param>
        /// <param name="callerFilePath">自动获取调用文件</param>
        /// <param name="callerLineNumber">自动获取调用行号</param>
        public static void Write(string message, LogLevel level = LogLevel.Info,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            string logTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string fileName = Path.GetFileName(callerFilePath);
            string log = $"[{logTime}] [{level}] [{fileName}:{callerLineNumber}] {message}";

            // 输出到控制台
            switch (level)
            {
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.White; break;
                case LogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow; break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red; break;
            }
            Console.WriteLine(log);
            Console.ResetColor();

            // 输出到日志文件（按天分文件）
            string logFile = Path.Combine(LogDir, DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            try
            {
                File.AppendAllText(logFile, log + Environment.NewLine);
            }
            catch { /* 防止日志写入异常影响主程序 */ }
        }

        /// <summary>
        /// 快捷方法：信息日志
        /// </summary>
        public static void Info(string message) => Write(message, LogLevel.Info);

        /// <summary>
        /// 快捷方法：警告日志
        /// </summary>
        public static void Warn(string message) => Write(message, LogLevel.Warn);

        /// <summary>
        /// 快捷方法：错误日志
        /// </summary>
        public static void Error(string message) => Write(message, LogLevel.Error);

        /// <summary>
        /// 快捷方法: 调试日志
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message) => Write(message, LogLevel.Debug);
    }
}
