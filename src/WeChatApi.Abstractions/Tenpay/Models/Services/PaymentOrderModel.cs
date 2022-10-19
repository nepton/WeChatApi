using System;

namespace WeChatApi.Tenpay
{
    public class TenpayOrder
    {
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string TradeType { get; set; }

        /// <summary>
        /// 支付Id
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        public TenpayOrderState State { get; set; }

        /// <summary>
        /// 商户的订单号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime PaymentTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 银行类型，采用字符串类型的银行卡标识 CCB_DEBIT
        /// </summary>
        public string BankType { get; set; }

        /// <summary>
        /// 用户在商户appid下的唯一标识
        /// </summary>
        public string OpenId { get; set; }
    }
}
