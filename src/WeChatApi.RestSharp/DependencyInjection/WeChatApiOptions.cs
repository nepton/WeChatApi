namespace WeChatApi.RestSharp;

public class WeChatApiOptions
{
    /// <summary>
    /// 公众号的 App Id
    /// </summary>
    public string AppId { get; set; }

    /// <summary>
    /// 公众号的 App Secret
    /// </summary>
    public string AppSecret { get; set; }

    /// <summary>
    /// 微信支付商户Id
    /// </summary>
    public string TenpayMerchantId { get; set; }

    /// <summary>
    /// 微信支付API Key
    /// </summary>
    public string TenpayApiKey { get; set; }

    /// <summary>
    /// 微信支付回调地址
    /// </summary>
    public string TenpayCallbackUrl { get; set; }

    /// <summary>
    /// 微信支付的沙箱模式
    /// </summary>
    public bool TenpaySandboxMode { get; set; }
}