namespace BattleShipApiInteraction
{

    public class ApiConnection
    {
        private const string API_URL = "https://api-lprgi.natono.biz/api/";
        private const string API_KEY = "lprgi_api_key_2023";
        private HttpClient Client { get; set; }


        public ApiConnection()
        {
            this.Client = GetHttpClient();
        }

        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(API_URL);
            client.DefaultRequestHeaders.Add("x-functions-key", API_KEY);
            return client;
        }

        public async Task<string> Request(string url)
        {
            HttpResponseMessage response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
            return await response.Content.ReadAsStringAsync();
        }
    }
}
