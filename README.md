# WeChatApi
[![Build status](https://ci.appveyor.com/api/projects/status/euhe9n0f30161hsu?svg=true)](https://ci.appveyor.com/project/nepton/wechatapi)
![GitHub issues](https://img.shields.io/github/issues/nepton/WeChatApi.svg)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/nepton/WeChatApi/blob/master/LICENSE)

WeChat API is a dotnet library for WeChat API. It provides a simple way to use WeChat API.

## Nuget packages

| Name                   | Version                                                                                                                       | Downloads                                                                                                                      |
|------------------------|-------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------|
| WeChatApi.Abstractions | [![nuget](https://img.shields.io/nuget/v/WeChatApi.Abstractions.svg)](https://www.nuget.org/packages/WeChatApi.Abstractions/) | [![stats](https://img.shields.io/nuget/dt/WeChatApi.Abstractions.svg)](https://www.nuget.org/packages/WeChatApi.Abstractions/) |
| WeChatApi.RestSharp    | [![nuget](https://img.shields.io/nuget/v/WeChatApi.RestSharp.svg)](https://www.nuget.org/packages/WeChatApi.RestSharp/)       | [![stats](https://img.shields.io/nuget/dt/Thingsboard.Net.Flurl.svg)](https://www.nuget.org/packages/Thingsboard.Net.Flurl/)   |

## Installing

Install the NuGet package from nuget.org

```
PM> Install-Package [WeChatApi.RestSharp]
```

## Usage

In startup.cs, add the following code to configure services:

```csharp
// add package WeChatApi.RestSharp
services.AddWeChatApi(options =>
{
    options.AppId     = "wx1234567890";
    options.AppSecret = "1234567890";
});
```

Then you can inject the client factory in your controllers:

```csharp
public class HomeController : Controller
{
    private readonly IWeChatLoginApi _loginApi;
    private readonly WeChatApiOptions _options;
    
    public HomeController(IWeChatLoginApi loginApi, IOptions<WeChatApiOptions> options)
    {
        _loginApi = loginApi;
        _options = options.Value;    
    }

    public async Task<IActionResult> Index()
    {
        // call the API
        var token = await _loginApi.GetAccessTokenAsync(_options.AppId, _options.AppSecret);

        return View();
    }
}
```

## Final
Leave a comment on GitHub if you have any questions or suggestions.

Turn on the star if you like this project.

## License
This project is licensed under the MIT License
