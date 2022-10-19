using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WeChatApi.OpenConnect;

namespace WeChatApi.RestSharp.OpenConnect;

public class WeChatOpenConnectService : IWeChatOpenConnectClient
{
    private readonly IWeChatOpenConnectApi _openConnectApi;
    private readonly WeChatApiOptions      _configuration;

    public WeChatOpenConnectService(IWeChatOpenConnectApi openConnectApi, IOptionsSnapshot<WeChatApiOptions> configuration)
    {
        _openConnectApi = openConnectApi;
        _configuration  = configuration.Value;
    }

    /// <summary>
    /// 获取指定用户的 OpenId
    /// </summary>
    /// <param name="authorizationCode"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<string> GetOpenIdAsync(string authorizationCode, CancellationToken cancel = default)
    {
        var token = await _openConnectApi.GetAccessTokenAsync(_configuration.AppId, _configuration.AppSecret, authorizationCode, cancel);
        return token.openid;
    }
}