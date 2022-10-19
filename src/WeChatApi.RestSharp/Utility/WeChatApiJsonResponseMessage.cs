using System;
using Newtonsoft.Json;

namespace WeChatApi.RestSharp.Utility;

/// <summary>
/// 微信 API JSON 的返回结果
/// </summary>
public class WeChatApiJsonResponseMessage 
{
    public WeChatApiJsonResponseMessage(string jsonText)
    {
        if (string.IsNullOrEmpty(jsonText))
            throw new ArgumentException("No text from response.", nameof(jsonText));

        ResponseText = jsonText;

        // 尝试读取错误信息
        var noError = new
        {
            errcode = 0,
            errmsg  = ""
        };
        var error = JsonConvert.DeserializeAnonymousType(jsonText, noError) ?? noError;

        Success = error.errcode == 0;
        if (!Success)
        {
            Error = new WeChatApiError(error.errcode, error.errmsg);
        }
    }

    /// <summary>
    /// 返回的文本
    /// </summary>
    public string ResponseText
    {
        get;
    }

    /// <summary>
    /// 回应是否成功
    /// </summary>
    public bool Success
    {
        get;
    }

    /// <summary>
    /// 微信API返回的错误信息
    /// </summary>
    public WeChatApiError Error
    {
        get;
    }


    /// <summary>
    /// 确保执行成功
    /// </summary>
    public void EnsureSuccess()
    {
        if (!Success)
            throw Error.GetException();
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public TResult Parse<TResult>()
    {
        return JsonConvert.DeserializeObject<TResult>(ResponseText);
    }
}