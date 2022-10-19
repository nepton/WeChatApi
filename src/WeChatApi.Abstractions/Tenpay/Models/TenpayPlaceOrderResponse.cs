using System.Xml.Serialization;

namespace WeChatApi.Tenpay
{
    /// <summary>
    /// 统一支付接口在return_code 和result_code 都为SUCCESS 的时候有返回详细信息
    /// </summary>
    public class TenpayPlaceOrderResponse : TenpayResponse
    {
        /// <summary>
        /// 微信支付分配的终端设备号
        /// </summary>
        [XmlElement("device_info")]
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 交易类型:JSAPI、NATIVE、APP
        /// </summary>
        [XmlElement("trade_type")]
        public string TradeType { get; set; }

        /// <summary>
        /// 微信生成的预支付ID，用于后续接口调用中使用
        /// </summary>
        [XmlElement("prepay_id")]
        public string PrepayId { get; set; }

        /// <summary>
        /// trade_type为NATIVE时有返回，此参数可直接生成二维码展示出来进行扫码支付
        /// </summary>
        [XmlElement("code_url")]
        public string CodeUrl { get; set; }

        /// <summary>
        /// 在H5支付时返回
        /// </summary>
        [XmlElement("mweb_url")]
        public string MobileWebUrl { get; set; }
    }
}
