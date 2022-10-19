using System;
using System.Threading;
using System.Threading.Tasks;
using WeChatApi.MediaPlatform;
using WeChatApi.RestSharp.Tenpay.Utility;
using WeChatApi.RestSharp.Utility;
using WeChatApi.RestSharp.WeChatBaseUrl;

namespace WeChatApi.RestSharp.MediaPlatform;

/// <summary>
/// 业务系统后端 API 接口
/// </summary>
public class WeChatLoginApi : IWeChatLoginApi
{
    private readonly IWeChatApiCaller        _client;
    private readonly IMediaPlatformServerUri _mediaPlatformServerUri;

    public WeChatLoginApi(IWeChatApiCaller client, IMediaPlatformServerUri mediaPlatformServer)
    {
        _client                 = client;
        _mediaPlatformServerUri = mediaPlatformServer;
    }

    /// <summary>
    /// 获取业务系统访问微信API的Access token 凭证
    /// </summary>
    /// <remarks>
    /// https://developers.weixin.qq.com/doc/offiaccount/Basic_Information/Get_access_token.html
    /// </remarks>
    /// <param name="appId">公众号分配给客户端的 AppId</param>
    /// <param name="secret">客户端的 Secret</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<ClientAccessTokenResponse> GetAccessTokenAsync(string appId, string secret, CancellationToken cancel = default)
    {
        if (string.IsNullOrEmpty(appId))
            throw new ArgumentException("Value cannot be null or empty.", nameof(appId));

        if (string.IsNullOrEmpty(secret))
            throw new ArgumentException("Value cannot be null or empty.", nameof(secret));

        appId  = appId.TryEscapeDataStringOrNull();
        secret = secret.TryEscapeDataStringOrNull();

        var uri = _mediaPlatformServerUri.GetUri($"/cgi-bin/token?grant_type=client_credential&appid={appId}&secret={secret}");
        return await _client.GetAsJsonAsync<ClientAccessTokenResponse>(uri, cancel);
    }

    /// <summary>
    /// 获取微信服务器的ip段
    /// </summary>
    /// <remarks>
    /// https://developers.weixin.qq.com/doc/offiaccount/Basic_Information/Get_the_WeChat_server_IP_address.html
    /// </remarks>
    /// <param name="clientAccessToken">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<WeChatMessageServerAddressResponse> GetMessageServerAddressAsync(string clientAccessToken, CancellationToken cancel = default)
    {
        if (string.IsNullOrEmpty(clientAccessToken))
            throw new ArgumentException("Value cannot be null or empty.", nameof(clientAccessToken));

        var uri = _mediaPlatformServerUri.GetUri($"/cgi-bin/getcallbackip?access_token={clientAccessToken}");
        return await _client.GetAsJsonAsync<WeChatMessageServerAddressResponse>(uri, cancel);
    }

    /// <summary>
    /// 把所有公众号调用或第三方平台调用的次数进行
    /// </summary>
    /// <remarks>
    /// https://developers.weixin.qq.com/doc/offiaccount/Message_Management/API_Call_Limits.html
    /// </remarks>
    /// <param name="clientAccessToken">客户端的 AccessToken</param>
    /// <param name="appId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task ClearQuotaAsync(string clientAccessToken, string appId, CancellationToken cancel = default)
    {
        var path = $"/cgi-bin/clear_quota?access_token={clientAccessToken}";
        var uri  = _mediaPlatformServerUri.GetUri(path);
        var body = new
        {
            appid = appId
        };

        // 执行请求
        await _client.PostAsJsonAsync(uri, body, cancel);
    }
}