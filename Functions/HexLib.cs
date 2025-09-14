using System;
using System.IO;
using System.Threading.Tasks;

// HexLib
// 操作文件的十六进制数据
// Version: 2025-9-14 10:50

namespace HexLib
{
    public static class Hex
    {
        /// <summary>
        /// 读取文件十六进制信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">起始位置(从0开始)</param>
        /// <param name="count">读取数量, 默认16, 如文件长度不足, 将会读到文件末尾</param>
        /// <returns>hex: 16进制字符串数组</returns>
        public static async Task<string[]> ReadHexAsync(string filePath, long offset, int count = 16)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("文件不存在！", filePath);

            byte[] buffer = new byte[count];

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                if (offset >= fs.Length)
                    throw new ArgumentOutOfRangeException(nameof(offset), "偏移超出文件长度！");

                fs.Seek(offset, SeekOrigin.Begin);
                int bytesRead = await fs.ReadAsync(buffer, 0, count);
                if (bytesRead < count)
                {
                    Array.Resize(ref buffer, bytesRead);
                }
            }

            string[] hex = new string[buffer.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                hex[i] = buffer[i].ToString("X2"); // 转为十六进制
            }

            return hex;
        }

        /// <summary>
        /// 修改文件十六进制数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">起始位置(从0开始)</param>
        /// <param name="newBytes">新数据</param>
        public static async Task ModifyBytesAsync(string filePath, long offset, byte[] newBytes)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("文件不存在！", filePath);

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Write, FileShare.None, 4096, true))
            {
                if (offset > fs.Length)
                    throw new ArgumentOutOfRangeException(nameof(offset), "偏移超出文件长度！");

                fs.Seek(offset, SeekOrigin.Begin);
                await fs.WriteAsync(newBytes, 0, newBytes.Length);
            }
        }

        /// <summary>
        /// 插入文件十六进制数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">起始位置(从0开始)</param>
        /// <param name="bytesToInsert">插入的内容, 插入后内容会整体后移</param>
        public static async Task InsertBytesAsync(string filePath, long offset, byte[] bytesToInsert)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("文件不存在！", filePath);

            byte[] original;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                original = new byte[fs.Length];
                await fs.ReadAsync(original, 0, (int)fs.Length);
            }

            if (offset > original.Length)
                throw new ArgumentOutOfRangeException(nameof(offset), "偏移超出文件长度！");

            byte[] modified = new byte[original.Length + bytesToInsert.Length];
            Array.Copy(original, 0, modified, 0, offset);
            Array.Copy(bytesToInsert, 0, modified, offset, bytesToInsert.Length);
            Array.Copy(original, offset, modified, offset + bytesToInsert.Length, original.Length - offset);

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            {
                await fs.WriteAsync(modified, 0, modified.Length);
            }
        }

        /// <summary>
        /// 删除文件内的数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">起始位置(从0开始)</param>
        /// <param name="length">长度</param>
        public static async Task DeleteBytesAsync(string filePath, long offset, int length)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("文件不存在！", filePath);

            // 读取整个文件
            byte[] original;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                original = new byte[fs.Length];
                await fs.ReadAsync(original, 0, (int)fs.Length);
            }

            if (offset < 0 || offset + length > original.Length)
                throw new ArgumentOutOfRangeException(nameof(offset), "删除范围超出文件长度！");

            // 创建新数组，长度 = 原长度 - 删除长度
            byte[] modified = new byte[original.Length - length];

            // 复制删除前的数据
            Array.Copy(original, 0, modified, 0, offset);

            // 复制删除后剩余的数据
            Array.Copy(original, offset + length, modified, offset, original.Length - offset - length);

            // 写回文件
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            {
                await fs.WriteAsync(modified, 0, modified.Length);
            }
        }

        /// <summary>
        /// 十六进制字符串数组转字节集
        /// </summary>
        /// <param name="hexArray">十六进制字符串数组</param>
        /// <returns>转化后的字节集</returns>
        public static byte[] HexStringArrayToBytes(string[] hexArray)
        {
            byte[] bytes = new byte[hexArray.Length];
            for (int i = 0; i < hexArray.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexArray[i], 16);
            }
            return bytes;
        }
    }
}
