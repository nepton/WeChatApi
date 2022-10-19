using System.Threading;
using System.Threading.Tasks;

namespace WeChatApi.OpenConnect
{
    /// <summary>
    /// 微信 Open Connect 服务
    /// </summary>
    public interface IWeChatOpenConnectClient
    {
        /// <summary>
        /// 获取指定用户的 OpenId
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<string> GetOpenIdAsync(string authorizationCode, CancellationToken cancel = default);
    }
}
