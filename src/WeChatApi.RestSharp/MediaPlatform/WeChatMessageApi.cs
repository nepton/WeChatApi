using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeChatApi.MediaPlatform;
using WeChatApi.RestSharp.Utility;
using WeChatApi.RestSharp.WeChatBaseUrl;

namespace WeChatApi.RestSharp.MediaPlatform;

public class WeChatMessageApi : IWeChatMessageApi
{
    private readonly IWeChatApiCaller        _weChatClient;
    private readonly IMediaPlatformServerUri _weChatApiServer;

    public WeChatMessageApi(IMediaPlatformServerUri weChatApiServer, IWeChatApiCaller weChatClient)
    {
        _weChatApiServer = weChatApiServer;
        _weChatClient    = weChatClient;
    }

    /// <summary>
    /// 发送模板消息, 例如通知信息, 订单状态变更消息等
    /// 参考 https://developers.weixin.qq.com/doc/offiaccount/Message_Management/Template_Message_Interface.html
    /// </summary>
    /// <param name="clientAccessToken">API 调用的 Access token</param>
    /// <param name="message"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<WeChatResponse> SendTemplateMessageAsync(string clientAccessToken, TemplateMessage message, CancellationToken cancel = default)
    {
        if (message == null) throw new ArgumentNullException(nameof(message));

        var json = new
        {
            touser      = message.ReceiverOpenId,
            template_id = message.TemplateId,
            data = message.Data.Select(x => new KeyValuePair<string, object>(
                x.Key,
                new
                {
                    value = x.Value.Value,
                    color = x.Value.Color
                })).ToDictionary(x => x.Key, x => x.Value),
            url = message.Url,
        };

        var url = $"/cgi-bin/message/template/send?access_token={clientAccessToken}";
        return _weChatClient.PostAsJsonAsync<WeChatResponse>(_weChatApiServer.GetUri(url), json, cancel);
    }
}