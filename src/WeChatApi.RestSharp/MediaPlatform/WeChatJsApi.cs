using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeChatApi.MediaPlatform;
using WeChatApi.RestSharp.Tenpay.Utility;
using WeChatApi.RestSharp.Utility;
using WeChatApi.RestSharp.WeChatBaseUrl;

namespace WeChatApi.RestSharp.MediaPlatform;

public class WeChatJsApi : IWeChatJsApi
{
    private readonly IWeChatApiCaller        _weChatClient;
    private readonly IMediaPlatformServerUri _weChatApiServer;
    private readonly ILogger<WeChatJsApi>          _logger;

    public WeChatJsApi(IMediaPlatformServerUri weChatApiServer, IWeChatApiCaller weChatClient, ILogger<WeChatJsApi> logger)
    {
        _weChatApiServer = weChatApiServer;
        _weChatClient    = weChatClient;
        _logger          = logger;
    }

    /// <summary>
    /// 获取JSAPI接口的ticket
    /// </summary>
    /// <param name="accessToken">公众号的全局唯一票据</param>
    /// <param name="cancel"></param>
    /// <returns>JSAPI接口的ticket</returns>
    public async Task<JsTicket> GetTicketAsync(string accessToken, CancellationToken cancel = default)
    {
        var url = $"/cgi-bin/ticket/getticket?access_token={accessToken}&type=jsapi";
        return await _weChatClient.GetAsJsonAsync<JsTicket>(_weChatApiServer.GetUri(url), cancel);
    }

    /// <summary>
    /// 签名
    /// </summary>
    /// <param name="ticket"></param>
    /// <param name="url"></param>
    /// <param name="nonceStr"></param>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    public string GetSignature(string ticket, string url, string nonceStr, string timestamp)
    {
        var stringToSign = $"jsapi_ticket={ticket}&noncestr={nonceStr}&timestamp={timestamp}&url={url}";
        var signature    = EncryptHelper.GetSha1(stringToSign);
        _logger.LogDebug("Use SHA signature ticket={Ticket} Url={Url} NonceStr={NonceStr} Timestamp={Timestamp} <{Text}> is <{Signature}>",
            ticket,
            url,
            nonceStr,
            timestamp,
            stringToSign,
            signature);

        return signature;
    }
}