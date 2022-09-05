using CodeEvents.Common.Dto;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;


namespace CodeEvents.Client.HttpClients
{
    public class CodeEventClient : BaseClient, ICodeEventClient
    {

        public CodeEventClient(HttpClient client) : base(client, new Uri("https://localhost:7150"))
        {

            HttpClient.Timeout = new TimeSpan(0, 0, 30);
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<CodeEventDto>?> GetCodeEvents(CancellationToken token)
        {
            return await GetASync<IEnumerable<CodeEventDto>>(token, "api/events");
        }

        public async Task<CodeEventDto?> GetCodeEvent(string name, CancellationToken token)
        {
            return await GetASync<CodeEventDto>(token, $"api/events/{name}");
        }

        public async Task<LectureDto?> GetLecture(string name, int id, CancellationToken token)
        {
            return await GetASync<LectureDto>(token, $"api/events/{name}/lectures/{id}");
        }


    }
}
