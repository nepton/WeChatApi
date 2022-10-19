using System.Threading;
using System.Threading.Tasks;
using WeChatApi.MediaPlatform.Users;

namespace WeChatApi.MediaPlatform
{
    public interface IWeChatBlacklistApi
    {
        /// <summary>
        /// 公众号可通过该接口来获取帐号的黑名单列表，黑名单列表由一串 OpenID（加密后的微信号，每个用户对每个公众号的OpenID是唯一的）组成。
        /// 该接口每次调用最多可拉取 10000 个OpenID，当列表数较多时，可以通过多次拉取的方式来满足需求。
        /// 
        /// 当公众号黑名单列表数量超过 10000 时，可通过填写 next_openid 的值，从而多次拉取列表的方式来满足需求。
        /// 具体而言，就是在调用接口时，将上一次调用得到的返回中的 next_openid 的值，作为下一次调用中的 next_openid 值。
        /// </summary>
        /// <remarks>
        /// https://developers.weixin.qq.com/doc/offiaccount/User_Management/Manage_blacklist.html
        /// </remarks>
        /// <param name="clientAccessToken">调用接口凭证</param>
        /// <param name="beginOpenId">当 begin_openid 为空时，默认从开头拉取。</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<OpenIdResultJson> GetBlackList(string clientAccessToken, string beginOpenId, CancellationToken cancel = default);

        /// <summary>
        /// 公众号可通过该接口来拉黑一批用户，黑名单列表由一串 OpenID （加密后的微信号，每个用户对每个公众号的OpenID是唯一的）组成。
        /// </summary>
        /// <remarks>
        /// https://developers.weixin.qq.com/doc/offiaccount/User_Management/Manage_blacklist.html
        /// </remarks>
        /// <param name="clientAccessToken">调用接口凭证</param>
        /// <param name="openIdArray">需要拉入黑名单的用户的openid，一次拉黑最多允许20个</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task AddToBlacklistAsync(string clientAccessToken, string[] openIdArray, CancellationToken cancel = default);

        /// <summary>
        /// 公众号可通过该接口来取消拉黑一批用户，黑名单列表由一串OpenID（加密后的微信号，每个用户对每个公众号的OpenID是唯一的）组成。
        /// </summary>
        /// <remarks>
        /// https://developers.weixin.qq.com/doc/offiaccount/User_Management/Manage_blacklist.html
        /// </remarks>
        /// <param name="clientAccessToken">调用接口凭证</param>
        /// <param name="openIdArray">需要取消拉入黑名单的用户的openid，一次拉黑最多允许20个</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task RemoveFromBlacklistAsync(string clientAccessToken, string[] openIdArray, CancellationToken cancel = default);
    }
}
