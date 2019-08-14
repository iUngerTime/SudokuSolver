﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver_v2._1.SolverLogic
{
   public class Board
   {
      public List<Square> Squares { get; private set; }

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
