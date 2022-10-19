using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeChatApi.RestSharp.Tenpay.Utility;
using WeChatApi.Tenpay;

namespace WeChatApi.RestSharp.Tenpay;

/// <summary>
/// 腾讯支付服务
/// https://mp.qpay.tenpay.cn/buss/wiki/1/1123
/// </summary>
public class WeChatTenpayService : IWeChatTenpayClient
{
    private readonly IWeChatTenpayApi             _tenpayApi;
    private readonly WeChatApiOptions       _configuration;
    private readonly ILogger<WeChatTenpayService> _logger;

    public WeChatTenpayService(
        IWeChatTenpayApi                         tenpayApi,
        IOptionsSnapshot<WeChatApiOptions> configuration,
        ILogger<WeChatTenpayService>             logger)
    {
        _tenpayApi     = tenpayApi;
        _configuration = configuration.Value;
        _logger        = logger;
    }

    /// <summary>
    /// 创建预付款订单
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<string> CreateTransactionAsync(JsApiPayModel model, CancellationToken cancel = default)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        // 构建请求
        var data = new TenpayPlaceOrderRequest(
            _configuration.AppId,
            _configuration.TenpayMerchantId,
            model.Description,
            model.OrderNumber,
            (int) Math.Round(model.Payable * 100),
            "127.0.0.1",
            _configuration.TenpayCallbackUrl,
            TenpayPaymentType.JSAPI,
            model.PayerOpenId,
            _configuration.TenpayApiKey)
        {
            TimeExpire = model.ExpireTime,
        };

        // 发送请求创建预订单
        var result = await _tenpayApi.CreateOrderAsync(data, cancel);

        // 处理回应的错误
        if (!result.IsResultCodeSuccess)
        {
            _logger.LogWarning(
                "微信支付下单失败: {ErrorMessage} {$TenpayRequest} {$TenpayResponse}",
                result.ErrorMessage,
                data,
                result.Xml);
            throw new TenpayException(result.ErrorCode, result.ErrorMessage);
        }

        // 返回预订单
        return result.PrepayId;
    }

    /// <summary>
    /// 查询微信付款单
    /// </summary>
    /// <param name="tradeNumber">订单号</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public async Task<TenpayOrder> GetPaymentOrderAsync(string tradeNumber, CancellationToken cancel = default)
    {
        var query = new TenpayOrderQueryRequest(
            _configuration.AppId,
            _configuration.TenpayMerchantId,
            null,
            tradeNumber,
            _configuration.TenpayApiKey);

        var result = await _tenpayApi.GetOrderAsync(query, cancel);

        if (!result.IsResultCodeSuccess)
        {
            if (result.ErrorCode == "ORDERNOTEXIST")
                return null;

            _logger.LogWarning(
                "查询订单 {OrderNumber} 失败: {ErrorMessage} {$TenpayRequest} {$TenpayResponse}",
                tradeNumber,
                result.ErrorMessage,
                query,
                result.Xml);

            throw new TenpayException(result.ErrorCode, result.ErrorMessage);
        }

        return new TenpayOrder
        {
            TradeType = result.TradeType,
            State = result.TradeState switch
            {
                "SUCCESS"    => TenpayOrderState.Success,
                "REFUND"     => TenpayOrderState.Refund,
                "NOTPAY"     => TenpayOrderState.NotPay,
                "CLOSED"     => TenpayOrderState.Closed,
                "REVOKED"    => TenpayOrderState.Revoked,
                "USERPAYING" => TenpayOrderState.UserPaying,
                "PAYERROR"   => TenpayOrderState.PayError,
                _            => TenpayOrderState.Others,
            },
            Amount        = int.Parse(result.TotalFee) / 100m,
            BankType      = result.BankType,
            TransactionId = result.TransactionId,
            OrderNumber   = result.OutTradeNo,
            PaymentTime   = DateTime.TryParse(result.TimeEnd, out var time) ? time : default,
            Description   = result.TradeStateDesc,
            OpenId        = result.OpenId,
        };
    }

    /// <summary>
    /// 创建客户端 JsApi 调用微信付款功能的参数对象
    /// </summary>
    /// <param name="prepayId">预付款订单</param>
    /// <returns></returns>
    public string SignForJsApiPackage(string prepayId)
    {
        if (prepayId == null)
            throw new ArgumentNullException(nameof(prepayId));

        // 创建保对象
        dynamic package = new ExpandoObject();
        package.appId     = _configuration.AppId;
        package.nonceStr  = RandomHelper.GetRandomString(16);
        package.package   = $"prepay_id={prepayId}";
        package.signType  = "MD5";
        package.timeStamp = JavaScriptDateTimeUtils.ConvertDateTimeToJavaScriptTicks(DateTime.Now).ToString();

        // 签名
        var dict = new SortedDictionary<string, object>(package).ToList();
        dict.Add(new KeyValuePair<string, object>("key", _configuration.TenpayApiKey));

        var signText = string.Join("&", dict.Select(item => $"{item.Key}={item.Value}"));
        var signStr  = EncryptHelper.GetMd5(signText).ToUpper();
        package.paySign = signStr;

        return JsonConvert.SerializeObject(package);
    }
}
