using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebApp.Extensions
{
    public static class HttpClientExtension
    {
        public static async Task<TResult> PostGetResponseAsync<TResult, TValue>(this HttpClient client, string Url, TValue value)
        {
            var httpReponse = await client.PostAsJsonAsync(Url, value);
            return httpReponse.IsSuccessStatusCode ? await httpReponse.Content.ReadFromJsonAsync<TResult>() : default;
        }

        public static async Task PostAsync<TValue>(this HttpClient client, string Url, TValue value)
        {
            await client.PostAsJsonAsync(Url, value);
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string Url)
        {
            return await client.GetFromJsonAsync<T>(Url);
        }
    }
}