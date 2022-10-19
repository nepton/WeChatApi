﻿using System;
using Microsoft.Extensions.Options;

namespace WeChatApi.RestSharp.WeChatBaseUrl.Implements;

/// <summary>
/// 简单版本的服务器地址选择器, 以后需要再实现高级版的
/// </summary>
/// <remarks>
/// https://developers.weixin.qq.com/doc/offiaccount/Basic_Information/Interface_field_description.html
/// </remarks>
public class SimpleTenpayServerUri : ITenpayServerUri
{
    private readonly WeChatApiOptions _configuration;

    public SimpleTenpayServerUri(IOptionsSnapshot<WeChatApiOptions> configuration)
    {
        _configuration = configuration.Value;
    }

    /// <summary>
    /// 获取当前最好的服务器地址
    /// </summary>
    /// <returns></returns>
    public Uri GetPreferAddress()
    {
        return new Uri("https://api.mch.weixin.qq.com");
    }

    /// <summary>
    /// 生成访问地址
    /// </summary>
    /// <param name="pathAndQuery">查询路径和查询字符串</param>
    /// <returns></returns>
    public Uri GetUri(string pathAndQuery)
    {
        var baseUri = GetPreferAddress();

        if (_configuration.TenpaySandboxMode)
            baseUri = new Uri(baseUri, "sandboxnew/");

        return new Uri(baseUri, pathAndQuery);
    }
}