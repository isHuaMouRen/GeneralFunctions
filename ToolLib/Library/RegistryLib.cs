using Microsoft.Win32;
using System;

// RegistryLib
// 注册表操作
// Version: 2025-9-20 9:13

namespace ToolLib.RegistryLib
{
    public static class RegistryHelper
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
        public static bool WriteRegistry(RegistryKey rootKey, string subKeyPath, string valueName, object value, RegistryValueKind valueKind)
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
    }
}