using System.IO;
using Newtonsoft.Json;

// JsonLib
// Json操作
// Version: 2025-9-14 10:59

namespace JsonLib
{
    public class JsonHelper
    {
        // / <summary>
        // / 从JSON文件读取并反序列化为对象
        // / </summary>
        public static T ReadJson<T>(string content)
        {
            JsonConvert.DeserializeObject<xxx>(jsonContent);
        }

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
    }
}