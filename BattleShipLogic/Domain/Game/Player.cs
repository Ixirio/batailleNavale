using BattleShipLogic.Domain.BattleShipConfig;

namespace BattleShipLogic.Domain.Game
{
    public class Player
    {

        internal string Name { get; set; }

        internal BattleShipGrid BattleShipGrid { get; set; }

        public Player(string name, BattleShipGrid battleShipGrid)
        {
            this.Name = name;
            this.BattleShipGrid = battleShipGrid;
        }

        public void AskPlayerToPlaceBoats()
        {
            this.BattleShipGrid.PlaceBoatsOnGrid();
        }

    }
}
