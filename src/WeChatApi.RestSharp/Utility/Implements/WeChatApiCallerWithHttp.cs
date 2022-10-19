using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace WeChatApi.RestSharp.Utility;

/// <summary>
/// 基于 HTTP Client 实现的 WeChatClient 通讯类
/// </summary>
public class WeChatApiCallerWithHttp : IWeChatApiCaller
{
    private readonly ILogger<WeChatApiCallerWithHttp> _logger;

    public WeChatApiCallerWithHttp(ILogger<WeChatApiCallerWithHttp> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 执行 HTTP GET 请求
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<string> GetAsStringAsync(Uri uri, CancellationToken cancel = default)
    {
        if (uri == null)
            throw new ArgumentNullException(nameof(uri));

        // 执行请求
        using var client   = new HttpClient();
        using var response = await client.GetAsync(uri, cancel);

        // HTTP 层错误
        response.EnsureSuccessStatusCode();

        // 读取结果, 并尝试解析
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// 执行 HTTP GET 请求, 并返回 JSON 对象
    /// </summary>
    /// <typeparam name="TResponseMessage"></typeparam>
    /// <param name="uri"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<TResponseMessage> GetAsJsonAsync<TResponseMessage>(Uri uri, CancellationToken cancel = default)
    {
        var responseText = await GetAsStringAsync(uri, cancel);

        var json = new WeChatApiJsonResponseMessage(responseText);
        json.EnsureSuccess();

        return json.Parse<TResponseMessage>();
    }

    /// <summary>
    /// 执行 HTTP POST 请求
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="value"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<string> PostAsStringAsync(Uri uri, object value, CancellationToken cancel = default)
    {
        if (uri == null)
            throw new ArgumentNullException(nameof(uri));
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        using var client = new HttpClient();

        var jsonText   = JsonConvert.SerializeObject(value);
        var restClient = new RestClient(uri.ToString());
        var request    = new RestRequest();
        request.AddBody(jsonText, "application/json");

        var response = await restClient.ExecutePostAsync(request, cancel);
        if (!response.IsSuccessful)
        {
            _logger.LogError("POST to {Url} failed with status code {StatusCode} and content {ResponseContent}, the request body is {RequestContent}",
                uri,
                response.StatusCode,
                response.Content,
                jsonText);
            throw new HttpRequestException($"POST failed with status code {response.StatusCode}");
        }

        return response.Content;

        // var jsonText = JsonConvert.SerializeObject(value);
        // var requestContent = new StringContent(jsonText);
        // using var response = await client.PostAsync(uri, requestContent, new JsonMediaTypeFormatter(), cancel);
        // _logger.LogInformation("POST {Uri}: {@Payload}", uri, jsonText);

        // if (!response.IsSuccessStatusCode)
        // {
        //     var content = await response.Content.ReadAsStringAsync(cancel);
        //     var header = response.Headers.ToString();
        //     _logger.LogError("POST {Uri} failed with status {StatusCode}: {@Header}\n{@Content}", uri, (int) response.StatusCode, header, content);
        //
        //     throw new HttpRequestException($"POST {uri} failed: {header}\n{content}");
        // }

        // return response.Content.ReadAsStringAsync(cancel);
    }

    /// <summary>
    /// 执行 HTTP POST 请求
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="value"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task PostAsJsonAsync(Uri uri, object value, CancellationToken cancel = default)
    {
        var responseText = await PostAsStringAsync(uri, value, cancel);

        var json = new WeChatApiJsonResponseMessage(responseText);
        json.EnsureSuccess();
    }

    /// <summary>
    /// 执行 HTTP POST 请求
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="value"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<TResponse> PostAsJsonAsync<TResponse>(Uri uri, object value, CancellationToken cancel = default)
    {
        var responseText = await PostAsStringAsync(uri, value, cancel);

        var json = new WeChatApiJsonResponseMessage(responseText);
        json.EnsureSuccess();

        return json.Parse<TResponse>();
    }
}
