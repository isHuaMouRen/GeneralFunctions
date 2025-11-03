using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace HuaZi.Library.Xml
{
    public class Xml
    {
        /// <summary>
        /// 读Xml文件或Xml文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentOrPath">传入xml文本或xml文件路径</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T ReadXml<T>(string contentOrPath)
        {
            string xmlText;

            // 判断是否是文件
            if (File.Exists(contentOrPath))
                xmlText = File.ReadAllText(contentOrPath);
            else
                xmlText = contentOrPath;

            if (string.IsNullOrWhiteSpace(xmlText))
                throw new Exception("XML 内容为空");

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringReader reader = new StringReader(xmlText))
            {
                object result = serializer.Deserialize(reader)!;
                return (T)result;
            }
        }

        /// <summary>
        /// 写入Xml到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">Xml内容</param>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string WriteXml<T>(T obj, string path = null!)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, obj, ns);
                string xml = sw.ToString();

                if (!string.IsNullOrEmpty(path))
                    File.WriteAllText(path, xml, Encoding.UTF8);

                return xml;
            }
        }
    }
}
