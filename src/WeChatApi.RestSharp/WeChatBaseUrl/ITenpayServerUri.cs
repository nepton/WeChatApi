using System;

namespace WeChatApi.RestSharp.WeChatBaseUrl;

/// <summary>
/// 腾讯支付服务器
/// </summary>
public interface ITenpayServerUri
{
    /// <summary>
    /// 生成访问地址
    /// </summary>
    /// <param name="pathAndQuery">查询路径和查询字符串</param>
    /// <returns></returns>
    Uri GetUri(string pathAndQuery);
}