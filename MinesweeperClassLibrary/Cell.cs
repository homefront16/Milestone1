using System;

namespace MinesweeperClassLibrary
{
    public class Cell
    {
        private int Row { get; set; } = -1;
        private int Column { get; set; } = -1;
        private int Neighbors { get; set; } = 0;
        private bool Visted { get; set; } = false;

        private bool Live { get; set; } = false;

        public Cell(int row, int column, bool visited, bool live, int neighbors)
        {
            this.Row = row;
            this.Column = column;
            this.Neighbors = neighbors;
            this.Visted = visited;
            this.Live = live;
        }

        public Cell(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        // Sets cell Live value to true which represents a mine being present
        public void SetCellToLive(Cell c)
        {
            c.Live = true;
        }

        // Method checks to see if cell is live == a mine is present.
        // returns a bool value true = mine, false = no mine.
        public bool IsCellLive(Cell c)
        {
            bool isLive = false;
            if(c.Live == true)
            {
                isLive = true;
            }
            return isLive;
        }

 
        // Method increments the number of neighbors a Cell has
        public void IncrementNeighbors(Cell c)
        {
            c.Neighbors += 1;
        }

        // Method returns the neighbor value of the Cell
        public int GetNeighbors(Cell c)
        {
            return c.Neighbors;
        }

    }
}
