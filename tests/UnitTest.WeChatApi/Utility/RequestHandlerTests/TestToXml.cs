using System.Xml;
using WeChatApi.RestSharp.Tenpay.Utility;

namespace UnitTest.WeChatApi.Utility.RequestHandlerTests;

public class TestToXml
{
    [Fact]
    public void TestConvertToXml()
    {
        var requestHandler = new RequestBuilder();
        requestHandler.SetParameter("appid",       "wx2421b1c4370ec43b");
        requestHandler.SetParameter("mch_id",      "10000100");
        requestHandler.SetParameter("device_info", "1000");
        requestHandler.SetParameter("body",        "test");

        var xml = requestHandler.ToXml();
        var doc = new XmlDocument();
        doc.LoadXml(xml);
        Assert.Equal("wx2421b1c4370ec43b", doc.DocumentElement.SelectSingleNode("appid").InnerText);
        Assert.Equal("10000100",           doc.DocumentElement.SelectSingleNode("mch_id").InnerText);
        Assert.Equal("1000",               doc.DocumentElement.SelectSingleNode("device_info").InnerText);
        Assert.Equal("test",               doc.DocumentElement.SelectSingleNode("body").InnerText);
    }
}
