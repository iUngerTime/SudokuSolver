using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SudokuSolverLogic
{
    public class Board
    {   
        //List of squares that make up the board
        private List<Square> Squares { get; set; }

        #region public properties
        public bool Solved
        {
            get
            {
                return (Squares.TrueForAll(SquareSet) ? true : false);
            }
        }
        /// <summary>
        /// Search predicate to determine board solved
        /// </summary>
        /// <param name="s">Square in question</param>
        /// <returns>true if square is solved</returns>
        private static bool SquareSet(Square s)
        {
            return s.IsSet;
        }
        #endregion

        /// <summary>
        /// Creates a new 9x9 board with empty values
        /// </summary>
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
                    if (!currSquare.IsSet)
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

        #region Remove Potential Values
        /// <summary>
        /// Used to remove the potential values for a cell.
        /// Will check the rest of the board to find any others that can be solved.
        /// Solved squares have their potential values cleared
        /// </summary>
        /// <param name="cell">The cell to set permanently to a value</param>
        private void SolidifySquareValue(Square cell)
        {
            if (cell.IsSet)
            {
                //Remove All Potential Values For Specified Square
                cell.PotentialValues.Clear();

                RemovePotentialValuesFromRow(cell.Row, cell.Value);

                RemovePotentialValuesFromColumn(cell.Column, cell.Value);

                RemovePotentialValuesFromQuaderant(cell.Value, cell);

                // Set the Value for any square that only have one remaining PotentialValue
                foreach (Square square in Squares.Where(s => !s.IsSet && (s.PotentialValues.Count == 1)))
                {
                    //Set the newly found value
                    SetSquareValue(square.Row, square.Column, square.PotentialValues[0]);

                    //Permanently set the value and continue the solve
                    SolidifySquareValue(square);
                }
            }
        }

        private void RemovePotentialValuesFromQuaderant(int value, Square sqareInQuadrent)
        {
            // Remove value from other squares in the same quadrant
            foreach (Square square in Squares.Where(s => !s.IsSet && (s.Block == sqareInQuadrent.Block)))
            {
                square.PotentialValues.Remove(value);
            }
        }

        private void RemovePotentialValuesFromColumn(int column, int value)
        {
            // Remove value from other squares in the same column
            foreach (Square square in Squares.Where(s => !s.IsSet && (s.Column == column)))
            {
                square.PotentialValues.Remove(value);
            }
        }

        private void RemovePotentialValuesFromRow(int row, int value)
        {
            // Remove value from other squares in the same row
            foreach (Square square in Squares.Where(s => !s.IsSet && (s.Row == row)))
            {
                square.PotentialValues.Remove(value);
            }
        }
        #endregion

        /// <summary>
        /// Finds Consectutive Sequences of either identitcal rows and columns.
        /// If a sequence is found it removes all other other potential values from those row/columns.
        /// </summary>
        /// <returns>True if any sequences were changed, False if not</returns>
        private bool FindRowAndColumnSequencingInQuadrents()
        {
            bool eliminatedValues = false;

            //Loop All Quadrents
            for (int quadrent = 0; quadrent < 9; quadrent++)
            {
                //Find Singles of Each number in Each quadrent
                for (int i = 1; i < 10; i++)
                {
                    //List to store all values in quadrent with i value
                    List<Square> squaresWithI = new List<Square>();

                    // SearchQuadrent
                    foreach (Square currSquare in Squares.Where(s => !s.IsSet && ((int)s.Block == quadrent) && s.PotentialValues.Contains(i)))
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
                            if (diffRow != s.Row)
                                sameRow = false;

                            if (diffCol != s.Column)
                                sameCol = false;
                        }

                        //If Same Row or Column (cannot be both at same time) remove all other values from columns/rows
                        if (sameRow)
                        {
                            RemovePotentialValuesFromRow(diffRow, i);
                            foreach (Square s in squaresWithI)
                            {
                                s.PotentialValues.Add(i);
                            }

                            //Indicate changed values
                            eliminatedValues = true;
                        }
                        else if (sameCol)
                        {
                            RemovePotentialValuesFromColumn(diffCol, i);
                            foreach (Square s in squaresWithI)
                            {
                                s.PotentialValues.Add(i);
                            }

                            //Indicate changed values
                            eliminatedValues = true;
                        }
                    }
                }
            }

            return eliminatedValues;
        }

        /// <summary>
        /// Finds numbers in quadrents that can only be one value and updates that square with its 
        /// proper value.
        /// </summary>
        /// <returns>True if changed a value, false if nothing was changed</returns>
        private bool FindSingleNumbersInQuadrents()
        {
            bool changedSquare = false;

            //Loop All Quadrents
            for (int quadrent = 0; quadrent < 9; quadrent++)
            {
                //Find Singles of Each number in Each quadrent
                for (int i = 1; i < 10; i++)
                {
                    //int to keep track of number found
                    int numValues = 0;

                    // SearchQuadrent
                    foreach (Square square in Squares.Where(s => !s.IsSet && ((int)s.Block == quadrent) && s.PotentialValues.Contains(i)))
                    {
                        numValues++;
                    }

                    //Update single square if one is found
                    if (numValues == 1)
                    {
                        Square newSolvedSquare = Squares.Single(s => !s.IsSet && ((int)s.Block == quadrent) && s.PotentialValues.Contains(i));
                        SetSquareValue(newSolvedSquare.Row, newSolvedSquare.Column, i);

                        //Mark that something was changed
                        changedSquare = true;
                    }
                }
            }

            return changedSquare;
        }

        #region public functions
        /// <summary>
        /// Function to be called externally to solve board to completion
        /// </summary>
        /// <returns>Returns True on success. Returns false if unsolvable or doesn't have at least 17 squares.</returns>
        public bool SolveEntireBoard()
        {
            //Check if puzzle currently has enough clues to be solved
            if (!HasMinimumSolvableSquares())
            {
                Debug.WriteLine("ERROR: Not enough set squares to solve. MINIMUM: 17");
                return false;
            }

            //Start solving the board
            foreach (Square sqr in Squares)
            {
                //Set each square value in place
                SolidifySquareValue(sqr);
            }

            if(!Solved)
            {
                //Advanced Algorithems
                Debug.WriteLine("Unable to finish solving");

                return false;
            }

            return true;

            //while (notSolved)
            //{
            //    bool singlesChanged = FindSingleNumbersInQuadrents();
            //    bool sequenceChanged = FindRowAndColumnSequencingInQuadrents();

            //    //Check solved or not
            //    if (Squares.TrueForAll(SquareSolved))
            //        notSolved = false;
            //    //Shortcut harder puzzles that can't be solved
            //    else if (!singlesChanged && !sequenceChanged)
            //        notSolved = false;
            //}
        }

        /// <summary>
        /// Sets a square's value based on the row and column. Recursively calls itself.
        /// </summary>
        /// <param name="row">Horizontal Directions</param>
        /// <param name="column">Vertical Directions</param>
        /// <param name="value">Value to set square at</param>
        public void SetSquareValue(int row, int column, int value)
        {
            //Get the square from the list
            Square activeSquare = Squares.Single(x => (x.Row == row) && (x.Column == column));

            //Set the value (this is not permanent)
            activeSquare.Value = value;
        }

        /// <summary>
        /// Gets a square's value from the row and colulmn
        /// </summary>
        /// <param name="row">Horizontal Direction (The Y coordinate in a plane)</param>
        /// <param name="column">Vertical Direction (The X coordinate in a place)</param>
        /// <returns>A null if it hasn't been set, otherwise the value.</returns>
        public int GetSquareValue(int row, int column)
        {
            return (Squares.Single(x => (x.Row == row) && (x.Column == column)).Value);
        }

        /// <summary>
        /// Sudoku Requires at least 17 minimum squares to be solvable. This determines if the board has enough squares solved
        /// </summary>
        /// <returns>True if has 17 or more, false if not.</returns>
        public bool HasMinimumSolvableSquares()
        {
            bool solvable = false;
            int numSet = 0;

            //Count the number of set values
            foreach (Square sq in Squares.Where(s => s.IsSet))
            {
                numSet++;
            }

            //Does it have 17 or more?
            if (numSet > 16)
                solvable = true;

            return solvable;
        }
        #endregion
    }
}
