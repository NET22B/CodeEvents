using CodeEvents.Common.Dto;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;


namespace CodeEvents.Client.HttpClients
{
    public class CodeEventClient
    {
        private readonly HttpClient client;

        public CodeEventClient(HttpClient client)
        {
            this.client = client;
            this.client.BaseAddress = new Uri("https://localhost:7150");
            this.client.Timeout = new TimeSpan(0, 0, 30);
            this.client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<CodeEventDto?>> GetWithRequestMessage()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/events");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            IEnumerable<CodeEventDto> codeEvents;

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using(var jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = new Newtonsoft.Json.JsonSerializer();
                        codeEvents = serializer.Deserialize<IEnumerable<CodeEventDto>>(jsonReader)!;
                    }
                }

            }

            return codeEvents!;
        }


    }
}
