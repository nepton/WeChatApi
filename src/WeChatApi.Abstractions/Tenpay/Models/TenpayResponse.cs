using System.Xml.Serialization;

namespace WeChatApi.Tenpay
{
    /// <summary>
    /// 统一支付接口在 return_code为 SUCCESS的时候有返回
    /// </summary>
    public class TenpayResponse
    {
        public bool IsReturnCodeSuccess => ReturnCode == "SUCCESS";

        /// <summary>
        /// result_code == "SUCCESS"
        /// </summary>
        /// <returns></returns>
        public bool IsResultCodeSuccess => ResultCode == "SUCCESS";

        /// <summary>
        /// 返回代码
        /// </summary>
        [XmlElement("return_code")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [XmlElement("return_msg")]
        public string ReturnMessage { get; set; }

        /// <summary>
        /// 微信分配的公众账号ID（付款到银行卡接口，此字段不提供）
        /// </summary>
        [XmlElement("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        [XmlElement("mch_id")]
        public string MerchantId { get; set; }

        /// <summary>
        /// 随机字符串，不长于32 位
        /// </summary>
        [XmlElement("nonce_str")]
        public string NonceStr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [XmlElement("sign")]
        public string Sign { get; set; }

        /// <summary>
        /// SUCCESS/FAIL
        /// </summary>
        [XmlElement("result_code")]
        public string ResultCode { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        [XmlElement("err_code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [XmlElement("err_code_des")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 原始XML信息
        /// </summary>
        public string Xml { get; set; }
    }
}
