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

        /// <summary>
        /// Récupère le client HTTP pour requeter l'api
        /// </summary>
        /// <returns></returns>
        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(API_URL);
            client.DefaultRequestHeaders.Add("x-functions-key", API_KEY);
            return client;
        }

        /// <summary>
        /// Effectue une requete sur l'url donnée et retourne la réponse
        /// </summary>
        /// <param name="url"></param>
        public async Task<string> Request(string url)
        {
            HttpResponseMessage response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
            return await response.Content.ReadAsStringAsync();
        }
    }
}
