using System;
using MinesweeperClassLibrary;

namespace Milestone1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Board board = new Board(10);
            Console.WriteLine("Hello!");

            board.SetupLiveNeighbors(board);
            board.PrintCells(board);
            board.CalculateLiveNeighbors(board);
            board.PrintNeighbors(board); 

        }
    }
}
