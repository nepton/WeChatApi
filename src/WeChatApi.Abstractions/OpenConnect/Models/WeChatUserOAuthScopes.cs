namespace WeChatApi.OpenConnect
{
    /// <summary>
    /// 应用授权作用域
    /// </summary>
    public enum WeChatUserOAuthScopes
    {
        /// <summary>
        /// 获取用户 openid
        /// </summary>
        OpenId,

        /// <summary>
        /// 弹出授权页面，获取昵称、性别、所在地
        /// </summary>
        UserInfo
    }
}
