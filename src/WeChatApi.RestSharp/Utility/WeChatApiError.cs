namespace WeChatApi.RestSharp.Utility;

public class WeChatApiError
{
    public WeChatApiError(int errorCode, string message)
    {
        ErrorCode = errorCode;
        Message   = message;
    }

    public int ErrorCode { get; }

    public string Message { get; }

    public WeChatApiException GetException()
    {
        return new WeChatApiException(ErrorCode, Message);
    }
}