using System.Xml.Serialization;

namespace WeChatApi.Tenpay
{
    /// <summary>
    /// 这个类用来接受微信支付时候的回调
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class TenpayPaymentCallbackMessage
    {
        /// <remarks/>
        [XmlElement("appid")]
        public string AppId { get; set; }

        /// <remarks/>
        [XmlElement("bank_type")]
        public string BankType { get; set; }

        /// <remarks/>
        [XmlElement("cash_fee")]
        public int CashFee { get; set; }

        /// <remarks/>
        [XmlElement("fee_type")]
        public string FeeType { get; set; }

        /// <remarks/>
        [XmlElement("is_subscribe")]
        public string IsSubscribe { get; set; }

        /// <remarks/>
        [XmlElement("mch_id")]
        public string MerchantId { get; set; }

        /// <remarks/>
        [XmlElement("nonce_str")]
        public string NonceStr { get; set; }

        /// <remarks/>
        [XmlElement("openid")]
        public string OpenId { get; set; }

        /// <remarks/>
        [XmlElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <remarks/>
        [XmlElement("result_code")]
        public string ResultCode { get; set; }

        /// <remarks/>
        [XmlElement("return_code")]
        public string ReturnCode { get; set; }

        /// <remarks/>
        [XmlElement("sign")]
        public string Sign { get; set; }

        /// <remarks/>
        [XmlElement("time_end")]
        public string TimeEnd { get; set; }

        /// <remarks/>
        [XmlElement("total_fee")]
        public decimal TotalFee { get; set; }

        /// <remarks/>
        [XmlElement("trade_type")]
        public string TradeType { get; set; }

        /// <remarks/>
        [XmlElement("transaction_id")]
        public string TransactionId { get; set; }
    }
}
