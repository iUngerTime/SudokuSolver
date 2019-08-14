using SudokuSolver_v2.Grid_Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuSolver_v2
{
   //Class of grid squares defined in XAML
   public class GridSquares : ObservableCollection<GridSquare> { }

   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      //Row 1
      GridSquares UpperLeftSquares;
      GridSquares UpperMiddleSquares;
      GridSquares UpperRightSquares;

      //Row 2
      GridSquares MiddleLeftSquares;
      GridSquares CenterSquares;
      GridSquares MiddleRightSquares;

      //Row 3
      GridSquares LowerLeftSquares;
      GridSquares LowerMiddleSquares;
      GridSquares LowerRightSquares;

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         //Observable Collections
         this.UpperLeftSquares = new GridSquares();
         this.UpperMiddleSquares = new GridSquares();
         this.UpperRightSquares = new GridSquares();

         this.MiddleLeftSquares = new GridSquares();
         this.CenterSquares = new GridSquares();
         this.MiddleRightSquares = new GridSquares();

         this.LowerLeftSquares = new GridSquares();
         this.LowerMiddleSquares = new GridSquares();
         this.LowerRightSquares = new GridSquares();

         //SET DATA CONTEXTS
         //TO DO HERE

         //butt.Style = this.FindResource("SelectedToggleButton") as Style;
         //butt.Content = 1;
      }
   }
}
