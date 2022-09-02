namespace CodeEvents.Client.HttpClients
{
    public class CodeEventClient
    {
        private readonly HttpClient client;

        public CodeEventClient(HttpClient client)
        {
            this.client = client;
        }


    }
}
