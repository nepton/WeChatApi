using System.Xml.Serialization;

namespace WeChatApi.Tenpay
{
    /// <summary>
    /// 查询订单接口返回结果
    /// </summary>
    public class TenpayOrderQueryResponse : TenpayResponse
    {
        /// <summary>
        /// 微信支付分配的终端设备号
        /// </summary>
        [XmlElement("device_info")]
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 用户在商户appid下的唯一标识
        /// </summary>
        [XmlElement("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
        /// </summary>
        [XmlElement("is_subscribe")]
        public string IsSubscribe { get; set; }

        /// <summary>
        /// 调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP，MICROPAY
        /// </summary>
        [XmlElement("trade_type")]
        public string TradeType { get; set; }

        /// <summary>
        /// SUCCESS—支付成功
        /// REFUND—转入退款
        /// NOTPAY—未支付
        /// CLOSED—已关闭
        /// REVOKED—已撤销（刷卡支付）
        /// USERPAYING--用户支付中
        /// PAYERROR--支付失败(其他原因，如银行返回失败)
        /// </summary>
        [XmlElement("trade_state")]
        public string TradeState { get; set; }

        /// <summary>
        /// 银行类型，采用字符串类型的银行标识
        /// </summary>
        [XmlElement("bank_type")]
        public string BankType { get; set; }

        /// <summary>
        /// 商品详情[服务商]
        /// </summary>
        [XmlElement("detail")]
        public string Detail { get; set; }

        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        [XmlElement("total_fee")]
        public string TotalFee { get; set; }

        /// <summary>
        /// 应结订单金额=订单金额-非充值代金券金额，应结订单金额<=订单金额
        /// </summary>
        [XmlElement("settlement_total_fee")]
        public string SettlementTotalFee { get; set; }

        /// <summary>
        /// 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        [XmlElement("fee_type")]
        public string FeeType { get; set; }

        /// <summary>
        /// 现金支付金额订单现金支付金额
        /// </summary>
        [XmlElement("cash_fee")]
        public string CashFee { get; set; }

        /// <summary>
        /// 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        [XmlElement("cash_fee_type")]
        public string CashFeeType { get; set; }

        /// <summary>
        /// “代金券”金额<=订单金额，订单金额-“代金券”金额=现金支付金额
        /// </summary>
        [XmlElement("coupon_fee")]
        public string CouponFee { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        [XmlElement("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户系统的订单号，与请求一致。
        /// </summary>
        [XmlElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 附加数据，原样返回
        /// </summary>
        [XmlElement("attach")]
        public string Attach { get; set; }

        /// <summary>
        /// 订单支付时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010
        /// </summary>
        [XmlElement("time_end")]
        public string TimeEnd { get; set; }

        /// <summary>
        /// 对当前查询订单状态的描述和下一步操作的指引
        /// </summary>
        [XmlElement("trade_state_desc")]
        public string TradeStateDesc { get; set; }
    }
}
