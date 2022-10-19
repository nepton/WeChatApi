using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeChatApi.MediaPlatform;
using WeChatApi.OpenConnect;
using WeChatApi.RestSharp.MediaPlatform;
using WeChatApi.RestSharp.OpenConnect;
using WeChatApi.RestSharp.Tenpay;
using WeChatApi.RestSharp.Utility;
using WeChatApi.RestSharp.WeChatBaseUrl;
using WeChatApi.RestSharp.WeChatBaseUrl.Implements;
using WeChatApi.Tenpay;

namespace WeChatApi.RestSharp;

/// <summary>
/// 仓储参数配置
/// </summary>
public static class WeChatApiRestSharpServicesExtensions
{
    /// <summary>
    /// 添加 WeChat API 接口
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    public static IServiceCollection AddWeChatApi(this IServiceCollection services, Action<WeChatApiOptions> configureOptions)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (configureOptions == null) throw new ArgumentNullException(nameof(configureOptions));

        services.Configure(configureOptions);
        AddCore(services);
        return services;
    }

    /// <summary>
    /// 添加 WeChat API 接口
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static IServiceCollection AddWeChatApi(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        services.Configure<WeChatApiOptions>(configuration);
        AddCore(services);
        return services;
    }

    private static void AddCore(IServiceCollection services)
    {
        // 调用客户端
        services.AddTransient<IWeChatApiCaller, WeChatApiCallerWithHttp>();

        // URI 生成
        services.AddTransient<IOpenConnectServerUri, SimpleOpenConnectServerUri>();
        services.AddTransient<IMediaPlatformServerUri, SimpleMediaPlatformServerUri>();
        services.AddTransient<ITenpayServerUri, SimpleTenpayServerUri>();

        // API
        services.AddTransient<IWeChatOpenConnectApi, WeChatOpenConnectApi>();

        // 向外公布的服务
        services.AddTransient<IWeChatOpenConnectClient, WeChatOpenConnectService>();

        services.AddTransient<IWeChatTenpayApi, WeChatTenpayApi>();
        services.AddTransient<IWeChatTenpayClient, WeChatTenpayService>();

        services.AddTransient<IWeChatJsApi, WeChatJsApi>();
        services.AddTransient<IWeChatLoginApi, WeChatLoginApi>();
        services.AddTransient<IWeChatMessageApi, WeChatMessageApi>();
    }
}
