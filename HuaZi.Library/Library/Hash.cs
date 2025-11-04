using System.Security.Cryptography;
using System.Text;

namespace HuaZi.Library.Hash
{
    /// <summary>
    /// 哈希转换
    /// </summary>
    public static class Hash
    {
        /// <summary>
        /// 支持的哈希算法枚举
        /// </summary>
        public enum HashAlgorithmType
        {
            MD5,
            SHA1,
            SHA256,
            SHA384,
            SHA512
        }

        private static HashAlgorithm CreateAlgorithm(HashAlgorithmType type)
        {
            return type switch
            {
                HashAlgorithmType.MD5 => System.Security.Cryptography.MD5.Create(),
                HashAlgorithmType.SHA1 => System.Security.Cryptography.SHA1.Create(),
                HashAlgorithmType.SHA256 => System.Security.Cryptography.SHA256.Create(),
                HashAlgorithmType.SHA384 => System.Security.Cryptography.SHA384.Create(),
                HashAlgorithmType.SHA512 => System.Security.Cryptography.SHA512.Create(),
                _ => throw new NotSupportedException($"不支持的算法: {type}")
            };
        }

        private static string GetAlgorithmName(HashAlgorithmType type)
        {
            return type.ToString();
        }


        /// <summary>
        /// 计算字符串的哈希值（默认 UTF-8 编码）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="algorithm">哈希算法</param>
        /// <param name="encoding">字符编码，默认 UTF8</param>
        /// <returns>小写十六进制哈希字符串</returns>
        public static string ComputeStringHash(string input,HashAlgorithmType algorithm = HashAlgorithmType.SHA256,Encoding? encoding = null)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            encoding ??= Encoding.UTF8;
            byte[] inputBytes = encoding.GetBytes(input);

            using var hashAlg = CreateAlgorithm(algorithm);
            byte[] hashBytes = hashAlg.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes).ToLower();
        }

        /// <summary>
        /// 计算文件哈希（适合中小文件）
        /// </summary>
        public static string ComputeFileHash(string filePath,HashAlgorithmType algorithm = HashAlgorithmType.SHA256)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("文件路径不能为空", nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException("文件未找到", filePath);

            using var hashAlg = CreateAlgorithm(algorithm);
            using var stream = File.OpenRead(filePath);
            byte[] hashBytes = hashAlg.ComputeHash(stream);
            return Convert.ToHexString(hashBytes).ToLower();
        }

        /// <summary>
        /// 异步计算文件哈希（推荐用于大文件）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="algorithm">哈希算法</param>
        /// <param name="bufferSize">读取缓冲区大小（默认 1MB）</param>
        /// <returns>小写十六进制哈希</returns>
        public static async Task<string> ComputeFileHashAsync(string filePath,HashAlgorithmType algorithm = HashAlgorithmType.SHA256,int bufferSize = 1024 * 1024)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("文件路径不能为空", nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException("文件未找到", filePath);
            if (bufferSize <= 0) throw new ArgumentOutOfRangeException(nameof(bufferSize), "缓冲区必须大于0");

            using var hashAlg = CreateAlgorithm(algorithm);
            using var stream = File.OpenRead(filePath);
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                hashAlg.TransformBlock(buffer, 0, bytesRead, buffer, 0);
            }

            hashAlg.TransformFinalBlock(buffer, 0, 0);
            return Convert.ToHexString(hashAlg.Hash!).ToLower();
        }
        

        public static string SHA256(string input) => ComputeStringHash(input, HashAlgorithmType.SHA256);
        public static string MD5(string input) => ComputeStringHash(input, HashAlgorithmType.MD5);
        public static string SHA1(string input) => ComputeStringHash(input, HashAlgorithmType.SHA1);

        public static string FileSHA256(string path) => ComputeFileHash(path, HashAlgorithmType.SHA256);
        public static string FileMD5(string path) => ComputeFileHash(path, HashAlgorithmType.MD5);
        public static Task<string> FileSHA256Async(string path) => ComputeFileHashAsync(path, HashAlgorithmType.SHA256);
    }
}