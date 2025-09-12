using System.Text;

// HashHelper
// 哈希转换
// Author: isHuaMouRen
// Version: 2025-9-12 18:32

namespace HashHelper
{
    public static class HashHelper
    {
        /// <summary>
        /// 暂无描述
        /// </summary>
        /// <param name="bytes">byte</param>
        /// <returns></returns>
        private static string ToHexLower(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        /// <summary>
        /// 字符串转MD5
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>MD5</returns>
        public static string MD5(string text)
        {
            if (text == null) return null;
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var hash = md5.ComputeHash(bytes);
                return ToHexLower(hash);
            }
        }

        /// <summary>
        /// 字符串转SHA1
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>SHA1哈希值</returns>
        public static string SHA1(string text)
        {
            if (text == null) return null;
            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var hash = sha1.ComputeHash(bytes);
                return ToHexLower(hash);
            }
        }

        /// <summary>
        /// 字符串转SHA256
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>SHA256哈希值</returns>
        public static string SHA256(string text)
        {
            if (text == null) return null;
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var hash = sha.ComputeHash(bytes);
                return ToHexLower(hash);
            }
        }

        /// <summary>
        /// 字符串转SHA512
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>SHA512哈希值</returns>
        public static string SHA512(string text)
        {
            if (text == null) return null;
            using (var sha = System.Security.Cryptography.SHA512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var hash = sha.ComputeHash(bytes);
                return ToHexLower(hash);
            }
        }

        // 简单的 CRC32 实现（常用于校验，不适合作为安全哈希）
        private static readonly uint[] _crcTable = CreateCrcTable();
        private static uint[] CreateCrcTable()
        {
            var table = new uint[256];
            const uint poly = 0xEDB88320u;
            for (uint i = 0; i < 256; i++)
            {
                uint crc = i;
                for (int j = 0; j < 8; j++)
                    crc = (crc & 1) != 0 ? (crc >> 1) ^ poly : (crc >> 1);
                table[i] = crc;
            }
            return table;
        }

        public static string CRC32(string text)
        {
            if (text == null) return null;
            var bytes = Encoding.UTF8.GetBytes(text);
            uint crc = 0xFFFFFFFFu;
            foreach (var b in bytes)
            {
                crc = (crc >> 8) ^ _crcTable[(crc ^ b) & 0xFF];
            }
            crc ^= 0xFFFFFFFFu;
            // 返回 8 位十六进制（小写）
            return crc.ToString("x8");
        }
    }
}