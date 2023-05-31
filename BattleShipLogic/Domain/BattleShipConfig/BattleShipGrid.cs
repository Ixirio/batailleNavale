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

        internal string[,] Grid { get; set; }

        internal string[,] GridChars { get; set; }

        public void InitGrid()
        {
            this.Grid = new string[this.RowCount, this.ColumnCount];

            for (int rowCount = 0; rowCount < this.RowCount; rowCount++)
            {
                for (int columnCount = 0; columnCount < this.ColumnCount; columnCount++)
                {
                    this.Grid[rowCount, columnCount] = "W";
                }
            }

            this.GridChars = new string[this.RowCount, this.ColumnCount];

            string[] colChars = new string[this.ColumnCount];

            for (int colCount = 0; colCount < this.ColumnCount; colCount++)
            {
                colChars[colCount] = colCount.ToString();
            }


            string[] rowChars = new string[this.RowCount];
            for (int rowCount = 0; rowCount < this.RowCount; rowCount++)
            {
                rowChars[rowCount] = ((char)('A' + rowCount)).ToString();
            }


        }

        private void DrawGrid()
        {
            if (this.Grid is null) { this.InitGrid(); }

            for (int rowCount = 0; rowCount < this.RowCount; rowCount++)
            {
                Console.WriteLine(" 0123456789");
                Console.WriteLine(" ");
                for (int columnCount = 0; columnCount < this.ColumnCount; columnCount++)
                {
                    Console.Write(this.Grid[rowCount, columnCount]);
                }

                Console.WriteLine("");
            }

        }

        public void PlaceBoatsOnGrid()
        {
            foreach (Boat boat in this.Boats)
            {
                Console.WriteLine($"Place the boat {boat.Name} on the grid");



                this.DrawGrid();
            }
        }

        public override string ToString()
        {
            return $"RowCount : {this.RowCount}, ColumnCount : {this.ColumnCount}, Boats :{this.Boats}";
        }
    }
}
