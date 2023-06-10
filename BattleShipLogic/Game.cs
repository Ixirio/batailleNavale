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

            Console.WriteLine("Creating first player... What's your name ?");
            this.Player1 = new Player(Console.ReadLine()!, player1Task.Result);
            this.Player1.AskPlayerToPlaceBoats();

            Console.Clear();

            Console.WriteLine("Creating second player... What's your name ?");
            this.Player2 = new Player(Console.ReadLine()!, player2Task.Result);
            this.Player2.AskPlayerToPlaceBoats();

            this.Player1.Oponent = this.Player2;
            this.Player2.Oponent = this.Player1;
        }

        public void Play()
        {
            while (true)
            {
                this.Player1.AskPlayerToPlay();

                if (this.Player2.HasPlayerLost())
                {
                    Console.WriteLine($"{this.Player1.Name} has won !");
                    break;
                }

                this.Player2.AskPlayerToPlay();

                if (this.Player1.HasPlayerLost())
                {
                    Console.WriteLine($"{this.Player2.Name} has won !");
                    break;
                }
            }
        }
    }
}
