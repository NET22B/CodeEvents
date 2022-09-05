using CodeEvents.Common.Dto;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CodeEvents.Client.HttpClients
{
    public class BaseClient
    {
        protected HttpClient HttpClient { get; }

        public BaseClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public BaseClient(HttpClient httpClient, Uri uri) : this(httpClient)
        {
            HttpClient.BaseAddress = uri;
        }

        public async Task<T?> GetASync<T>(CancellationToken token, string path, string contentType = "application/json")
        {
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

            using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();

                using (var streamReader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = new Newtonsoft.Json.JsonSerializer();
                        return serializer.Deserialize<T>(jsonReader)!;
                    }
                }
            }
        }

    }
}
