WeChat API is a dotnet library for WeChat API. It provides a simple way to use WeChat API.

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
