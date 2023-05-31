namespace BattleShipApiInteraction
{

    public class ApiConnection
    {
        private string ApiKey = "lprgi_api_key_2023";
        private string ApiURL = "https://api-lprgi.natono.biz/api/";
        private HttpClient Client { get; }


        public ApiConnection()
        {
            Client = GetHttpClient();
        }

        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(ApiURL);
            client.DefaultRequestHeaders.Add("x-functions-key", ApiKey);
            return client;
        }

        public async Task<string> Request(string url)
        {
            HttpResponseMessage response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
            return await response.Content.ReadAsStringAsync();
        }
    }
}
