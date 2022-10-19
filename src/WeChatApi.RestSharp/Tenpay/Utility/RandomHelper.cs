using System;
using System.Linq;
using System.Security.Cryptography;

namespace WeChatApi.RestSharp.Tenpay.Utility;

public static class RandomHelper
{
    /// <summary>
    /// 生成随机数
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string GetRandomString(int length)
    {
        if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));

        var rand = RandomNumberGenerator.Create();
        var data = new byte[(length + 1) / 2];
        rand.GetBytes(data);

        var result = string.Join("", data.Select(x => x.ToString("X2")));
        if (result.Length > length)
        {
            result = result.Substring(0, length);
        }

        return result;
    }
}
