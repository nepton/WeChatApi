using System;

namespace WeChatApi.RestSharp.Utility;

public class WeChatApiException : Exception
{
    public int ErrorCode
    {
        get;
    }

    public WeChatApiException(in int errorCode, string errorMessage) : base(errorMessage)
    {
        ErrorCode = errorCode;
    }

    public override string ToString()
    {
        return $"Error: [{ErrorCode}] {base.Message} ";
    }
}