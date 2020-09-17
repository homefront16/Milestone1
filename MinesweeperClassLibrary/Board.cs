using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;

namespace MinesweeperClassLibrary
{
    public class Board
    {
        private int Size { get; set; }
        private Cell[,] Grid { get; set; }
        private double Difficulty { get; set; } = .5;
        private Cell cell { get; set; }




        // Constructor
        public Board(int size)
        {
            // Initial size of the board is defined here
            this.Size = size;

            // Creates a new 2D array of type cell.
            this.Grid = new Cell[size, size];


            // Filling the 2D array with New Cells
            // Each will have a unique row and column
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j] = new Cell(i, j, false, false, 0);

                }
            }

        }

        public void SetDifficulty()
        {
            this.Difficulty = .12;

        }

        // Method generates a random integer between 0 and the max size of the array
        public int GenerateRandomPlace()
        {
            Random r = new Random();
            int rInt = r.Next(0, Size);

            return rInt;
        }

        // Method will set up mines in random locations throughout the Grid.
        // Difficulty is set to 12% currently but can be changed which increases total mines
        public void SetupLiveNeighbors(Board theBoard)
        {
            // total mines is determined by the size of grid multiplied by the difficulty percentage
            int totalMines = (int)(Size * Difficulty);
            Console.WriteLine(totalMines);

            // While loop will run until there are no mines left
            // each iteration that complete decrements the totalMines variable
            while (totalMines > 0)
            {
                // the i and j locations are generated each time randomly
                // they are within the size of the board to avoid out of bounds exception
                for (int i = GenerateRandomPlace(); i < theBoard.Size;)
                {
                    for (int j = GenerateRandomPlace(); j < theBoard.Size;)
                    {
                        // This while loop is for catching events where a mine is about 
                        // to be placed at the location that a previous mine already exist
                        // The while loop will keep generating new locations until it's a non live (no mine) grid location
                        while (Grid[i, j].IsCellLive(Grid[i, j]))
                        {
                            i = GenerateRandomPlace();
                            j = GenerateRandomPlace();
                        }
                        Grid[i, j].SetCellToLive(Grid[i, j]);
                        totalMines--;
                        break;

                    }
                    break;
                }

            }

        }

        /*Finds how many live mines are next to the cell.
        sets value of neighbors which is the number of neighboring mines
        Traverses through each grid location adjacent to the current Cell
        if a mine is located neighbor is incremeneted for that Cell object. */
        public void CalculateLiveNeighbors(Board theBoard)
        {
            /*these arrays hold the x and y values for adjacent positions
            around the selected grid location. They will act as "square" around
            the selected grid location to check for mines*/
            int[] offSetX = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] offSetY = { -1, -1, 0, 1, 1, 1, 0, -1 };

            // Double for loop to iterate though every grid location
            for (int x = 0; x < theBoard.Size; x++)
            {
                for (int y = 0; y < theBoard.Size; y++)
                {
                    /*Third for loop is to use the two offset int arrays to check 
                    adjacent grid locations for mines. If a mine is found the 
                    Neighbor property of Cells is incremented.*/
                    for (int i = 0; i < 8; i++)
                    {
                        int nx = x + offSetX[i];
                        int ny = y + offSetY[i];

                        // The first if statement catches times where the current
                        // for loop is checking a location that is out of bounds
                        if (nx < 0 || nx > Size - 1 || ny < 0 || ny > Size - 1)
                        {
                            continue;
                        }
                         /*This if statment checks to see if the current grid location
                         is holding a mine and then increments the Neighbor property if true.
                         True = mine is present.*/
                        if (Grid[ny, nx].IsCellLive(Grid[ny, nx]) == true)
                        {
                            Grid[y, x].IncrementNeighbors(Grid[y, x]);
                        }

                    }
                }
            }

        }

        // Method will print cells in matrix form. 
        // The N's represent no mine
        // The L's represent live mine
        public void PrintCells(Board theBoard)
        {
            for (int i = 0; i < theBoard.Size; i++)
            {
                for (int j = 0; j < theBoard.Size; j++)
                {
                    Cell c = theBoard.Grid[i, j];

                    if (c.IsCellLive(c) == false)
                    {
                        Console.Write("N\t");
                    }
                    else if (c.IsCellLive(c) == true)
                    {
                        Console.Write("L\t");
                    }
                    else
                    {
                        Console.Write("E");
                    }


                }
                Console.WriteLine();
            }
            Console.WriteLine("==========================================================");
        }

        // Method will print the value of Cell neighbors in matrix form. 
        // A number value represents he number of neighbors for that Cell. 
        // The L's represents a live mine. E is for an error or unexpected result.
        public void PrintNeighbors(Board theBoard)
        {
            for (int i = 0; i < theBoard.Size; i++)
            {
                for (int j = 0; j < theBoard.Size; j++)
                {
                    Cell c = theBoard.Grid[i, j];

                    if (c.IsCellLive(c) == false)
                    {
                        Console.Write(Grid[i, j].GetNeighbors(c) + "\t");
                    }
                    else if (c.IsCellLive(c) == true)
                    {
                        Console.Write("L\t");
                    }
                    else
                    {
                        Console.Write("E");
                    }


                }
                Console.WriteLine();
            }
            Console.WriteLine("==========================================================");
        }
    }




}
