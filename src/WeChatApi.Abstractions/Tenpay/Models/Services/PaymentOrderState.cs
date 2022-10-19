namespace WeChatApi.Tenpay
{
    public enum TenpayOrderState
    {
        /// <summary>
        /// 支付成功
        /// </summary>
        Success,

        /// <summary>
        /// 转入退款
        /// </summary>
        Refund,

        /// <summary>
        /// 未支付
        /// </summary>
        NotPay,

        /// <summary>
        /// 已关闭
        /// </summary>
        Closed,

        /// <summary>
        /// 已撤销（刷卡支付）
        /// </summary>
        Revoked,

        /// <summary>
        /// 用户支付中
        /// </summary>
        UserPaying,

        /// <summary>
        /// 支付失败(其他原因，如银行返回失败)
        /// </summary>
        PayError,

        /// <summary>
        /// 微信新的不支持的状态
        /// </summary>
        Others,
    }
}
