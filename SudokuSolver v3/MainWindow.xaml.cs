using System;
using System.Collections.Generic;
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

namespace SudokuSolver_v3
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      //CurrentSelected Number
      int m_selectedValue;

      public MainWindow()
      {
         InitializeComponent();
      }

      private void ToolBarSelectionButtons(object sender, RoutedEventArgs e)
      {
         if (Convert.ToString((sender as ToggleButton).Content) == "X")
            m_selectedValue = 0;
         else
            m_selectedValue = Convert.ToInt32((sender as ToggleButton).Content);
      }
   }
}
