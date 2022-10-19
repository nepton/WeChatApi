using System;

namespace WeChatApi.Tenpay
{
    /// <summary>
    /// 通过 JS API 付款模型
    /// </summary>
    public class JsApiPayModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public JsApiPayModel(string payerOpenId, string orderNumber, DateTime expireTime, string description, decimal payable)
        {
            PayerOpenId = payerOpenId;
            OrderNumber = orderNumber;
            ExpireTime  = expireTime;
            Description = description;
            Payable     = payable;
        }

        /// <summary>
        /// 付款人
        /// </summary>
        public string PayerOpenId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNumber { get; set; }

        public DateTime ExpireTime { get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 应付款
        /// </summary>
        public decimal Payable { get; set; }
    }
}
