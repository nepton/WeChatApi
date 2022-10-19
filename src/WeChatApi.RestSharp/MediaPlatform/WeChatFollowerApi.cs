using System.Threading;
using System.Threading.Tasks;
using WeChatApi.MediaPlatform;
using WeChatApi.MediaPlatform.Users;
using WeChatApi.RestSharp.Utility;
using WeChatApi.RestSharp.WeChatBaseUrl;

namespace WeChatApi.RestSharp.MediaPlatform;

/// <summary>
/// 微信公众号关注者 API
/// </summary>
public class WeChatFollowerApi : IWeChatFollowerApi
{
    private readonly IWeChatApiCaller        _weChatClient;
    private readonly IMediaPlatformServerUri _weChatApiServer;

    public WeChatFollowerApi(IMediaPlatformServerUri weChatApiServer, IWeChatApiCaller weChatClient)
    {
        _weChatApiServer = weChatApiServer;
        _weChatClient    = weChatClient;
    }

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
    public async Task<OpenIdResultJson> GetOpenIdArrayAsync(string clientAccessToken, string nextOpenId, CancellationToken cancel = default)
    {
        var url = $"/cgi-bin/user/get?access_token={clientAccessToken}";
        if (!string.IsNullOrEmpty(nextOpenId))
        {
            url += "&next_openid=" + nextOpenId;
        }

        return await _weChatClient.GetAsJsonAsync<OpenIdResultJson>(_weChatApiServer.GetUri(url), cancel);
    }

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
    public async Task<WeChatUserInfo> GetUserInfoAsync(string clientAccessToken, string openId, Language language, CancellationToken cancel = default)
    {
        var url = $"/cgi-bin/user/info?access_token={clientAccessToken}&openid={openId}&lang={language}";
        return await _weChatClient.GetAsJsonAsync<WeChatUserInfo>(_weChatApiServer.GetUri(url), cancel);
    }

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
    public async Task<BatchGetUserInfoJsonResult> GetUserInfoArrayAsync(string clientAccessToken, BatchGetUserInfoData[] openIds, CancellationToken cancel = default)
    {
        var data = new
        {
            user_list = openIds,
        };
        var uri = _weChatApiServer.GetUri($"/cgi-bin/user/info/batchget?access_token={clientAccessToken}");
        return await _weChatClient.PostAsJsonAsync<BatchGetUserInfoJsonResult>(uri, data, cancel);
    }

    /// <summary>
    /// 修改关注者备注信息
    /// </summary>
    /// <param name="clientAccessToken">调用接口凭证</param>
    /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
    /// <param name="newRemark">新的备注名，长度必须小于30字符</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<WeChatResponse> UpdateRemarkAsync(string clientAccessToken, string openId, string newRemark, CancellationToken cancel = default)
    {
        var path = $"/cgi-bin/user/info/updateremark?access_token={clientAccessToken}";
        var data = new
        {
            openid = openId,
            remark = newRemark,
        };

        var uri = _weChatApiServer.GetUri(path);
        return await _weChatClient.PostAsJsonAsync<WeChatResponse>(uri, data, cancel);
    }
}