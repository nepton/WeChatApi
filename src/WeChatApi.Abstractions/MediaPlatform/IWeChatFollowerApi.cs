using System.Threading;
using System.Threading.Tasks;
using WeChatApi.MediaPlatform.Users;

namespace WeChatApi.MediaPlatform
{
    /// <summary>
    /// 关注公众号的用户的访问接口
    /// </summary>
    public interface IWeChatFollowerApi
    {
        /// <summary>
        /// 获取关注者OpenId信息 .
        /// 一次拉取调用最多拉取10000个关注者的OpenID，可通过填写next_openid的值，从而多次拉取列表的方式来满足需求。
        /// </summary>
        /// <remarks>
        /// https://developers.weixin.qq.com/doc/offiaccount/User_Management/Getting_a_User_List.html
        /// </remarks>
        /// <param name="clientAccessToken">调用接口凭证</param>
        /// <param name="nextOpenId">要拉取的OPENID的起始点，不填默认从头开始拉取</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<OpenIdResultJson> GetOpenIdArrayAsync(string clientAccessToken, string nextOpenId, CancellationToken cancel = default);

        /// <summary>
        /// 获取关注公众号用户的基本信息
        /// 在关注者与公众号产生消息交互后，公众号可获得关注者的OpenID
        /// 公众号可通过本接口来根据OpenID获取用户基本信息，包括昵称、头像、性别、所在城市、语言和关注时间。
        /// </summary>
        /// <remarks>
        /// https://developers.weixin.qq.com/doc/offiaccount/User_Management/Get_users_basic_information_UnionID.html#UinonId
        /// </remarks>
        /// <param name="clientAccessToken">调用接口凭证</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="language">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<WeChatUserInfo> GetUserInfoAsync(string clientAccessToken, string openId, Language language, CancellationToken cancel = default);

        /// <summary>
        /// 开发者可通过该接口来批量获取用户基本信息。最多支持一次拉取100条。
        /// </summary>
        /// <remarks>
        /// https://developers.weixin.qq.com/doc/offiaccount/User_Management/Get_users_basic_information_UnionID.html
        /// </remarks>
        /// <param name="clientAccessToken">调用接口凭证</param>
        /// <param name="openIds">用户标识数组</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<BatchGetUserInfoJsonResult> GetUserInfoArrayAsync(string clientAccessToken, BatchGetUserInfoData[] openIds, CancellationToken cancel = default);

        /// <summary>
        /// 修改关注者备注信息
        /// </summary>
        /// <param name="clientAccessToken">调用接口凭证</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="newRemark">新的备注名，长度必须小于30字符</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<WeChatResponse> UpdateRemarkAsync(string clientAccessToken, string openId, string newRemark, CancellationToken cancel = default);
    }
}
