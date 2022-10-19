// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WeChatApi.MediaPlatform;
using WeChatApi.RestSharp;

// Prepare the service
var builder = new ServiceCollection();
builder.AddLogging();

builder.AddWeChatApi(options =>
{
    options.AppId     = "wx1234567890";
    options.AppSecret = "1234567890";
});
var services = builder.BuildServiceProvider();

// build the client
var options = services.GetRequiredService<IOptions<WeChatApiOptions>>().Value; // 获取登录参数
var api     = services.GetRequiredService<IWeChatLoginApi>();                        // 获取API对象

// call the API
var token = await api.GetAccessTokenAsync(options.AppId, options.AppSecret); // 获取 AccessToken

Console.WriteLine(token.access_token); 
