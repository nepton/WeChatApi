using System.Threading;
using System.Threading.Tasks;

namespace WeChatApi.MediaPlatform
{
    /// <summary>
    /// 客户端的API
    /// </summary>
    public interface IWeChatLoginApi
    {
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
        Task<ClientAccessTokenResponse> GetAccessTokenAsync(string appId, string secret, CancellationToken cancel = default);

        /// <summary>
        /// 获取微信服务器的ip段
        /// </summary>
        /// <remarks>
        /// https://developers.weixin.qq.com/doc/offiaccount/Basic_Information/Get_the_WeChat_server_IP_address.html
        /// </remarks>
        /// <param name="accessToken">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<WeChatMessageServerAddressResponse> GetMessageServerAddressAsync(string accessToken, CancellationToken cancel = default);

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
        Task ClearQuotaAsync(string clientAccessToken, string appId, CancellationToken cancel = default);
    }
}
