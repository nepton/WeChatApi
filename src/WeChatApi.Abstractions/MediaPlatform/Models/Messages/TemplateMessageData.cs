namespace WeChatApi.MediaPlatform
{
    public class TemplateMessageData
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TemplateMessageData(string value)
        {
            this.Value = value;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TemplateMessageData(string value, string color)
        {
            this.Value = value;
            this.Color = color;
        }

        public string Value { get; set; }

        public string Color { get; set; }
    }
}
