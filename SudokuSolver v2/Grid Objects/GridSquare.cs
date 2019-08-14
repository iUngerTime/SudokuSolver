using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SudokuSolver_v2.Grid_Objects
{
   public class GridSquare : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      //Notify Function
      void Notify(string propName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
      }

      //Variables
      int row;
      int col;

      //Row Property
      public int Row
      {
         get { return row; }
         set
         {
            row = value;
            Notify("Row");
         }
      }

      //Column Property
      public int Column
      {
         get { return col; }
         set
         {
            col = value;
            Notify("Column");
         }
      }

      //Constructors
      public GridSquare() : this(0, 0) { }
      public GridSquare(int row, int col)
      {
         this.row = row;
         this.col = col;
      }
   }
}
