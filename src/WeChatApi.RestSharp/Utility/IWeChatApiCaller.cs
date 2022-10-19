using System;
using System.Threading;
using System.Threading.Tasks;

namespace WeChatApi.RestSharp.Utility;

/// <summary>
/// 微信客户端, 负责执行微信指令
/// </summary>
public interface IWeChatApiCaller
{
    /// <summary>
    /// 执行 HTTP GET 请求
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<string> GetAsStringAsync(Uri uri, CancellationToken cancel = default);

    /// <summary>
    /// 执行 HTTP GET 请求, 并返回 JSON 对象
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="uri"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TResponse> GetAsJsonAsync<TResponse>(Uri uri, CancellationToken cancel = default);

    /// <summary>
    /// 执行 HTTP POST 请求
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="value"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<string> PostAsStringAsync(Uri uri, object value, CancellationToken cancel = default);

    /// <summary>
    /// 执行 HTTP POST 请求
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="value"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task PostAsJsonAsync(Uri uri, object value, CancellationToken cancel = default);

    /// <summary>
    /// 执行 HTTP POST 请求
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="uri"></param>
    /// <param name="value"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TResponse> PostAsJsonAsync<TResponse>(Uri uri, object value, CancellationToken cancel = default);
}