using Newtonsoft.Json;

namespace BattleShipLogic.Domain.BattleShipConfig
{
    public class Boat
    {
        [JsonProperty("taille")]
        internal int Size { get; set; }

        [JsonProperty("nom")]
        internal string Name { get; set; }

        public override string ToString()
        {
            return $"Size: {this.Size}, Name: {this.Name}";
        }
    }
}
