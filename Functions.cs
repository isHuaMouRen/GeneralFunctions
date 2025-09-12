using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// General Functions
// Version: 2025-9-11 20
// Author: isHuaMouRen
// License: MIT

namespace Functions
{
    public static class SystemHelper
    {
        //写注册表项
        //rootKey常用常量
        //Registry.CurrentUser (HKEY_CURRENT_USER)
        //Registry.LocalMachine (HKEY_LOCAL_MACHINE)
        //Registry.ClassesRoot (HKEY_CLASSES_ROOT)

        /*valueKind：支持的类型包括：
        String：字符串值
        DWord：32位整数
        QWord：64位整数
        Binary：二进制数据
        MultiString：字符串数组*/

        /// <summary>
        /// 写注册表项
        /// </summary>
        /// <param name="rootKey">根键，通常为Registry.CurrentUser(HKEY_CURRENT_USER)；Registry.LocalMachine(HKEY_LOCAL_MACHINE)；Registry.ClassesRoot(HKEY_CLASSES_ROOT)</param>
        /// <param name="subKeyPath">路径，前方与后方不需要加"\"</param>
        /// <param name="valueName">键名</param>
        /// <param name="value">欲写入的值</param>
        /// <param name="valueKind">键类型通常为String(字符串)；DWord(32为整数)；QWord(64位整数)；Binary(二进制数)；MultiString(字符串数组)</param>
        /// <returns>是否写入成功</returns>
        public static bool WriteRegistryValue(RegistryKey rootKey, string subKeyPath, string valueName, object value, RegistryValueKind valueKind)
        {
            try
            {
                if (rootKey == null)
                    throw new ArgumentNullException(nameof(rootKey));

                if (string.IsNullOrEmpty(subKeyPath))
                    throw new ArgumentException("子项路径不能为空", nameof(subKeyPath));

                using (RegistryKey subKey = rootKey.CreateSubKey(subKeyPath))
                {
                    if (subKey == null) return false;

                    subKey.SetValue(valueName, value, valueKind);
                    return true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                // 权限不足，可能需要以管理员身份运行
                throw;
            }
            catch (Exception ex)
            {
                // 记录异常或处理其他错误
                Console.WriteLine($"写入注册表失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 读取注册表项
        /// </summary>
        /// <param name="rootKey">根键，通常为Registry.CurrentUser(HKEY_CURRENT_USER)；Registry.LocalMachine(HKEY_LOCAL_MACHINE)；Registry.ClassesRoot(HKEY_CLASSES_ROOT)</param>
        /// <param name="subKeyPath">路径，前方与后方不需要加"\"</param>
        /// <param name="valueName">键名</param>
        /// <param name="defaultValue">默认值，在读取失败时返回此值，默认为null</param>
        /// <returns>目标项的值</returns>
        public static object ReadRegistryValue(RegistryKey rootKey, string subKeyPath, string valueName, object defaultValue = null)
        {
            try
            {
                if (rootKey == null)
                    throw new ArgumentNullException(nameof(rootKey));

                if (string.IsNullOrEmpty(subKeyPath))
                    throw new ArgumentException("子项路径不能为空", nameof(subKeyPath));

                using (RegistryKey subKey = rootKey.OpenSubKey(subKeyPath, false))
                {
                    // 子项不存在时返回默认值
                    if (subKey == null) return defaultValue;

                    // 获取值（值不存在时返回默认值）
                    return subKey.GetValue(valueName, defaultValue);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // 权限不足，可能需要管理员权限
                throw;
            }
            catch (Exception ex)
            {
                // 记录异常或处理其他错误
                Console.WriteLine($"读取注册表失败: {ex.Message}");
                return defaultValue;
            }
        }

        /// <summary>
        /// 写ini配置项
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void WriteConfig(string filePath, string section, string key, string value)
        {
            var sections = ParseConfigFile(filePath);

            // 创建或更新节
            if (!sections.ContainsKey(section))
            {
                sections[section] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }

            // 更新键值
            sections[section][key.Trim()] = value;

            // 生成配置文件内容
            var lines = new List<string>();

            // 处理默认节（空节名）
            if (sections.TryGetValue("", out var defaultSection) && defaultSection.Count > 0)
            {
                lines.AddRange(defaultSection.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            }

            // 处理带节名的配置（按字母顺序排序）
            foreach (var sec in sections.Keys
                .Where(s => !string.IsNullOrEmpty(s))
                .OrderBy(s => s, StringComparer.OrdinalIgnoreCase))
            {
                // 添加节分隔空行
                if (lines.Count > 0) lines.Add("");

                lines.Add($"[{sec}]");
                lines.AddRange(sections[sec].Select(kvp => $"{kvp.Key}={kvp.Value}"));
            }

            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }

        /// <summary>
        /// 读ini配置项
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <returns>目标值</returns>
        public static string ReadConfig(string filePath, string section, string key)
        {
            if (!File.Exists(filePath)) return null;

            var sections = ParseConfigFile(filePath);

            if (sections.TryGetValue(section, out var sectionData) &&
                sectionData.TryGetValue(key, out var value))
            {
                return value;
            }
            return null;
        }

        // 解析配置文件为节字典(读写配置)
        private static Dictionary<string, Dictionary<string, string>> ParseConfigFile(string filePath)
        {
            var sections = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
            string currentSection = "";

            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    var trimmed = line.Trim();

                    // 处理节头
                    if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
                    {
                        currentSection = trimmed.Substring(1, trimmed.Length - 2).Trim();
                        if (!sections.ContainsKey(currentSection))
                        {
                            sections[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        }
                        continue;
                    }

                    // 处理键值对
                    var parts = line.Split(new[] { '=' }, 2);
                    if (parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[0]))
                    {
                        var k = parts[0].Trim();
                        var v = parts[1].Trim();

                        if (!sections.ContainsKey(currentSection))
                        {
                            sections[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        }

                        sections[currentSection][k] = v;
                    }
                }
            }
            return sections;
        }

        /// <summary>
        /// 删除配置键
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <exception cref="ArgumentException"></exception>
        public static void DeleteConfig(string filePath, string section, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty", nameof(key));

            var sections = ParseConfigFile(filePath);
            section = section ?? "";

            if (sections.TryGetValue(section, out var sectionData) && sectionData.Remove(key))
            {
                // 如果节已空则移除整个节
                if (sectionData.Count == 0)
                {
                    sections.Remove(section);
                }

                SaveSectionsToFile(filePath, sections);

            }
        }

        // 统一保存配置的方法
        private static void SaveSectionsToFile(string filePath, Dictionary<string, Dictionary<string, string>> sections)
        {
            var lines = new List<string>();

            // 处理默认节
            if (sections.TryGetValue("", out var defaultSection) && defaultSection.Count > 0)
            {
                lines.AddRange(defaultSection.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            }

            // 处理带节名的配置（按字母排序）
            foreach (var sec in sections.Keys
                .Where(s => !string.IsNullOrEmpty(s))
                .OrderBy(s => s, StringComparer.OrdinalIgnoreCase))
            {
                var sectionData = sections[sec];
                if (sectionData.Count == 0) continue;

                if (lines.Count > 0) lines.Add("");
                lines.Add($"[{sec}]");
                lines.AddRange(sectionData.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            }

            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }

        /// <summary>
        /// 删除配置节
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="section">节</param>
        public static void DeleteSection(string filePath, string section)
        {
            var sections = ParseConfigFile(filePath);
            section = section ?? "";

            if (sections.Remove(section))
            {
                SaveSectionsToFile(filePath, sections);
            }
        }

        /// <summary>
        /// 从JSON文件读取并反序列化为对象
        /// </summary>
        /*public static T ReadJson<T>(string content)
        {
            JsonConvert.DeserializeObject<xxx>(jsonContent);
        }*/

        /// <summary>
        /// 将对象序列化为JSON并写入文件
        /// </summary>
        public static void WriteJson<T>(string filePath, T data)
        {
            var serializer = new JsonSerializer
            {
                Formatting = Newtonsoft.Json.Formatting.Indented
            };

            using (var sw = new StreamWriter(filePath))
            using (var writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Newtonsoft.Json.Formatting.Indented;
                writer.IndentChar = ' ';
                writer.Indentation = 4;
                serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// 异步执行控制台命令
        /// </summary>
        /// <param name="command">要执行的命令</param>
        /// <param name="showWindow">是否显示 cmd 窗口</param>
        /// <param name="closeAfter">执行完成后是否关闭窗口</param>
        /// <param name="workingDirectory">可选，指定工作目录</param>
        /// <returns>命令输出的字符串（仅在隐藏窗口时有效）</returns>
        public static async Task<string> RunCmdAsync(string command, bool showWindow = false, bool closeAfter = true, string workingDirectory = "")
        {
            return await Task.Run(() =>
            {
                try
                {
                    string argPrefix = closeAfter ? "/c " : "/k "; // /c 执行后关闭, /k 执行后保留

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = argPrefix + command,
                        RedirectStandardOutput = !showWindow,
                        RedirectStandardError = !showWindow,
                        UseShellExecute = showWindow,
                        CreateNoWindow = !showWindow
                    };

                    if (!string.IsNullOrEmpty(workingDirectory))
                    {
                        psi.WorkingDirectory = workingDirectory;
                    }

                    using (Process process = Process.Start(psi))
                    {
                        if (showWindow)
                        {
                            process.WaitForExit();
                            return "";
                        }
                        else
                        {
                            string output = process.StandardOutput.ReadToEnd();
                            string error = process.StandardError.ReadToEnd();
                            process.WaitForExit();

                            return string.IsNullOrEmpty(error) ? output : output + Environment.NewLine + "错误: " + error;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "执行异常: " + ex.Message;
                }
            });
        }
    }

    public class InputManager
    {
        public static class Mouse
        {
            // --- WinAPI ---
            private const int MOUSEEVENTF_MOVE = 0x0001;
            private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
            private const int MOUSEEVENTF_LEFTUP = 0x0004;
            private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
            private const int MOUSEEVENTF_RIGHTUP = 0x0010;
            private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
            private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
            private const int MOUSEEVENTF_WHEEL = 0x0800; // 滚轮
            private const int MOUSEEVENTF_HWHEEL = 0x1000; // 横向滚轮（部分鼠标支持）

            [DllImport("user32.dll")]
            private static extern bool SetCursorPos(int X, int Y);

            [DllImport("user32.dll")]
            private static extern bool GetCursorPos(out Point lpPoint);

            [DllImport("user32.dll")]
            private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

            /// <summary>
            /// 获取当前鼠标坐标
            /// </summary>
            public static Point GetMousePosition()
            {
                GetCursorPos(out Point p);
                return p;
            }

            /// <summary>
            /// 设置鼠标位置
            /// </summary>
            public static void SetMousePosition(int x, int y)
            {
                SetCursorPos(x, y);
            }

            // ========= 左键 =========
            public static class LeftButton
            {
                public static void Click()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, pos.X, pos.Y, 0, 0);
                }

                public static void Down()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_LEFTDOWN, pos.X, pos.Y, 0, 0);
                }

                public static void Up()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_LEFTUP, pos.X, pos.Y, 0, 0);
                }
            }

            // ========= 右键 =========
            public static class RightButton
            {
                public static void Click()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, pos.X, pos.Y, 0, 0);
                }

                public static void Down()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, pos.X, pos.Y, 0, 0);
                }

                public static void Up()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_RIGHTUP, pos.X, pos.Y, 0, 0);
                }
            }

            // ========= 中键 =========
            public static class MiddleButton
            {
                public static void Click()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, pos.X, pos.Y, 0, 0);
                }

                public static void Down()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, pos.X, pos.Y, 0, 0);
                }

                public static void Up()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_MIDDLEUP, pos.X, pos.Y, 0, 0);
                }
            }

            // ========= 滚轮 =========
            public static class Wheel
            {
                /// <summary>
                /// 向上滚动一格（120）
                /// </summary>
                public static void ScrollUp(int amount = 120)
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_WHEEL, pos.X, pos.Y, amount, 0);
                }

                /// <summary>
                /// 向下滚动一格（-120）
                /// </summary>
                public static void ScrollDown(int amount = 120)
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_WHEEL, pos.X, pos.Y, -amount, 0);
                }

                /// <summary>
                /// 向右滚动（水平滚轮，正值向右）
                /// </summary>
                public static void ScrollRight(int amount = 120)
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_HWHEEL, pos.X, pos.Y, amount, 0);
                }

                /// <summary>
                /// 向左滚动（水平滚轮，负值向左）
                /// </summary>
                public static void ScrollLeft(int amount = 120)
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_HWHEEL, pos.X, pos.Y, -amount, 0);
                }
            }
        }

        public static class Keyboard
        {
            // --- WinAPI ---
            [DllImport("user32.dll", SetLastError = true)]
            private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

            private const int KEYEVENTF_KEYDOWN = 0x0000; // 按下
            private const int KEYEVENTF_KEYUP = 0x0002;   // 松开

            /// <summary>
            /// 按下某个键
            /// </summary>
            public static void KeyDown(Keys key)
            {
                keybd_event((byte)key, 0, KEYEVENTF_KEYDOWN, 0);
            }

            /// <summary>
            /// 松开某个键
            /// </summary>
            public static void KeyUp(Keys key)
            {
                keybd_event((byte)key, 0, KEYEVENTF_KEYUP, 0);
            }

            /// <summary>
            /// 模拟一次按键（按下再释放）
            /// </summary>
            public static void KeyPress(Keys key, int delay = 50)
            {
                KeyDown(key);
                System.Threading.Thread.Sleep(delay); // 模拟真实按键的停顿
                KeyUp(key);
            }
        }
    }
}
