namespace WeChatApi.MediaPlatform
{
    /// <summary>
    /// 微信消息服务器的地址, 用于接收到微信通知后, 验证发送者使用
    /// </summary>
    public class WeChatMessageServerAddressResponse
    {
        /// <summary>
        /// 获取微信服务器的 IP 段后的 JSON 返回格式
        /// </summary>
        public string[] ip_list { get; set; }
    }
}
