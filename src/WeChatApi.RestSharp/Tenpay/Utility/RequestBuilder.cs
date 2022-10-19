using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

// ReSharper disable StringLiteralTypo

namespace WeChatApi.RestSharp.Tenpay.Utility;

/// <summary>
/// Request utility
/// </summary>
public sealed class RequestBuilder
{
    /// <summary>
    /// 请求的参数
    /// </summary>
    private readonly Dictionary<string, object> _parameters = new();

    /// <summary>
    /// 设置参数值
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="parameterValue"></param>
    public void SetParameter(string parameter, string parameterValue)
    {
        if (parameter == null) throw new ArgumentNullException(nameof(parameter));
        _parameters[parameter] = parameterValue;
    }

    /// <summary>
    /// 当参数不为null或空字符串时，设置参数值
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="parameterValue"></param>
    public void SetParameterWhenNotNull(string parameter, string parameterValue)
    {
        if (!string.IsNullOrEmpty(parameterValue))
        {
            SetParameter(parameter, parameterValue);
        }
    }

    /// <summary>
    /// 签名
    /// </summary>
    /// <param name="key">密钥</param>
    /// <returns></returns>
    public string SignByMd5(string key)
    {
        var sb = new StringBuilder();

        // 规则是:按参数名称a-z排序,遇到空值的参数不参加签名
        foreach (string k in _parameters.Keys.OrderBy(x => x))
        {
            var v = (string) _parameters[k];
            if (string.IsNullOrEmpty(v) || k is "sign" or "key")
                continue;

            sb.Append($"{k}={v}&");
        }

        sb.Append("key" + "=" + key);

        // https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_1
        var sign = EncryptHelper.GetMd5(sb.ToString()).ToUpper();

        SetParameter("sign", sign); //签名

        return sign;
    }

    /// <summary>
    /// 输出XML
    /// </summary>
    /// <returns></returns>
    public string ToXml()
    {
        var content = from keyValue in _parameters
                      select new XElement(keyValue.Key, keyValue.Value);

        var root = new XElement("xml", content);
        return root.ToString();
    }
}
