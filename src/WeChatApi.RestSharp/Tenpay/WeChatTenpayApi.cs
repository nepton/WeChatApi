using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeChatApi.RestSharp.Tenpay.Utility;
using WeChatApi.RestSharp.WeChatBaseUrl;
using WeChatApi.Tenpay;

// ReSharper disable StringLiteralTypo

namespace WeChatApi.RestSharp.Tenpay;

/// <summary>
/// 微信支付API
/// </summary>
public class WeChatTenpayApi : IWeChatTenpayApi
{
    private readonly ITenpayServerUri         _tenpayServer;
    private readonly ILogger<WeChatTenpayApi> _logger;

    public WeChatTenpayApi(
        ITenpayServerUri         tenPayServer,
        ILogger<WeChatTenpayApi> logger
    )
    {
        _tenpayServer = tenPayServer;
        _logger       = logger;
    }

    /// <summary>
    /// 统一支付接口
    /// 统一支付接口，可接受JSAPI/NATIVE/APP 下预支付订单，返回预支付订单号。NATIVE 支付返回二维码code_url。
    /// </summary>
    /// <remarks>
    /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1
    /// </remarks>
    /// <param name="request">微信支付需要post的Data数据</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<TenpayPlaceOrderResponse> CreateOrderAsync(TenpayPlaceOrderRequest request, CancellationToken cancel = default)
    {
        //创建支付应答对象
        var requestBuilder = new RequestBuilder();

        //设置package订单参数
        //以下设置顺序按照官方文档排序，方便维护：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1

        requestBuilder.SetParameter("version", "1.0");
        requestBuilder.SetParameter("appid",   request.AppId);                                                 //公众账号ID
        requestBuilder.SetParameter("mch_id",  request.MchId);                                                 //商户号
        requestBuilder.SetParameterWhenNotNull("device_info", request.DeviceInfo);                             //自定义参数
        requestBuilder.SetParameter("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));                  //随机字符串
        requestBuilder.SetParameterWhenNotNull("sign_type", "MD5");                                            //签名类型，默认为MD5
        requestBuilder.SetParameter("body", request.Body);                                                     //商品信息
        requestBuilder.SetParameterWhenNotNull("detail", request.Detail);                                      //商品详细列表
        requestBuilder.SetParameterWhenNotNull("attach", request.Attach);                                      //附加数据
        requestBuilder.SetParameter("out_trade_no", request.OutTradeNo);                                       //商家订单号
        requestBuilder.SetParameterWhenNotNull("fee_type", request.FeeType);                                   //符合ISO 4217标准的三位字母代码，默认人民币：CNY
        requestBuilder.SetParameter("total_fee",        request.TotalFee.ToString());                          //商品金额,以分为单位(money * 100).ToString()
        requestBuilder.SetParameter("spbill_create_ip", request.BillCreateIp);                                 //用户的公网ip，不是商户服务器IP
        requestBuilder.SetParameterWhenNotNull("time_start",  request.TimeStart?.ToString("yyyyMMddHHmmss"));  //订单生成时间
        requestBuilder.SetParameterWhenNotNull("time_expire", request.TimeExpire?.ToString("yyyyMMddHHmmss")); //订单失效时间
        requestBuilder.SetParameterWhenNotNull("goods_tag",   request.GoodsTag);                               //商品标记
        requestBuilder.SetParameter("notify_url", request.NotifyUrl);                                          //接收财付通通知的URL
        requestBuilder.SetParameter("trade_type", request.TradeType.ToString());                               //交易类型
        requestBuilder.SetParameterWhenNotNull("product_id",     request.ProductId);                           //trade_type=NATIVE时（即扫码支付），此参数必传。
        requestBuilder.SetParameterWhenNotNull("limit_pay",      request.LimitPay ? "no_credit" : null);       //上传此参数no_credit--可限制用户不能使用信用卡支付
        requestBuilder.SetParameterWhenNotNull("openid",         request.OpenId);                              //用户的openId，trade_type=JSAPI时（即公众号支付），此参数必传
        requestBuilder.SetParameterWhenNotNull("profit_sharing", request.ProfitSharing);                       //是否需要分账标识

        // 生成签名
        requestBuilder.SignByMd5(request.Key);

        var httpClient = new HttpClient();
        var uri        = _tenpayServer.GetUri("pay/unifiedorder");
        var data       = requestBuilder.ToXml();
        var rsp        = await httpClient.PostAsync(uri, new StringContent(data), cancel);
        var xml        = await rsp.Content.ReadAsStringAsync();

        _logger.LogInformation("微信支付生成订单, 发送 {RequestXml} 并接到返回 {ResponseXml}", data, xml);

        var result = XmlConverter.Deserialize<TenpayPlaceOrderResponse>(xml);
        result.Xml = xml;
        if (!result.IsReturnCodeSuccess)
        {
            _logger.LogWarning("Invoke wechat API CreateOrderAsync failed: {ErrorMessage} {$TenpayRequest} {$TenpayResult}", result.ReturnMessage, data, xml);
            throw new TenpayException(result.ReturnCode, result.ReturnMessage);
        }

        return result;
    }

    /// <summary>
    /// 订单查询接口
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<TenpayOrderQueryResponse> GetOrderAsync(TenpayOrderQueryRequest request, CancellationToken cancel = default)
    {
        var requestBuilder = new RequestBuilder();

        //设置package订单参数
        requestBuilder.SetParameter("appid",          request.AppId);                              //公众账号ID
        requestBuilder.SetParameter("mch_id",         request.MchId);                              //商户号
        requestBuilder.SetParameter("transaction_id", request.TransactionId ?? "");                //微信的订单号
        requestBuilder.SetParameter("out_trade_no",   request.OutTradeNo ?? "");                   //商户系统内部的订单号
        requestBuilder.SetParameter("nonce_str",      Guid.NewGuid().ToString().Replace("-", "")); //随机字符串
        requestBuilder.SetParameter("sign_type",      "MD5");                                      //签名类型

        //签名
        requestBuilder.SignByMd5(request.Key);

        var httpClient = new HttpClient();
        var uri        = _tenpayServer.GetUri("pay/orderquery");
        var data       = requestBuilder.ToXml();
        var rsp        = await httpClient.PostAsync(uri, new StringContent(data), cancel);
        var xml        = await rsp.Content.ReadAsStringAsync();

        _logger.LogInformation("微信支付订单查询, 发送 {RequestXml} 并接到返回 {ResponseXml}", data, xml);

        var result = XmlConverter.Deserialize<TenpayOrderQueryResponse>(xml);
        result.Xml = xml;

        if (!result.IsReturnCodeSuccess)
        {
            _logger.LogWarning("Invoke wechat API OrderQuery failed: {ErrorMessage} {$TenpayRequest} {$TenpayResult}", result.ReturnMessage, data, xml);
            throw new TenpayException(result.ReturnCode, result.ReturnMessage);
        }

        return result;
    }
}
