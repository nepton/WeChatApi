using System;

namespace WeChatApi.RestSharp.WeChatBaseUrl;

/// <summary>
/// 微信 API 服务器地址提供模块
/// </summary>
public interface IMediaPlatformServerUri
{
    /// <summary>
    /// 生成访问地址
    /// </summary>
    /// <param name="pathAndQuery">查询路径和查询字符串</param>
    /// <returns></returns>
    Uri GetUri(string pathAndQuery);
}