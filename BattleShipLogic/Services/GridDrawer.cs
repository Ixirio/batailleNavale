using System;
namespace BattleShipLogic.Services
{
    internal class GridDrawer
    {
        public GridDrawer Draw(char[,] grid)
        {

            Console.Write("  ");

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                Console.Write((char)('A' + i) + " ");
            }

            Console.WriteLine();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                Console.Write(i + 1 + " ");

                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write($"{grid[i, j]} ");
                }

                Console.WriteLine();
            }

            return this;
        }
    }
}
