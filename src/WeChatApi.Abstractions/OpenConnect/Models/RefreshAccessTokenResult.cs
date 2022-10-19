namespace WeChatApi.OpenConnect
{
    /// <summary>
    /// 刷新access_token有效期返回结果
    /// </summary>
    public class RefreshAccessTokenResult
    {
        /// <summary>
        /// 接口调用凭证
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// 授权用户唯一标识
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>
        public string scope { get; set; }
    }
}
