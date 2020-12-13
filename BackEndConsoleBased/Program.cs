using PuzzleGenerator.Models;
using PuzzleGenerator.Service;
using SudokuSolverLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndConsoleBased
{
    class Program
    {
        static void Main(string[] args)
        {
            //Generate easy puzzle
            SudokuPuzzle puzzle = new Generator().GenerateNewPuzzle(1);

            //Create new board
            Board solver = new Board();
            
            //Put puzzle into board
            foreach(Square sqr in puzzle.squares)
            {
                //Add 1 to X and Y (API starts at zero index, solving logic starts at 1 index)
                solver.SetSquareValue(sqr.x + 1, sqr.y + 1, sqr.value);
            }

            //Print Board again
            PrintBoardState(solver);

            Console.WriteLine("\n\n");

            //Try to Solve the puzzle
            solver.SolveEntireBoard();

            //Print Board again
            PrintBoardState(solver);

            //Wait for input
            Console.ReadLine();
            Console.ReadLine();
        }

        static void PrintBoardState(Board solver)
        {
            //Loop through the board
            for (int row = 1; row < 10; row++)
            {
                for (int column = 1; column < 10; column++)
                {
                    int value = solver.GetSquareValue(row, column);

                    if (value != 0)
                        Console.Write(string.Format("{0} ", value));
                    else
                        Console.Write("~ ");

                    //Write " | " between blocks
                    if (column == 3 || column == 6)
                        Console.Write("| ");
                }

                Console.WriteLine("");

                //Write "------------" between blocks
                if (row == 3 || row == 6)
                    Console.WriteLine("----------------------");

            }
        }
    }
}
