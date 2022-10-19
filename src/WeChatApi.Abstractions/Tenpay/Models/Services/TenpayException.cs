using System;

namespace WeChatApi.Tenpay
{
    public class TenpayException : Exception
    {
        public string Code
        {
            get;
        }

        public TenpayException(string code, string message) : base(message)
        {
            Code = code;
        }
    }
}
