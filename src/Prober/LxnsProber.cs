using DXKumaBot.Response.Lxns;
using System.Net.Http.Json;

namespace DXKumaBot.Prober;

public sealed class LxnsProber
{
    private const string BaseUrl = "https://maimai.lxns.net/api/v0/maimai";
    private readonly HttpClient _httpClient;
    private readonly LxnsPlayer _userInfo;

    public LxnsProber(string qq, string apiKey)
    {
        _httpClient = new();
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", apiKey);
        string url = $"{BaseUrl}/player/qq/{qq}";
#pragma warning disable VSTHRD002
#pragma warning disable VSTHRD104
        _userInfo = GetAsync<LxnsPlayer>(new(url)).Result;
#pragma warning restore VSTHRD104
#pragma warning restore VSTHRD002
    }

    private async Task<T> GetAsync<T>(Uri url)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        LxnsResponse<T>? result = await response.Content.ReadFromJsonAsync<LxnsResponse<T>>();
        if (result is null || !result.Success || result.Data is null)
        {
            throw new SystemException(result?.Message);
        }

        return result.Data;
    }

    public async Task<LxnsB50> GetB50Async()
    {
        string url = $"{BaseUrl}/player/{_userInfo.FriendCode}/bests";
        LxnsB50 result = await GetAsync<LxnsB50>(new(url));
        return result;
    }

    public Task<LxnsPlayer> GetUserInfoAsync()
    {
        return Task.FromResult(_userInfo);
    }
}