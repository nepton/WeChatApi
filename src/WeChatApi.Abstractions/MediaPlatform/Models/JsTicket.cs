namespace WeChatApi.MediaPlatform
{
    /// <summary>
    /// 微信前端调用摄像头的设备, 需要这个ticket对象数据
    /// </summary>
    public class JsTicket : WeChatResponse
    {
        public string ticket { get; set; }

        public int expires_in { get; set; }
    }
}
