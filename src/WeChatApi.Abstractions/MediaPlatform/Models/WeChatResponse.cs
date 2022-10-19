namespace WeChatApi.MediaPlatform
{
    /// <summary>
    /// 每一个微信接口的公共范围, 主要是错误信息的处理, 这里设定的错误默认值为0, 如果没有读到错误字段, 则保持 0 默认值.
    /// </summary>
    public class WeChatResponse
    {
        /// <summary>
        /// 返回是否成功
        /// </summary>
        public bool Successful => errcode == 0;

        /// <summary>
        /// 错误代码
        /// </summary>
        public int errcode { get; set; } = 0;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string errmsg { get; set; }
    }
}
