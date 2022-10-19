using System;

namespace WeChatApi.Tenpay
{
    /// <summary>
    /// 微信支付提交的XML Data数据[统一下单]
    /// </summary>
    public class TenpayPlaceOrderRequest
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
        /// 自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"，String(32)如：013467007045764
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 商品详细列表，使用Json格式，传输签名前请务必使用CDATA标签将JSON文本串保护起来。
        /// cost_price Int 可选 32 订单原价，商户侧一张小票订单可能被分多次支付，订单原价用于记录整张小票的支付金额。当订单原价与支付金额不相等则被判定为拆单，无法享受优惠。
        ///     receipt_id String 可选 32 商家小票ID
        /// goods_detail 服务商必填[]：
        ///     goods_id String 必填 32 商品的编号
        ///     wxpay_goods_id String 可选 32 微信支付定义的统一商品编号
        ///     goods_name String 可选 256 商品名称 
        ///     quantity Int 必填  32 商品数量
        ///     price Int 必填 32 商品单价，如果商户有优惠，需传输商户优惠后的单价
        /// String(6000)
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。String(127)，如：深圳分店
        /// </summary>
        public string Attach { get; set; }

        /// <summary>
        /// 符合ISO 4217标准的三位字母代码，默认人民币：CNY，详细列表请参见货币类型。String(16)，如：CNY
        /// </summary>
        public string FeeType { get; set; } = "CNY";

        /// <summary>
        /// 商家订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 商品金额,以分为单位(money * 100).ToString()
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>
        /// 用户的公网ip，不是商户服务器IP
        /// </summary>
        public string BillCreateIp { get; set; }

        /// <summary>
        /// 订单生成时间，最终生成格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。其他详见时间规则。
        /// 如果为空，则默认为当前服务器时间
        /// </summary>
        public DateTime? TimeStart { get; set; }

        /// <summary>
        /// 订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。其他详见时间规则
        /// 注意：最短失效时间间隔必须大于5分钟。
        /// 留空则不设置失效时间
        /// </summary>
        public DateTime? TimeExpire { get; set; }

        /// <summary>
        /// 商品标记，使用代金券或立减优惠功能时需要的参数，说明详见代金券或立减优惠。String(32)，如：WXG
        /// </summary>
        public string GoodsTag { get; set; }

        /// <summary>
        /// 接收财付通通知的URL
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public TenpayPaymentType TradeType { get; set; }

        /// <summary>
        /// trade_type=NATIVE时（即扫码支付），此参数必传。此参数为二维码中包含的商品ID，商户自行定义。
        /// String(32)，如：12235413214070356458058
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 上传此参数no_credit--可限制用户不能使用信用卡支付
        /// </summary>
        public bool LimitPay { get; set; }

        /// <summary>
        /// 用户的openId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 是否需要分账
        /// https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=24_3&index=3
        /// https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=26_3
        /// "Y" -- 需要分账, null 或者 "N"-不需要分账,
        /// 服务商需要在 产品中心--特约商户授权产品 申请特约商户授权,
        /// 并且特约商户需要在 产品中心-我授权的商品中给服务商授权才可以使用分账功能;
        /// 普通商户需要 产品中心-我的产品 中开通分账功能；
        /// </summary>
        public string ProfitSharing { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 服务商
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="body"></param>
        /// <param name="outTradeNo"></param>
        /// <param name="totalFee">单位：分</param>
        /// <param name="billCreateIp"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="tradeType"></param>
        /// <param name="openid">trade_type=NATIVE时，OpenId应该为null</param>
        /// <param name="key"></param>
        public TenpayPlaceOrderRequest(
            string       appId,
            string       mchId,
            string       body,
            string       outTradeNo,
            int          totalFee,
            string       billCreateIp,
            string       notifyUrl,
            TenpayPaymentType tradeType,
            string       openid,
            string       key
        )
        {
            AppId        = appId;
            MchId        = mchId;
            Body         = body;
            OutTradeNo   = outTradeNo;
            TotalFee     = totalFee;
            BillCreateIp = billCreateIp;
            NotifyUrl    = notifyUrl;
            TradeType    = tradeType;
            OpenId       = openid;
            Key          = key;
        }
    }
}
