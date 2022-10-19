using WeChatApi.RestSharp.Tenpay.Utility;

namespace UnitTest.WeChatApi.Utility.RequestHandlerTests;

public class SignWithMd5Tester
{
    [Fact]
    public void TestSignWithMd5()
    {
        var requestHandler = new RequestBuilder();
        requestHandler.SetParameter("appid",  "wx2421b1c4370ec43b");
        requestHandler.SetParameter("attach", "支付测试");
        requestHandler.SetParameter("body",   "JSAPI支付测试");
        requestHandler.SetParameter("mch_id", "10000100");
        requestHandler.SetParameter("nonce_str", "1add1a30ac87aa2db72f57a2375d8fec");
        requestHandler.SetParameter("out_trade_no", "1415659990");
        var sign = requestHandler.SignByMd5("KEY_TEXT");
        Assert.Equal("4F3117BC6FA2A6A86D50B56B0775A190", sign);
    }
}
