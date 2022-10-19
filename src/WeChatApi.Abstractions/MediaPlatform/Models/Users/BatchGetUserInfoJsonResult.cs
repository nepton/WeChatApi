using System.Collections.Generic;

namespace WeChatApi.MediaPlatform.Users
{
    /// <summary>
    /// 批量获取用户基本信息返回结果
    /// </summary>
    public class BatchGetUserInfoJsonResult : WeChatResponse
    {
        public List<UserInfoJson> user_info_list { get; set; }
    }
}
