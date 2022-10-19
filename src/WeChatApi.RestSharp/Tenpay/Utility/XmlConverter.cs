using System;
using System.Xml;
using System.Xml.Serialization;

namespace WeChatApi.RestSharp.Tenpay.Utility;

/// <summary>
/// XML Convert
/// </summary>
public static class XmlConverter
{
    /// <summary>
    /// 解析xml字符串, 转换为对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static T Deserialize<T>(string xml)
    {
        if (xml == null)
            throw new ArgumentNullException(nameof(xml));

        var doc = new XmlDocument();
        doc.LoadXml(xml);

        return Deserialize<T>(doc);
    }

    /// <summary>
    /// 通过 XML Document 解析至对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="doc"></param>
    /// <returns></returns>
    public static T Deserialize<T>(XmlDocument doc)
    {
        var root       = new XmlRootAttribute(doc.DocumentElement?.Name ?? "");
        var serializer = new XmlSerializer(typeof(T), root);

        return (T) serializer.Deserialize(new XmlNodeReader(doc));
    }
}
