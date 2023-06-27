using System.Net.Http.Headers;

namespace AssetTrackerWebsite.HttpExtensions;

public static class HttpClientFactoryExtension
{
    public static HttpClient CreateAuthenticatedClientAsync(this IHttpClientFactory httpClientFactory,
        string token)
    {
        var client = httpClientFactory.CreateClient("AssetTrackerApi");
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return client;
    }
}