using System;
using System.Threading;
using System.Threading.Tasks;
using WeChatApi.MediaPlatform;

namespace WeChatApi.OpenConnect
{
    public interface IWeChatOpenConnectApi
    {
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
        Uri GetAuthorizeUrl(string appId, string redirectUrl, string state, WeChatUserOAuthScopes scope);

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
        Task<QrConnectAccessTokenResult> GetAccessTokenAsync(string appId, string secret, string authorizationCode, CancellationToken cancel = default);

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
        Task<RefreshAccessTokenResult> RefreshTokenAsync(string appId, string refreshToken, CancellationToken cancel = default);

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
        Task<QRConnectUserInfo> GetUserInfoAsync(string userAccessToken, string openId, Language language, CancellationToken cancel = default);

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
        Task<bool> IsAccessTokenEffectiveAsync(string userAccessToken, string openId, CancellationToken cancel = default);
    }
}
