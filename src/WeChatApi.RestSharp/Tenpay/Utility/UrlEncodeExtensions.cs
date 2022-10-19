using System;

namespace WeChatApi.RestSharp.Tenpay.Utility;

/// <summary>
/// Url 编码处理扩展类
/// </summary>
public static class UrlEncodeExtensions
{
    public static string TryEscapeDataStringOrNull(this string data)
    {
        return data == null ? (string) null : Uri.EscapeDataString(data);
    }
}