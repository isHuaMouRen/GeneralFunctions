using System;
using System.IO;
using System.Threading.Tasks;

namespace FileBinaryHelper
{
    public static class BinaryFileHelper
    {
        /// <summary>
        /// 读取文件二进制/十六进制信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">起始位置(从0开始)</param>
        /// <param name="count">读取数量, 默认16, 如文件长度不足, 将会读到文件末尾</param>
        /// <returns>hex: 16进制字符串数组  bin: 二进制字符串数组</returns>
        public static async Task<(string[] hex, string[] bin)> ReadBytesAsync(string filePath, long offset, int count = 16)
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
            string[] bin = new string[buffer.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                hex[i] = buffer[i].ToString("X2");
                bin[i] = Convert.ToString(buffer[i], 2).PadLeft(8, '0');
            }

            return (hex, bin);
        }

        /// <summary>
        /// 修改文件二进制/十六进制信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">起始位置(从0开始)</param>
        /// <param name="newBytes">新数据</param>
        public static async void ModifyBytesAsync(string filePath, long offset, byte[] newBytes)
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
        /// 插入文件的二进制/十六进制信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">起始位置(从0开始)</param>
        /// <param name="bytesToInsert">插入的内容, 插入后内容会整体后移</param>
        public static async void InsertBytesAsync(string filePath, long offset, byte[] bytesToInsert)
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
    }
}
