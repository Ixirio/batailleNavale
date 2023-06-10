using BattleShipLogic.Services;
using Newtonsoft.Json;

namespace BattleShipLogic.Domain.BattleShipConfig
{
    public class BattleShipGrid
    {
        [JsonProperty("nbLignes")]
        internal int RowCount { get; set; }

        [JsonProperty("nbColonnes")]
        internal int ColumnCount { get; set; }

        [JsonProperty("bateaux")]
        internal List<Boat> Boats { get; set; }

        internal char[,] Grid { get; set; }

        internal char[,] ShootGrid { get; set; }

        internal GridDrawer GridDrawer = new GridDrawer();

        internal const char GRID_EMPTY_CHAR = '-';

        internal const char GRID_BOAT_CHAR = '#';

        internal const char GRID_BOAT_TOUCHED_CHAR = 'O';

        internal const char GRID_MISSED_CHAR = 'X';

        /// <summary>
        /// Initialise les grilles pour la bataille navale
        /// </summary>
        private void InitGrid()
        {
            this.Grid = new char[this.RowCount, this.ColumnCount];

            for (int rowCount = 0; rowCount < this.RowCount; rowCount++)
            {
                for (int columnCount = 0; columnCount < this.ColumnCount; columnCount++)
                {
                    this.Grid[rowCount, columnCount] = GRID_EMPTY_CHAR;
                }
            }

            this.ShootGrid = new char[this.RowCount, this.ColumnCount];

            for (int rowCount = 0; rowCount < this.RowCount; rowCount++)
            {
                for (int columnCount = 0; columnCount < this.ColumnCount; columnCount++)
                {
                    this.ShootGrid[rowCount, columnCount] = GRID_EMPTY_CHAR;
                }
            }
        }

        public void DrawGrids()
        {
            Console.Clear();

            this.GridDrawer.Draw(this.ShootGrid);

            Console.WriteLine(new string('-', this.Grid.Length));

            this.GridDrawer.Draw(this.Grid);
        }

        /// <summary>
        /// Demande au joueur de placer le bateau sur la grille
        /// </summary>
        public void PlaceBoatsOnGrid()
        {
            this.InitGrid();

            foreach (Boat boat in this.Boats)
            {
                Console.Clear();

                this.GridDrawer.Draw(this.Grid);

                Console.WriteLine($"Place the boat {boat.Name} on the grid, length : {boat.Size}\n");

                bool errorOnPosition = true;
                while (errorOnPosition)
                {
                    Console.WriteLine("Enter X coordinate");
                    int boatx;
                    while (!int.TryParse(Console.ReadLine(), out boatx))
                    {
                        Console.WriteLine("Invalid input. Please enter an integer for the X coordinate.");
                    }

                    Console.WriteLine("Enter Y coordinate");
                    int boaty;
                    while (!int.TryParse(Console.ReadLine(), out boaty))
                    {
                        Console.WriteLine("Invalid input. Please enter an integer for the Y coordinate.");
                    }

                    boatx--; boaty--;

                    char boatDirection = '0';
                    while (boatDirection != 'H' && boatDirection != 'V')
                    {
                        Console.WriteLine("Place the boat horizontally or vertically ? (H/V)");
                        boatDirection = char.ToUpper(Console.ReadLine().Trim()[0]);
                    }

                    if (this.CanPlaceBoat(boat, boatDirection, boatx, boaty))
                    {
                        errorOnPosition = false;

                        boat.XPosition = boatx;
                        boat.YPosition = boaty;
                        boat.Direction = boatDirection;

                        for (int i = 0; i < boat.Size; i++)
                        {
                            if (boatDirection == 'H')
                            {
                                this.Grid[boaty, boatx + i] = GRID_BOAT_CHAR;
                            } 
                            else if (boatDirection == 'V')
                            {
                                this.Grid[boaty + i, boatx] = GRID_BOAT_CHAR;
                            }
                        }
                    }
                    else
                    {
                        errorOnPosition = true;
                        Console.WriteLine("Error, can't place the boat here");
                    }
                }
            }


            Console.Clear();
            this.GridDrawer.Draw(this.Grid);

            Console.WriteLine("Here is your final grid, press any key to continue");
            Console.ReadLine();
        }

        /// <summary>
        /// Verifie si le bateau peut etre placé au coordonées données 
        /// </summary>
        /// <param name="boat"></param>
        /// <param name="boatDirection"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private bool CanPlaceBoat(Boat boat, char boatDirection, int x, int y)
        {
            // Vérifie si le bateau dépasse les limites de la grille
            if (boatDirection == 'H')
            {
                if (x + boat.Size > this.RowCount) return false;
            }
            else if (boatDirection == 'V')
            {
                if (y + boat.Size > this.ColumnCount) return false;
            }

            // Vérifie s'il y a des bateaux adjacents
            for (int i = 0; i <= boat.Size; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (y + i >= 0 && y + i < this.RowCount && x + j >= 0 && x + j < this.ColumnCount)
                    {
                        if (this.Grid[y + i, x + j] == GRID_BOAT_CHAR) return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Retourne un bateau selon les coordonnées renseignées
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Boat? FindBoat(int x, int y)
        {
            foreach (Boat boat in this.Boats)
            {
                if (boat.XPosition == x && boat.YPosition == y) return boat;

                for (int i = 0; i < boat.Size; i++)
                {
                    if (boat.Direction == 'H' && boat.XPosition == x && boat.YPosition + i == y) return boat;

                    if (boat.Direction == 'V' && boat.XPosition + i == x && boat.YPosition == y) return boat;
                }
            }

            /* Au cas ou mais c'est pas sensé arriver */
            return null;
        }

        /// <summary>
        /// Parcour la grille pour savoir si le bateau passé en paramètre est coulé
        /// </summary>
        /// <param name="boat"></param>
        public bool IsBoatDefeated(Boat? boat)
        {
            if (boat == null) return false;

            for (int i = 0; i < boat.Size; i++)
            {
                if (boat.Direction == 'H' && this.Grid[boat.YPosition + i, boat.XPosition] == GRID_BOAT_CHAR) return false;

                if (boat.Direction == 'V' && this.Grid[boat.YPosition, boat.XPosition + i] == GRID_BOAT_CHAR) return false;
            }

            boat.IsDefeated = true;
            return true;
        }

        /// <summary>
        /// Retoure vrai si les bateaux sur la grille sont tous coulés sinon faux
        /// </summary>
        public bool HasLost()
        {
            foreach (Boat boat in this.Boats)
            {
                if (!boat.IsDefeated) return false;
            }
            return true;
        }
    }
}
