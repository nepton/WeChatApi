using System;

namespace WeChatApi.MediaPlatform
{
    /// <summary>
    /// 批量获取用户基本信息数据
    /// </summary>
    public class BatchGetUserInfoData
    {
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// 必填
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 国家地区语言版本，请使用Language范围内的值：zh_CN 简体，zh_TW 繁体，en 英语，默认为zh-CN
        /// 非必填
        /// </summary>
        public string lang { get; set; }

        /// <summary>
        /// lang属性的枚举转换
        /// </summary>
        public Language LangEnum
        {
            get { return (Language) Enum.Parse(typeof(Language), lang); }
            set { lang = value.ToString(); }
        }
    }
}
