using System;
using MinesweeperClassLibrary;

namespace Milestone1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Board board = new Board(10);
            board.SetupLiveNeighbors(board);
            board.CalculateLiveNeighbors(board);

            
             /*
              A while loop runs as long as the game is not over. Hitting
              a mine will end the game along with finding all the non-mine squares
              and making them visible. User attempts a valid row and column. If a game
              ending move is not played than the board will be printed showing the updated
              visible square and the while loop will continue until the game ends. 
              */
             while(!board.GetGameOver())
             {
                
                Cell attemptedSquare = board.UserCheckSquare(board);
                board.FloodFill(attemptedSquare.GetRow(attemptedSquare), attemptedSquare.GetColumn(attemptedSquare));
                if (attemptedSquare.IsCellLive(attemptedSquare))
                {
                    Console.WriteLine("You Hit a Mine! GAME OVER...");
                    board.SetGameOver(board);
                }

                if (board.CheckVisitedSquaresLeft(board))
                {
                    Console.WriteLine("There are no squares left. You Won!!!!!!");
                    board.SetGameOver(board);
                }

                PrintBoardDuringGame(board);

             }
        }

        // Method sets the gameOver property to true. 
        public bool SetGameOver()
        {
            return true;
        }

        /*
         * Method prints the board game. Cells that have not been visited
         * are shown with a ? while visted squares are shown with an empty space. 
         * An E would indicate an unexpected outcome. 
         */
        public static void PrintBoardDuringGame(Board theBoard)
        {
            for (int i = 0; i < theBoard.GetSize(); i++)
            {
                for (int j = 0; j < theBoard.GetSize(); j++)
                {
                    Cell c = theBoard.GetCell(i, j);

                    if (c.GetCellVisited(c) == false)
                    {
                        Console.Write("?\t");
                    }
                    else if (c.GetCellVisited(c) == true)
                    {
                        Console.Write(" \t");
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
