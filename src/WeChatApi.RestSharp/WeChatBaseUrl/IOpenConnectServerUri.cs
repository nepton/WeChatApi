using System;

namespace WeChatApi.RestSharp.WeChatBaseUrl;

/// <summary>
/// 微信 OPEN CONNECT 服务器地址
/// </summary>
public interface IOpenConnectServerUri
{
    /// <summary>
    /// 生成访问地址
    /// </summary>
    /// <param name="pathAndQuery">查询路径和查询字符串</param>
    /// <returns></returns>
    Uri GetUri(string pathAndQuery);
}