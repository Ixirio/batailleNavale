﻿using BattleShipLogic.Domain.BattleShipConfig;

namespace BattleShipLogic.Domain.Game
{
    public class Player
    {

        internal string Name { get; set; }

        internal BattleShipGrid BattleShipGrid { get; set; }

        internal Player Oponent { get; set; }

        public Player(string name, BattleShipGrid battleShipGrid)
        {
            this.Name = name;
            this.BattleShipGrid = battleShipGrid;
        }

        public void AskPlayerToPlaceBoats()
        {
            Console.WriteLine($"{this.Name} it's time to place boats on the grid !");

            this.BattleShipGrid.PlaceBoatsOnGrid();
        }

        public void AskPlayerToPlay()
        {
            this.BattleShipGrid.DrawGrids();

            Console.WriteLine($"{this.Name} it's your turn.");

            Console.WriteLine("Enter target X coordinate");
            int targetx;
            while (!int.TryParse(Console.ReadLine(), out targetx))
            {
                this.BattleShipGrid.DrawGrids();

                Console.WriteLine("Invalid input. Please enter an integer for the X coordinate.");
            }

            Console.WriteLine("Enter target Y coordinate");
            int targety;
            while (!int.TryParse(Console.ReadLine(), out targety))
            {
                this.BattleShipGrid.DrawGrids();

                Console.WriteLine("Invalid input. Please enter an integer for the Y coordinate.");
            }

            targetx--; targety--;

            bool result = this.AttackOponent(targetx, targety);
            while (!result)
            {
                this.BattleShipGrid.DrawGrids();

                Console.WriteLine("You already tried theses coordinates, try again");
                result = this.AttackOponent(targetx, targety);
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        public bool AttackOponent(int x, int y)
        {
            if (this.BattleShipGrid.ShootGrid[x, y] != BattleShipGrid.GRID_EMPTY_CHAR) return false;

            if (this.Oponent.BattleShipGrid.Grid[x, y] == BattleShipGrid.GRID_BOAT_CHAR)
            {
                this.BattleShipGrid.ShootGrid[x, y] = BattleShipGrid.GRID_BOAT_TOUCHED_CHAR;
                this.Oponent.BattleShipGrid.Grid[x, y] = BattleShipGrid.GRID_BOAT_TOUCHED_CHAR;
                Console.WriteLine($"Boat hit {(this.Oponent.BattleShipGrid.IsBoatDefeated(this.Oponent.BattleShipGrid.FindBoat(x, y)) ? "and sunk " : "")}!");
                return true;
            }
            else
            {
                this.BattleShipGrid.ShootGrid[x, y] = BattleShipGrid.GRID_MISSED_CHAR;
                Console.WriteLine("Miss ...");
                return true;
            }
        }

        public bool HasPlayerLost()
        {
            return this.BattleShipGrid.HasLost();
        }
    }
}
