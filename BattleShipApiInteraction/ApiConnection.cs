using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipApiInteraction
{

    internal class ApiConnection
    {
        private string apiKey = "lprgi_api_key_2023";
        private string apiURL = "https://apilprgi.natono.biz/api";


        public HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(apiURL);

            return client;
        }

    }
}
