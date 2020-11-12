/*  Author: Raymond Popsie
 *  Date: 9/12/2020
 *  File Name: Board.cs
 *  Purpose: This class will contain the 2D array of Cell Objects which 
 *  create the board. It will hold the size of the board, difficulty of the game,
 *  and the GameOver boolean property. Its purpose is to create the board, 
 *  populate mines, print board, and any other logic required for the user to 
 *  handle the Board.
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;

namespace MinesweeperClassLibrary
{
    public class Board
    {
        private int Size { get; set; }
        private Cell[,] Grid { get; set; }
        private double Difficulty { get; set; } = .5;
        private bool GameOver { get; set; } = false;




        // Constructor
        public Board(int size)
        {
            // Initial size of the board is defined here.
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

        // Returns the value of the GameOver property of Board
        public bool GetGameOver()
        {
            return GameOver;
        }

        // Returns the value of the GameOver property of Board
        public void SetGameOver(Board theBoard)
        {
            theBoard.GameOver = true;
        }

        // Returns the Size property of Board
        public int GetSize()
        {
            return Size;
        }

        // Returns the value of the Cell given the row and column of the grid
        public Cell GetCell(int row, int column)
        {
            return Grid[row, column];
        }

        // Sets the difficulty of the game
        public void SetDifficulty(double difficulty)
        {
            this.Difficulty = difficulty;

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
        if a mine is located neighbor is incremented for that Cell object. */
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

        /// <summary>
        /// This method will loop through the board to see if there 
        /// are any visible cells left. It increments each time a Cell
        /// is found with the Visted property value as false. This indicates
        /// that the game can continue. The method will then return a boolean 
        /// value of false unless there are no Cells left that show a Visted Property 
        /// of false. 
        /// </summary>
        /// <param name="theBoard"></param>
        /// <returns>Boolean value of false unless there are Cells that can still be 
        /// vistedAN</returns>
        public bool CheckVisitedSquaresLeft(Board theBoard)
        {
            int notVistedCount = 0;
            bool noCellsToVisit = false;

            for (int i = 0; i < theBoard.Size; i++)
            {
                for (int j = 0; j < theBoard.Size; j++)
                {
                    Cell c = theBoard.Grid[i, j];

                    if (c.IsCellLive(c) == true)
                    {
                        continue;
                    }
                    else if (c.GetCellVisited(c) == false)
                    {
                        notVistedCount++;
                    }
                    else { }
                }
            }

            if(notVistedCount == 0)
            {
                noCellsToVisit = true;
            }

            return noCellsToVisit;
        }

        // Method will print the value of Cell neighbors in matrix form. 
        // A number value represents he number of neighbors for that Cell. 
        // The L's represents a live mine. E is for an error or unexpected result.
        // here we go 
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
                        Console.Write("X\t");
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





        /// <summary>
        /// Prompts user to make a row and column selection. A try catch block is implemented to
        /// catch possible exceptions.A default value of 1 for row and 1 for column is used if 
        ///  the user is unable to select a valid row and column. 
        /// </summary>
        /// <param name="theBoard"></param>
        /// <returns>A Cell object</returns>
        public Cell UserCheckSquare(Board theBoard)
        {
            int currentRow = 0;
            int currentCol = 0;

            /*using a try catch for improper user input. The user will have an opportunities
              to place the correct input. If the user does not enter the correct input the error
              will be caught. This will prevent errors and will prevent the user from being in 
              an endless loop asking for the correct input.*/
            try
            {
                Console.WriteLine("Enter the row number you would like to attempt");
                currentRow = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the column number you would like to attempt");
                currentCol = int.Parse(Console.ReadLine());
                theBoard.Grid[currentRow, currentCol].SetCellToVisited(theBoard.Grid[currentRow, currentCol]);

                return theBoard.Grid[currentRow, currentCol];
            }
            /*Most common exception is choosing a number out of range which is caught here. 
            The user will have a second opportunity to choose the correct input. */
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Please ensure you enter a number between 0 and 7");

            }
            catch (FormatException)
            {
                Console.WriteLine("That is not a valid number. Please ensure that you are entering a number between +" +
                    "0 and 7");
            }

            catch (Exception)
            {
                Console.WriteLine("Not Valid. Setting default to 1, 1");
            }

            /*Checks to ensure the entered amounts are within the grid. If not default values of 1, 1 
            are entered. */
            if (currentRow < 0 || currentRow > theBoard.Size || currentCol < 0 || currentCol > theBoard.Size)
            {
                Console.WriteLine("Not Valid. Setting default to 1, 1");
                currentRow = 1;
                currentCol = 1;
            }

            theBoard.Grid[currentRow, currentCol].SetCellToVisited(theBoard.Grid[currentRow, currentCol]);

            return theBoard.Grid[currentRow, currentCol];
        }
        
        /// <summary>
        /// This method checks to ensure the given row and column are 
        /// within the Grid.  
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>True if given row and column are within the Grid</returns>
        public bool LimitCheck(int row, int col)
        {
            if(row < 0 || row > Size - 1 || col < 0 || col > Size - 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// A  Recursive method that checks the adjacent Cells around
        /// the given row and column for bombs. If no bomb is found the 
        /// value of the Cell is changed to visted. The method is called 
        /// recursively until either the row and column are out of bounds or 
        /// a bomb is found adjacent to the row and column
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>False when row and col are out of bounds or a bomb is found 
        /// adjacent to the row and column</returns>
        public bool FloodFill(int row, int col)
        {
    

          /*these arrays hold the x and y values for adjacent positions
            around the selected grid location. They will act as "square" around
            the selected grid location to check for mines*/
            int[] offSetX = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] offSetY = { -1, -1, 0, 1, 1, 1, 0, -1 };

                // Check which Cells have no bombs next to them
                for (int i = 0; i < 8; i++)
                {
                    int nx = row + offSetX[i];
                    int ny = col + offSetY[i];
                    
                    // checking to make sure location is within the Grid
                    if (!LimitCheck(nx, ny))
                    {
                        continue;
                    }

                    // If a bomb is found adjacent to the current location boolean returns false
                    /*if (Grid[ny, nx].IsCellLive(Grid[ny, nx]) == true)
                    {
                        return false;
                    }*/
                    if(Grid[ny, nx].GetNeighbors(Grid[ny, nx]) > 0)
                    {
                        return false;
                    }


                    // Mark Cell as visted when they are included in the block of affected cells
                    Grid[ny, nx].SetCellToVisited(Grid[ny, nx]);
                }
                
                /*Conditional statements increment or decrement the row 
                toward the center of board. It also ensures that 
                the given row and column are not out of bounds
                if its out of bounds boolean returns false*/
                if(row < 4 && col < 4 && LimitCheck(row, col))
                {
                    return FloodFill(row + 1, col + 1);
                }
                else if (row > 4 && col > 4 && LimitCheck(row, col))
                {
                    return FloodFill(row - 1, col - 1);
                }
                else if(row < 4 && col > 4 && LimitCheck(row, col))
                {
                    return FloodFill(row + 1, col - 1);
                }
                else if(row > 4 && col < 4 && LimitCheck(row, col))
                {
                    return FloodFill(row - 1, col + 1);
                }
                else
                {
                    return false;
                }
        }

        public void FloodFill2(Board theBoard, int row, int col)
        {

            if(LimitCheck(row, col) && Grid[row, col].GetCellVisited(Grid[row, col]) == false)
            {
                Grid[row, col].SetCellToVisited(Grid[row, col]);

                if(Grid[row, col].GetNeighbors(Grid[row, col]) == 0)
                {
                    FloodFill2(theBoard, row - 1, col - 1);
                    FloodFill2(theBoard, row - 1, col);
                    FloodFill2(theBoard, row - 1, col + 1);
                    FloodFill2(theBoard, row, col - 1);
                    FloodFill2(theBoard, row, col + 1);
                    FloodFill2(theBoard, row + 1, col - 1);
                    FloodFill2(theBoard, row + 1, col);
                    FloodFill2(theBoard, row + 1, col + 1);
                }
            }

        }

     /*   public int CheckForAdjacentMines(int row, int col)
        {
            int mineCount = 0;
            *//*these arrays hold the x and y values for adjacent positions
            around the selected grid location. They will act as "square" around
            the selected grid location to check for mines*//*
            int[] offSetX = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] offSetY = { -1, -1, 0, 1, 1, 1, 0, -1 };

            // Check which Cells have no bombs next to them
            for (int i = 0; i < 8; i++)
            {
                int nx = row + offSetX[i];
                int ny = col + offSetY[i];

                // checking to make sure location is within the Grid
                if (!LimitCheck(nx, ny))
                {
                    continue;
                }

                if (Grid[nx, ny].GetNeighbors(Grid[nx, ny]) > 0)
                {
                    mineCount++;
                }
            }
            return mineCount;
        }*/
    }

  




}
