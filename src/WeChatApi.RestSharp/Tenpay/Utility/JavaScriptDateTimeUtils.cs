using System;

namespace WeChatApi.RestSharp.Tenpay.Utility;

public static class JavaScriptDateTimeUtils
{
    private static readonly long InitialJavaScriptDateTicks = 621355968000000000;

    public static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime)
    {
        var javaScriptTicks = (dateTime.Ticks - InitialJavaScriptDateTicks) / 10000;
        return javaScriptTicks;
    }

    public static DateTime ConvertJavaScriptTicksToDateTime(long javaScriptTicks, DateTimeKind kind = DateTimeKind.Utc)
    {
        var dateTime = new DateTime((javaScriptTicks * 10000) + InitialJavaScriptDateTicks, kind);
        return dateTime;
    }
}