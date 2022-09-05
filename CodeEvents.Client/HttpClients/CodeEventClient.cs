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


    }
}
