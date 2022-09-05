using CodeEvents.Client.HttpClients;
using CodeEvents.Client.Models;
using CodeEvents.Common.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CodeEvents.Client.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient httpClient;
        private const string json = "application/json";
        private readonly IHttpClientFactory httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory, CodeEventClient codeEventClient)
        {
           
           //httpClient = new HttpClient();
           //httpClient.BaseAddress = new Uri("https://localhost:7150");
            this.httpClientFactory = httpClientFactory;


            httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7150");
           // httpClient = httpClientFactory.CreateClient("CodeEventClient");
            // httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index()
        {
            //var res = await SimpleGet();
            //var res = await GetWithRequestMessage();
            //var res = await CreateLecture();
            var res = await PatchCodeEvent();

            return View();
        }

        private async Task<CodeEventDto> PatchCodeEvent()
        {

             var patchDokument = new JsonPatchDocument<CodeEventDto>();
            patchDokument.Remove(e => e.LocationAddress);
            patchDokument.Replace(e => e.LocationCityTown, "Stockholm");
            patchDokument.Add(e => e.LocationCountry, "Sweden");

            var serializedDto = JsonConvert.SerializeObject(patchDokument);

            var request = new HttpRequestMessage(HttpMethod.Patch, "api/events/Gruppen1");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));
            request.Content = new StringContent(serializedDto);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var codeEvent = JsonSerializer.Deserialize<CodeEventDto>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return codeEvent!;
        }

        private async Task<LectureDto> CreateLecture()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/events/Gruppen1/lectures");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            var lecture = new LectureCreateDto
            {
                Level = 500,
                Title = "From Client"
            };

            var serializedLecture = JsonSerializer.Serialize(lecture);

            request.Content = new StringContent(serializedLecture);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(json);

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var codeEvents = JsonSerializer.Deserialize<LectureDto>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return codeEvents!;

        }

        private async Task<IEnumerable<CodeEventDto?>> GetWithRequestMessage()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/events");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var codeEvents = JsonSerializer.Deserialize<IEnumerable<CodeEventDto>>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return codeEvents!;
        }

        private async Task<IEnumerable<CodeEventDto?>> SimpleGet()
        {
            var response = await httpClient.GetAsync("api/events/?includelectures=true");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var codeEvents = JsonSerializer.Deserialize<IEnumerable<CodeEventDto>>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            
            return codeEvents!;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}