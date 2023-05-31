﻿using BattleShipApiInteraction;
using BattleShipLogic.Domain.BattleShipConfig;
using Newtonsoft.Json;

namespace BattleShipLogic.Services
{
    public class ConfigRequester
    {
        private ApiConnection Connection { get; }
        public BattleShipGrid Grid { get; set; }

        public ConfigRequester()
        {
            Connection = new ApiConnection();
        }

        public async Task<BattleShipGrid> GetBattleShipGrid()
        {
            string config = await Connection.Request("GetConfig");
            return JsonConvert.DeserializeObject<BattleShipGrid>(config)!;
        }
    }
}
