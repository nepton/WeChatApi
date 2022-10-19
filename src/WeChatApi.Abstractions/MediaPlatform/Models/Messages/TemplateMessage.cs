using System.Collections.Generic;

namespace WeChatApi.MediaPlatform
{
    public class TemplateMessage
    {
        public string ReceiverOpenId { get; set; }

        public string TemplateId { get; set; }

        public string Url { get; set; }

        public Dictionary<string, TemplateMessageData> Data { get; set; }
    }
}
