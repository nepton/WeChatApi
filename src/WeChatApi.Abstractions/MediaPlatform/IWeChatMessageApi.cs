using System.Threading;
using System.Threading.Tasks;

namespace WeChatApi.MediaPlatform
{
    /// <summary>
    /// 消息发送API
    /// </summary>
    public interface IWeChatMessageApi
    {
        /// <summary>
        /// 发送模板消息, 例如通知信息, 订单状态变更消息等
        /// 参考 https://developers.weixin.qq.com/doc/offiaccount/Message_Management/Template_Message_Interface.html
        /// </summary>
        /// <param name="clientAccessToken">API 调用的 Access token</param>
        /// <param name="message"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<WeChatResponse> SendTemplateMessageAsync(string clientAccessToken, TemplateMessage message, CancellationToken cancel = default);
    }
}
