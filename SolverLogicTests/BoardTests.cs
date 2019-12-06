using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolverLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolverLogic.Tests
{
    [TestClass()]
    public class BoardTests
    {
        [TestMethod()]
        public void FindSquareWithLowestValue_AllSquaresAtTwoValues()
        {
            for (int row = 1; row < 10; row++)
            {
                for (int col = 1; col < 10; col++)
                {
                    Board testBoard = new Board();

                    //At left edge (set all but that col and (row +/- 1)(col)
                    if (col == 9)
                    {
                        //check if row would go out of boundary
                        int currRow;

                        if (row > 6)
                            currRow = row - 3;
                        else
                            currRow = row + 3;

                        //Set all but last 3 in row
                        for (int i = 1; i < 7; i++)
                        {
                            testBoard.SetSquareValue(row, i, i);
                        }

                        //Set one above or below col 9
                        testBoard.SetSquareValue(currRow, 9, 8);
                    }
                    //Not in a boundary (set all but that col and col + 1)
                    else
                    {
                        //Set all but that col and col + 1
                        for (int i = 1; i < 10; i++)
                        {
                            if(i != col && i != (col + 1))
                            {
                                testBoard.SetSquareValue(row, i, i);
                            }
                        }
                    }

                    Square testSquare = testBoard.SquareWithLeastAmountOfPotentialValues();

                    Assert.IsTrue(testSquare.Row == row);
                    Assert.IsTrue(testSquare.Column == col);
                }
            }
        }

        [TestMethod()]
        public void HasMinimumSolvableSquaresTestPastBoundary()
        {
            Board testBoard = new Board();

            int numAdded = 0;
            int row = 1;

            //Add 18 values
            while(numAdded < 17)
            {
                //Add 2 per row
                testBoard.SetSquareValue(row, 4, 2);
                testBoard.SetSquareValue(row, 7, 5);

                //Increment Row
                row++;
                numAdded += 2;
            }

            //Make sure that there is more than 17
            Assert.IsTrue(numAdded > 17);
            Assert.IsTrue(testBoard.HasMinimumSolvableSquares());
        }

        [TestMethod()]
        public void HasMinimumSolvableSquaresTestAtBoundary()
        {
            Board testBoard = new Board();

            int numAdded = 0;
            int row = 1;

            //Add 17 values
            while (numAdded < 16)
            {
                //Add 2 per row
                testBoard.SetSquareValue(row, 4, 2);
                testBoard.SetSquareValue(row, 7, 5);

                //Increment Row
                row++;
                numAdded += 2;
            }

            //Add one more
            testBoard.SetSquareValue(row, 7, 5);
            numAdded++;

            //Make sure that there is at least 17
            Assert.IsTrue(numAdded == 17);
            Assert.IsTrue(testBoard.HasMinimumSolvableSquares());
        }

        [TestMethod()]
        public void HasMinimumSolvableSquaresTestBelowBoundary()
        {
            Board testBoard = new Board();

            int numAdded = 0;
            int row = 1;

            //Add 17 values
            while (numAdded < 16)
            {
                //Add 2 per row
                testBoard.SetSquareValue(row, 4, 2);
                testBoard.SetSquareValue(row, 7, 5);

                //Increment Row
                row++;
                numAdded += 2;
            }

            //Make sure that there is at least 17
            Assert.IsTrue(numAdded == 16);
            Assert.IsFalse(testBoard.HasMinimumSolvableSquares());
        }
    }
}