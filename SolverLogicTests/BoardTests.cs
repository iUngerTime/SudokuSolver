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
        public void FindSquareWithLowestValueAllSquares()
        {
            //ONLY DOES ONE ROW RIGHT NOW


            Board testBoard = new Board();

            //Set 8 values in row 1
            for (int col = 1; col < 8; col++)
            {
                testBoard.SetSquareValue(1, col, col);
            }

            Square testSquare = testBoard.SquareWithLeastAmountOfPotentialValues();

            Assert.IsTrue(testSquare.Row == 1);
            Assert.IsTrue(testSquare.Column == 8);
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