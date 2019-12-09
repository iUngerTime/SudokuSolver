using System;
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
        private Square SquareWithLeastAmountOfPotentialValues()
        {
            //Auto stops at the first square with 2 potential values
            bool shortcut = false;

            //Variables
            Square bestSquare = new Square(0, 0);
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

        public int? GetSquareValue(int row, int column)
        {
            return (Squares.Single(x => (x.Row == row) && (x.Column == column)).Value);
        }

        public void SetSquareValue(int row, int column, int value)
        {
            Square activeSquare = Squares.Single(x => (x.Row == row) && (x.Column == column));

            activeSquare.Value = value;

            //Remove All Potential Values For Specified Square
            activeSquare.PotentialValues.Clear();

            RemovePotentialValuesFromSpecifiedRow(row, value);

            RemovePotentialValuesFromSpecifiedColumn(column, value);

            RemovePotentialValuesFromSpecifiedQuaderant(value, activeSquare);

            // Set the Value for any square that only have one remaining PotentialValue
            foreach (Square square in Squares.Where(s => !s.IsSolved && (s.PotentialValues.Count == 1)))
            {
                SetSquareValue(square.Row, square.Column, square.PotentialValues[0]);
            }
        }

        private void RemovePotentialValuesFromSpecifiedQuaderant(int value, Square activeSquare)
        {
            // Remove value from other squares in the same quadrant
            foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Block == activeSquare.Block)))
            {
                square.PotentialValues.Remove(value);
            }
        }

        private void RemovePotentialValuesFromSpecifiedColumn(int column, int value)
        {
            // Remove value from other squares in the same column
            foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Column == column)))
            {
                square.PotentialValues.Remove(value);
            }
        }

        private void RemovePotentialValuesFromSpecifiedRow(int row, int value)
        {
            // Remove value from other squares in the same row
            foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Row == row)))
            {
                square.PotentialValues.Remove(value);
            }
        }

        public void SolveEntireBoard()
        {
            bool notSolved = true;

            while (notSolved)
            {
                FindSingleNumbersInQuadrents();
                FindRowAndColumnSequencingInQuadrents();

                //Check solved or not
                if (Squares.TrueForAll(SquareSolved))
                    notSolved = false;
            }
        }

        /// <summary>
        /// Search predicate to determine board solved
        /// </summary>
        /// <param name="s">Square in question</param>
        /// <returns>true if square is solved</returns>
        private static bool SquareSolved(Square s)
        {
            return s.IsSolved;
        }

        /// <summary>
        /// Finds Consectutive Sequences of either identitcal rows and columns.
        /// If a sequence is found it removes all other other potential values from those row/columns.
        /// </summary>
        private void FindRowAndColumnSequencingInQuadrents()
        {
            //Loop All Quadrents
            for (int quadrent = 0; quadrent < 9; quadrent++)
            {
                //Find Singles of Each number in Each quadrent
                for (int i = 1; i < 10; i++)
                {
                    //List to store all values in quadrent with i value
                    List<Square> squaresWithI = new List<Square>();

                    // SearchQuadrent
                    foreach (Square currSquare in Squares.Where(s => !s.IsSolved && ((int)s.Block == quadrent) && s.PotentialValues.Contains(i)))
                    {
                        squaresWithI.Add(currSquare);
                    }

                    //If potentially sequenced squares (more than one square) have been found
                    if (squaresWithI.Count() > 1)
                    {
                        //Row Knowledge Variables
                        bool sameRow = true;
                        int diffRow = 0;

                        //Column knowledge variables
                        bool sameCol = true;
                        int diffCol = 0;

                        foreach (Square s in squaresWithI)
                        {
                            //First iteration to set column and row
                            if (diffRow == 0 || diffCol == 0)
                            {
                                //Set running value for rows
                                diffRow = s.Row;

                                //set running value for columns
                                diffCol = s.Column;
                            }

                            //Set bools if one square violates same column/row rules
                            if (diffRow != s.Column)
                                sameRow = false;

                            if (diffCol != s.Column)
                                sameCol = false;
                        }

                        //If Same Row or Column (cannot be both at same time) remove all other values from columns/rows
                        if (sameRow)
                        {
                            RemovePotentialValuesFromSpecifiedRow(diffRow, i);
                            foreach (Square s in squaresWithI)
                            {
                                s.PotentialValues.Add(i);
                            }
                        }
                        else if (sameCol)
                        {
                            RemovePotentialValuesFromSpecifiedColumn(diffCol, i);
                            foreach (Square s in squaresWithI)
                            {
                                s.PotentialValues.Add(i);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Finds numbers in quadrents that can only be one value and updates that square with its 
        /// proper value.
        /// </summary>
        private void FindSingleNumbersInQuadrents()
        {
            //Loop All Quadrents
            for (int quadrent = 0; quadrent < 9; quadrent++)
            {
                //Find Singles of Each number in Each quadrent
                for (int i = 1; i < 10; i++)
                {
                    //int to keep track of number found
                    int numValues = 0;

                    // SearchQuadrent
                    foreach (Square square in Squares.Where(s => !s.IsSolved && ((int)s.Block == quadrent) && s.PotentialValues.Contains(i)))
                    {
                        numValues++;
                    }

                    //Update single square if one is found
                    if (numValues == 1)
                    {
                        Square newSolvedSquare = Squares.Single(s => !s.IsSolved && ((int)s.Block == quadrent) && s.PotentialValues.Contains(i));
                        SetSquareValue(newSolvedSquare.Row, newSolvedSquare.Column, i);
                    }
                }
            }
        }
    }
}
