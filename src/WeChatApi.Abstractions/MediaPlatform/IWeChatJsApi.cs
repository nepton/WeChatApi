using System.Threading;
using System.Threading.Tasks;

namespace WeChatApi.MediaPlatform
{
    /// <summary>
    /// JSAPI接口, 提供
    /// </summary>
    public interface IWeChatJsApi
    {
        /// <summary>
        /// 获取JSAPI接口的ticket
        /// </summary>
        /// <param name="accessToken">公众号的全局唯一票据</param>
        /// <param name="cancel"></param>
        /// <returns>JSAPI接口的ticket</returns>
        Task<JsTicket> GetTicketAsync(string accessToken, CancellationToken cancel = default);

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="ticketTicket"></param>
        /// <param name="url"></param>
        /// <param name="nonceStr"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        string GetSignature(string ticketTicket, string url, string nonceStr, string timestamp);
    }
}
