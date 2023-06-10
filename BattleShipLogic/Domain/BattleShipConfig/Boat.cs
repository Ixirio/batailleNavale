using Newtonsoft.Json;

namespace BattleShipLogic.Domain.BattleShipConfig
{
    public class Boat
    {
        [JsonProperty("taille")]
        internal int Size { get; set; }

        [JsonProperty("nom")]
        internal string Name { get; set; }

        internal int XPosition { get; set; }

        internal int YPosition { get; set; }

        internal char Direction { get; set; }

        internal bool IsDefeated { get; set; } = false;
    }
}
