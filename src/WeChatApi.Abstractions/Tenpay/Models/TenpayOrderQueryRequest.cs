namespace WeChatApi.Tenpay
{
    /// <summary>
    /// 微信支付订单查询参数
    /// </summary>
    public class TenpayOrderQueryRequest
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 微信的订单号，建议优先使用
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户系统内部的订单号，请确保在同一商户号下唯一
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 查询订单 请求参数[境内服务商]
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="outTradeNo"></param>
        /// <param name="key"></param>
        /// <param name="transactionId"></param>
        public TenpayOrderQueryRequest(
            string appId,
            string mchId,
            string transactionId,
            string outTradeNo,
            string key)
        {
            AppId         = appId;
            MchId         = mchId;
            TransactionId = transactionId;
            OutTradeNo    = outTradeNo;
            Key           = key;
        }
    }
}
