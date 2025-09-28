using Microsoft.Win32;
using System;

namespace ToolLib.RegistryLib
{
    public static class RegistryHelper
    {
        /// <summary>
        /// 写注册表
        /// </summary>
        /// <param name="rootKey">根键</param>
        /// <param name="subKeyPath">路径</param>
        /// <param name="valueName">键名</param>
        /// <param name="value">值</param>
        /// <param name="valueKind">值类型</param>
        /// <returns>是否成功操作</returns>
        public static bool WriteRegistry(RegistryKey rootKey, string subKeyPath, string valueName, object value, RegistryValueKind valueKind = RegistryValueKind.String)
        {
            try
            {
                if (rootKey == null) throw new ArgumentNullException(nameof(rootKey));
                if (string.IsNullOrEmpty(subKeyPath)) throw new ArgumentException("子项路径不能为空", nameof(subKeyPath));

                using (RegistryKey subKey = rootKey.CreateSubKey(subKeyPath))
                {
                    if (subKey == null) return false;
                    subKey.SetValue(valueName, value, valueKind);
                    return true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw; // 权限不足
            }
            catch (Exception ex)
            {
                Console.WriteLine($"写入注册表失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 读注册表项
        /// </summary>
        /// <param name="rootKey">根键</param>
        /// <param name="subKeyPath">路径</param>
        /// <param name="valueName">键名</param>
        /// <param name="defaultValue">默认值，读取失败时返回</param>
        /// <returns>只</returns>
        public static object ReadRegistryValue(RegistryKey rootKey, string subKeyPath, string valueName, object defaultValue = null)
        {
            try
            {
                if (rootKey == null) throw new ArgumentNullException(nameof(rootKey));
                if (string.IsNullOrEmpty(subKeyPath)) throw new ArgumentException("子项路径不能为空", nameof(subKeyPath));

                using (RegistryKey subKey = rootKey.OpenSubKey(subKeyPath, false))
                {
                    return subKey?.GetValue(valueName, defaultValue) ?? defaultValue;
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取注册表失败: {ex.Message}");
                return defaultValue;
            }
        }

        // 泛型读取注册表（直接返回指定类型）
        public static T ReadRegistryValue<T>(RegistryKey rootKey, string subKeyPath, string valueName, T defaultValue = default)
        {
            object value = ReadRegistryValue(rootKey, subKeyPath, valueName, defaultValue);
            return value is T tValue ? tValue : defaultValue;
        }

        /// <summary>
        /// 删除注册表值
        /// </summary>
        /// <param name="rootKey">根键</param>
        /// <param name="subKeyPath">路径</param>
        /// <param name="valueName">键</param>
        /// <returns>是否成功操作</returns>
        public static bool DeleteRegistryValue(RegistryKey rootKey, string subKeyPath, string valueName)
        {
            try
            {
                using (RegistryKey subKey = rootKey.OpenSubKey(subKeyPath, true))
                {
                    if (subKey == null) return false;
                    subKey.DeleteValue(valueName, false);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除注册表失败: {ex.Message}");
                return false;
            }
        }

        // 删除子键
        public static bool DeleteSubKey(RegistryKey rootKey, string subKeyPath)
        {
            try
            {
                rootKey.DeleteSubKey(subKeyPath, false);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除注册表子键失败: {ex.Message}");
                return false;
            }
        }
    }
}
