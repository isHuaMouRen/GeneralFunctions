using Microsoft.Win32;
using System.Diagnostics;

namespace HuaZi.Library.AutoStart
{
    public static class AutoStart
    {
        // 注册表路径
        private const string RunKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// 设置开机自启
        /// </summary>
        /// <param name="appName">应用名称（注册表键名）</param>
        public static void Enable(string appName)
        {
            if (string.IsNullOrEmpty(appName))
            {
                throw new ArgumentException("应用名称不能为空或空白。", nameof(appName));
            }

            // 获取进程路径，做空检查
            string? exePath = Process.GetCurrentProcess().MainModule?.FileName;
            if (exePath == null)
            {
                throw new InvalidOperationException("无法获取当前进程的执行路径。");
            }

            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKey, true))
            {
                if (key == null)
                {
                    throw new InvalidOperationException("无法访问注册表键。");
                }

                key.SetValue(appName, exePath);
            }
        }

        /// <summary>
        /// 取消开机自启
        /// </summary>
        /// <param name="appName">应用名称（注册表键名）</param>
        public static void Disable(string appName)
        {
            if (string.IsNullOrEmpty(appName))
            {
                throw new ArgumentException("应用名称不能为空或空白。", nameof(appName));
            }

            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKey, true))
            {
                if (key == null)
                {
                    throw new InvalidOperationException("无法访问注册表键。");
                }

                if (key.GetValue(appName) == null)
                {
                    throw new InvalidOperationException($"注册表中没有找到与 '{appName}' 相关的值。");
                }

                key.DeleteValue(appName);
            }
        }

        /// <summary>
        /// 判断是否已设置开机自启
        /// </summary>
        /// <param name="appName">应用名称（注册表键名）</param>
        /// <returns>是否开机自启</returns>
        public static bool IsEnabled(string appName)
        {
            if (string.IsNullOrEmpty(appName))
            {
                throw new ArgumentException("应用名称不能为空或空白。", nameof(appName));
            }

            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKey, false))
            {
                if (key == null)
                {
                    throw new InvalidOperationException("无法访问注册表键。");
                }

                object? value = key.GetValue(appName);
                if (value == null)
                {
                    throw new InvalidOperationException($"注册表中没有找到与 '{appName}' 相关的值。");
                }

                string? exePath = Process.GetCurrentProcess().MainModule?.FileName;
                if (exePath == null)
                {
                    throw new InvalidOperationException("无法获取当前进程的执行路径。");
                }

                return value.ToString() == exePath;
            }
        }
    }
}
