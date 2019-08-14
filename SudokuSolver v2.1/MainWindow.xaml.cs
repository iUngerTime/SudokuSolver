using SudokuSolver_v2._1.GridObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuSolver_v2._1
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

      //CurrentStored Number
      int m_selectedValue;

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
         //SET DATA CONTEXTS
         this.UpperLeftSquares = (GridSquares)FindResource("UpperLeftSquares");
         this.UpperMiddleSquares = (GridSquares)FindResource("UpperMiddleSquares");
         this.UpperRightSquares = (GridSquares)FindResource("UpperRightSquares");

         this.MiddleLeftSquares = (GridSquares)FindResource("MiddleLeftSquares");
         this.CenterSquares = (GridSquares)FindResource("CenterSquares");
         this.MiddleRightSquares = (GridSquares)FindResource("MiddleRightSquares");

         this.LowerLeftSquares = (GridSquares)FindResource("LowerLeftSquares");
         this.LowerMiddleSquares = (GridSquares)FindResource("LowerMiddleSquares");
         this.LowerRightSquares = (GridSquares)FindResource("LowerRightSquares");

         m_selectedValue = 0;
      }

      private void MenuItemClear_Click(object sender, RoutedEventArgs e)
      {
         //foreach (var square in UpperLeftSquares)
         //{
         //    square.Content = 0;
         //}
      }

      private void ToggleButton_Click(object sender, RoutedEventArgs e)
      {
         (sender as ToggleButton).Content = Convert.ToString(m_selectedValue);
      }

      private void RadioButton_Click(object sender, RoutedEventArgs e)
      {
         if (Convert.ToString((sender as ToggleButton).Content) == "X")
            m_selectedValue = 0;
         else
            m_selectedValue = Convert.ToInt32((sender as ToggleButton).Content);
      }


   }
}
