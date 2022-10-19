using System;
using System.Threading;
using System.Threading.Tasks;
using WeChatApi.MediaPlatform;
using WeChatApi.OpenConnect;
using WeChatApi.RestSharp.Tenpay.Utility;
using WeChatApi.RestSharp.Utility;
using WeChatApi.RestSharp.WeChatBaseUrl;

namespace WeChatApi.RestSharp.OpenConnect;

/// <summary>
/// OAuthApi implement
/// </summary>
/// <remarks>
/// </remarks>
public class WeChatOpenConnectApi : IWeChatOpenConnectApi
{
    private readonly IMediaPlatformServerUri _mediaPlatformServer;
    private readonly IOpenConnectServerUri   _openConnectServer;
    private readonly IWeChatApiCaller        _weChatClient;

    public WeChatOpenConnectApi(
        IOpenConnectServerUri   weChatOpenServer,
        IMediaPlatformServerUri weChatApiServer,
        IWeChatApiCaller           weChatClient)
    {
        _openConnectServer   = weChatOpenServer;
        _mediaPlatformServer = weChatApiServer;
        _weChatClient        = weChatClient;
    }

    /// <summary>
    /// 获取验证地址
    /// </summary>
    /// <remarks>
    /// https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorization.html
    /// </remarks>
    /// <param name="appId">公众号的唯一标识</param>
    /// <param name="redirectUrl">授权后重定向的回调链接地址</param>
    /// <param name="state">重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值，最多128字节</param>
    /// <param name="scope">请求用户授权范围</param>
    /// <returns></returns>
    public Uri GetAuthorizeUrl(string appId, string redirectUrl, string state, WeChatUserOAuthScopes scope)
    {
        redirectUrl = redirectUrl.TryEscapeDataStringOrNull();
        state       = state.TryEscapeDataStringOrNull();

        var responseType = "code";
        var scopeText = scope switch
        {
            WeChatUserOAuthScopes.OpenId   => "snsapi_base",
            WeChatUserOAuthScopes.UserInfo => "snsapi_userinfo",
            _                              => throw new NotSupportedException(),
        };

        // 这一步发送之后，客户会得到授权页面，无论同意或拒绝，都会返回redirectUrl页面。
        // 如果用户同意授权，页面将跳转至 redirect_uri/?code=CODE&state=STATE。这里的code用于换取access_token（和通用接口的access_token不通用）
        // 若用户禁止授权，则重定向后不会带上code参数，仅会带上state参数redirect_uri?state=STATE

        // TODO 据说加上 &connect_redirect=1 可以解决40029-invalid code的问题, 目前没加, 遇到问题再说
        var path = $"/connect/oauth2/authorize?appid={appId}&redirect_uri={redirectUrl}&response_type={responseType}&scope={scopeText}&state={state}#wechat_redirect";
        var uri  = _openConnectServer.GetUri(path);
        return uri;
    }

    /// <summary>
    /// 通过微信用户的 Authorization code 获取该用户的 AccessToken
    /// </summary>
    /// <remarks>
    /// https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorization.html
    /// </remarks>
    /// <param name="appId">公众号的唯一标识</param>
    /// <param name="secret">公众号的appsecret</param>
    /// <param name="authorizationCode">填写第一步获取的code参数</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<QrConnectAccessTokenResult> GetAccessTokenAsync(string appId, string secret, string authorizationCode, CancellationToken cancel = default)
    {
        var grantType = "authorization_code";
        var path      = $"/sns/oauth2/access_token?appid={appId}&secret={secret}&code={authorizationCode}&grant_type={grantType}";
        var uri       = _mediaPlatformServer.GetUri(path);

        return await _weChatClient.GetAsJsonAsync<QrConnectAccessTokenResult>(uri, cancel);
    }

    /// <summary>
    /// 刷新 微信用户的 access_token
    /// </summary>
    /// <remarks>
    /// https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorization.html
    /// </remarks>
    /// <param name="appId">公众号的唯一标识</param>
    /// <param name="refreshToken">填写通过access_token获取到的refresh_token参数</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<RefreshAccessTokenResult> RefreshTokenAsync(string appId, string refreshToken, CancellationToken cancel = default)
    {
        var grantType = "refresh_token";
        var path      = $"/sns/oauth2/refresh_token?appid={appId}&grant_type={grantType}&refresh_token={refreshToken}";

        return await _weChatClient.GetAsJsonAsync<RefreshAccessTokenResult>(_mediaPlatformServer.GetUri(path), cancel);
    }

    /// <summary>
    /// 如果网页授权作用域为snsapi_userinfo，则此时开发者可以通过access_token和openid拉取用户信息了。
    /// 这个方法是访问微信用户授权的用户信息
    /// </summary>
    /// <remarks>
    /// https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorization.html
    /// </remarks>
    /// <param name="userAccessToken">网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同</param>
    /// <param name="openId">微信用户的标识</param>
    /// <param name="language">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<QRConnectUserInfo> GetUserInfoAsync(string userAccessToken, string openId, Language language, CancellationToken cancel = default)
    {
        var path = $"/sns/userinfo?access_token={userAccessToken}&openid={openId}&lang={language}";
        return await _weChatClient.GetAsJsonAsync<QRConnectUserInfo>(_mediaPlatformServer.GetUri(path), cancel);
    }

    /// <summary>
    /// 检验授权凭证（access_token）是否有效
    /// </summary>
    /// <remarks>
    /// https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorization.html
    /// </remarks>
    /// <param name="userAccessToken">网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同</param>
    /// <param name="openId">用户的唯一标识</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<bool> IsAccessTokenEffectiveAsync(string userAccessToken, string openId, CancellationToken cancel = default)
    {
        var path = $"/sns/auth?access_token={userAccessToken}&openid={openId}";
        var rsp  = await _weChatClient.GetAsJsonAsync<WeChatResponse>(_mediaPlatformServer.GetUri(path), cancel);

        return rsp.Successful;
    }
}