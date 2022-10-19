using System.Threading;
using System.Threading.Tasks;

namespace WeChatApi.Tenpay
{
    /// <summary>
    /// 腾讯支付接口
    /// </summary>
    public interface IWeChatTenpayApi
    {
        /// <summary>
        /// 统一支付接口
        /// 统一支付接口，可接受JSAPI/NATIVE/APP 下预支付订单，返回预支付订单号。NATIVE 支付返回二维码code_url。
        /// </summary>
        /// <remarks>
        /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1
        /// </remarks>
        /// <param name="request">微信支付需要post的Data数据</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<TenpayPlaceOrderResponse> CreateOrderAsync(TenpayPlaceOrderRequest request, CancellationToken cancel = default);

        /// <summary>
        /// 订单查询接口
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<TenpayOrderQueryResponse> GetOrderAsync(TenpayOrderQueryRequest request, CancellationToken cancel = default);
    }
}
