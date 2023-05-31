using BattleShipLogic.Domain.BattleShipConfig;
using BattleShipLogic.Domain.Game;
using BattleShipLogic.Services;

namespace BattleShipLogic
{
    public class Game
    {
        Player Player1 { get; set; }

        Player Player2 { get; set; }

        ConfigRequester ConfigRequester { get; set; }

        public Game()
        {
            ConfigRequester = new ConfigRequester();
        }

        public void CreatePlayers()
        {
            Task<BattleShipGrid> player1Task = ConfigRequester.GetBattleShipGrid();
            Task<BattleShipGrid> player2Task = ConfigRequester.GetBattleShipGrid();

            Task.WaitAll(player1Task, player2Task);

            string name = "";
            Console.WriteLine("Creating first player... What's your name ?");
            name = Console.ReadLine()!;
            Player1 = new Player(name, player1Task.Result);

            Console.WriteLine("Creating second player... What's your name ?");
            name = Console.ReadLine()!;
            Player2 = new Player(name, player2Task.Result);

            Player1.AskPlayerToPlaceBoats();

        }

    }
}
