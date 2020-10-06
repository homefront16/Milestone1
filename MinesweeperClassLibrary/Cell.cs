/*  Author: Raymond Popsie
 *  Date: 9/12/2020
 *  File Name: Cell.cs
 *  Purpose: This class is to set properties for a Cell object. 
 *  The cell object will contain a row, column, number of neighboring
 *  mines (Neighbors), Cells Visted, and mines present. The class will 
 *  be used by the board class to create a board of Cell objects. 
 */

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

        // Sets the Visted property of cell to true
        public void SetCellToVisited(Cell c)
        {
             c.Visted = true;
        }
        // Get the value of the Visted property of cell
        public bool GetCellVisited(Cell c)
        {
            return c.Visted;
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
