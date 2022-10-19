using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WeChatApi.RestSharp.Tenpay.Utility;

public static class EncryptHelper
{
    /// <summary>采用SHA-1算法加密字符串（小写）</summary>
    /// <param name="plainText">需要加密的字符串</param>
    /// <returns></returns>
    public static string GetSha1(string plainText)
    {
        using var sha1 = SHA1.Create();

        var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(plainText));
        return string.Concat(hash.Select(b => b.ToString("x2")));
    }

    /// <summary>HMAC SHA256 加密</summary>
    /// <param name="plainText">加密消息原文</param>
    /// <param name="secret">秘钥</param>
    /// <returns></returns>
    public static string GetHmacSha256(string plainText, string secret)
    {
        plainText ??= "";
        secret    ??= "";
        var sec  = Encoding.UTF8.GetBytes(secret);
        var text = Encoding.UTF8.GetBytes(plainText);

        using var hmacSha256 = new HMACSHA256(sec);

        var hash = hmacSha256.ComputeHash(text);
        return string.Concat(hash.Select(b => b.ToString("x2")));
    }

    /// <summary>获取大写的MD5签名结果</summary>
    /// <param name="plainText">需要加密的字符串</param>
    /// <returns></returns>
    public static string GetMd5(string plainText)
    {
        using var md5 = MD5.Create();

        var bytes = Encoding.UTF8.GetBytes(plainText);
        var hash  = md5.ComputeHash(bytes);
        return string.Join("", hash.Select(x => x.ToString("x2")));
    }
}
