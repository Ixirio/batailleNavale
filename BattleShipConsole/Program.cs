using BattleShipLogic;

namespace BattleShipConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            game.CreatePlayers();
        }
    }
}