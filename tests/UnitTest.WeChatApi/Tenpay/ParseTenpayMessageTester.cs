using WeChatApi.RestSharp.Tenpay.Utility;
using WeChatApi.Tenpay;

namespace UnitTest.WeChatApi.Tenpay;

public class ParseTenpayMessageTester
{
    /// <summary>
    /// 测试解析微信支付通知消息
    /// </summary>
    [Fact]
    public void TestParsePaidCallback()
    {
        var callbackMessage = @"<xml><appid><![CDATA[wx16edbb21f2043f70]]></appid>
            <bank_type><![CDATA[GDB_CREDIT]]></bank_type>
            <cash_fee><![CDATA[51]]></cash_fee>
            <fee_type><![CDATA[CNY]]></fee_type>
            <is_subscribe><![CDATA[Y]]></is_subscribe>
            <mch_id><![CDATA[1505546241]]></mch_id>
            <nonce_str><![CDATA[A1575749C24310F7FCF81DDCB418366A]]></nonce_str>
            <openid><![CDATA[oOF60w2-wRQP9xzPJ9GuTaAk0f9I]]></openid>
            <out_trade_no><![CDATA[d59e3f86a4094ecfb4c3014705f16d0c]]></out_trade_no>
            <result_code><![CDATA[SUCCESS]]></result_code>
            <return_code><![CDATA[SUCCESS]]></return_code>
            <sign><![CDATA[F35B1D353002F4A27B28FFAA12D16AB2]]></sign>
            <time_end><![CDATA[20221015111734]]></time_end>
            <total_fee>51</total_fee>
            <trade_type><![CDATA[JSAPI]]></trade_type>
            <transaction_id><![CDATA[4200001589202210152760945029]]></transaction_id>
            </xml>";

        var message = XmlConverter.Deserialize<TenpayPaymentCallbackMessage>(callbackMessage);

        Assert.Equal("wx16edbb21f2043f70",               message.AppId);
        Assert.Equal("GDB_CREDIT",                       message.BankType);
        Assert.Equal(51,                                 message.CashFee);
        Assert.Equal("CNY",                              message.FeeType);
        Assert.Equal("Y",                                message.IsSubscribe);
        Assert.Equal("1505546241",                       message.MerchantId);
        Assert.Equal("A1575749C24310F7FCF81DDCB418366A", message.NonceStr);
        Assert.Equal("oOF60w2-wRQP9xzPJ9GuTaAk0f9I",     message.OpenId);
        Assert.Equal("d59e3f86a4094ecfb4c3014705f16d0c", message.OutTradeNo);
        Assert.Equal("SUCCESS",                          message.ResultCode);
        Assert.Equal("SUCCESS",                          message.ReturnCode);
    }

    [Fact]
    public void TestPlaceOrderResponse()
    {
        var response = @"<xml><return_code><![CDATA[SUCCESS]]></return_code>
            <return_msg><![CDATA[OK]]></return_msg>
            <result_code><![CDATA[SUCCESS]]></result_code>
            <mch_id><![CDATA[1505546241]]></mch_id>
            <appid><![CDATA[wx16edbb21f2043f70]]></appid>
            <nonce_str><![CDATA[mEumpbMKRWjoNvht]]></nonce_str>
            <sign><![CDATA[482383CD6106827E5C61BC48B0ABB9D4]]></sign>
            <prepay_id><![CDATA[wx1810274334130380556e69a4866d540000]]></prepay_id>
            <trade_type><![CDATA[JSAPI]]></trade_type>
            </xml>";

        var message = XmlConverter.Deserialize<TenpayPlaceOrderResponse>(response);

        Assert.Equal("SUCCESS",                              message.ReturnCode);
        Assert.Equal("OK",                                   message.ReturnMessage);
        Assert.Equal("SUCCESS",                              message.ResultCode);
        Assert.Equal("1505546241",                           message.MerchantId);
        Assert.Equal("wx16edbb21f2043f70",                   message.AppId);
        Assert.Equal("mEumpbMKRWjoNvht",                     message.NonceStr);
        Assert.Equal("482383CD6106827E5C61BC48B0ABB9D4",     message.Sign);
        Assert.Equal("wx1810274334130380556e69a4866d540000", message.PrepayId);
        Assert.Equal("JSAPI",                                message.TradeType);
    }
}
