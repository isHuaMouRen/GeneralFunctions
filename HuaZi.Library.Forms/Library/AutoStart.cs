using Microsoft.Win32;
using System.Windows.Forms;

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
            string exePath = Application.ExecutablePath;
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RunKey, true))
            {
                key.SetValue(appName, exePath);
            }
        }

        /// <summary>
        /// 取消开机自启
        /// </summary>
        /// <param name="appName">应用名称（注册表键名）</param>
        public static void Disable(string appName)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RunKey, true))
            {
                if (key.GetValue(appName) != null)
                {
                    key.DeleteValue(appName);
                }
            }
        }

        /// <summary>
        /// 判断是否已设置开机自启
        /// </summary>
        /// <param name="appName">应用名称（注册表键名）</param>
        /// <returns>是否开机自启</returns>
        public static bool IsEnabled(string appName)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RunKey, false))
            {
                object value = key.GetValue(appName);
                return value != null && value.ToString() == Application.ExecutablePath;
            }
        }
    }
}
