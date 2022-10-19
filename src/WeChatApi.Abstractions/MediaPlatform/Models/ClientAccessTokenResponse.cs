namespace WeChatApi.MediaPlatform
{
    /// <summary>
    /// 业务系统 Access token 结果回应
    /// </summary>
    public class ClientAccessTokenResponse : WeChatResponse
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }
    }
}
