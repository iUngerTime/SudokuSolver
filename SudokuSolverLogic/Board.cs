using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverLogic
{
    public class Board
    {
        private List<Square> Squares { get; set; }

        public Board()
        {
            Squares = new List<Square>();

            for (int row = 1; row < 10; row++)
            {
                for (int column = 1; column < 10; column++)
                {
                    Squares.Add(new Square(row, column));
                }
            }
        }

        /// <summary>
        /// This will brute force solve the board using the information it already knows
        /// </summary>
        public void ForceSudokuSolve()
        {
            ////////////////////////
            //tentative algorithim//
            ////////////////////////
            
            //While(not finished)
            //{
            //  Duplicate board
            //  Get square with smallest amount of potential values
            //  Try to solve with that potential value
            //  if resulting board is INVALID then remove that potential value from that square
            //}

            /*NOTES
             * This needs a good function to determine if board becomes invalid!
             * How will this be done?
             */
        }

        /// <summary>
        /// Sudoku Requires at least 17 minimum squares to be solvable. This determines if the board has enough squares solved
        /// </summary>
        /// <returns>True if has 17 or more, false if not.</returns>
        public bool HasMinimumSolvableSquares()
        {
            bool solvable = false;
            int numSolved = 0;

            foreach (Square sq in Squares.Where(s => s.IsSolved))
            {
                numSolved++;
            }

            //Does it have 17 or more?
            if (numSolved >= 17)
                solvable = true;

            return solvable;
        }

        /// <summary>
        /// Searches left to right, top down to find first square with least amount of potential values.
        /// </summary>
        /// <returns>Will default return the first square found with only 2 values</returns>
        public Square SquareWithLeastAmountOfPotentialValues()
        {
            //Auto stops at the first square with 2 potential values
            bool shortcut = false;

            //Variables
            Square bestSquare = new Square(0,0);
            int currentSmallestNum = 10;

            //Search every square
            for (int row = 1; row < 10 && !shortcut; row++)
            {
                for (int column = 1; column < 10 && !shortcut; column++)
                {
                    Square currSquare = Squares.Single(x => (x.Row == row) && (x.Column == column));

                    //Skip if solved
                    if (!currSquare.IsSolved)
                    {
                        //IMPLEMENT SEARCH
                        int numVals = currSquare.PotentialValues.Count;

                        //Shortcut
                        if (numVals == 2)
                        {
                            shortcut = true;
                            bestSquare = currSquare;
                        }
                        else
                        {
                            //Compare to current best
                            if (numVals < currentSmallestNum)
                            {
                                bestSquare = currSquare;
                                currentSmallestNum = currSquare.PotentialValues.Count;
                            }
                        }
                    }
                }
            }

            return bestSquare;
        }

        public int GetSquareValue(int row, int column)
        {
            return (int)(Squares.Single(x => (x.Row == row) && (x.Column == column)).Value);
        }

        public void SetSquareValue(int row, int column, int value)
        {
            Square activeSquare = Squares.Single(x => (x.Row == row) && (x.Column == column));

            activeSquare.Value = value;

            // Remove value from other squares in the same row
            foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Row == row)))
            {
                square.PotentialValues.Remove(value);
            }

            // Remove value from other squares in the same column
            foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Column == column)))
            {
                square.PotentialValues.Remove(value);
            }

            // Remove value from other squares in the same quadrant
            foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Block == activeSquare.Block)))
            {
                square.PotentialValues.Remove(value);
            }

            // Set the Value for any square that only have one remaining PotentialValue
            foreach (Square square in Squares.Where(s => !s.IsSolved && (s.PotentialValues.Count == 1)))
            {
                SetSquareValue(square.Row, square.Column, square.PotentialValues[0]);
            }
        }
    }
}
