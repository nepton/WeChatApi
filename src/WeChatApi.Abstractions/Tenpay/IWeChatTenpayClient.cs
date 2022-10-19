using System.Threading;
using System.Threading.Tasks;

namespace WeChatApi.Tenpay
{
    /// <summary>
    /// 支付服务
    /// </summary>
    public interface IWeChatTenpayClient
    {
        /// <summary>
        /// 创建预付款订单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<string> CreateTransactionAsync(JsApiPayModel model, CancellationToken cancel = default);

        /// <summary>
        /// 创建客户端 JsApi 调用微信付款功能的参数对象
        /// </summary>
        /// <param name="transactionId">预付款订单</param>
        /// <returns></returns>
        string SignForJsApiPackage(string transactionId);

        /// <summary>
        /// 查询微信付款单
        /// </summary>
        /// <param name="tradeNumber">订单号</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<TenpayOrder> GetPaymentOrderAsync(string tradeNumber, CancellationToken cancel = default);
    }
}
