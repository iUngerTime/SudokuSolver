using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SolverLogic
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summar>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            Board solver = new Board();

            //Assign Values
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if(GetUISquare(row, col).Text != "")
                        solver.SetSquareValue(row + 1, col + 1, Convert.ToInt32(GetUISquare(row, col).Text));
                }
            }

            try
            {
                //Update Board
                UpdateBoard(solver);
            }
            catch (Exception)
            {
                //Tell user not enough information
                MessageBox.Show("Not Enough Information To Solve Puzzle", "ERROR");
            }
        }

        private void UpdateBoard(Board solver)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (GetUISquare(row, col).Text == "")
                    {
                        GetUISquare(row, col).Text = Convert.ToString(solver.GetSquareValue(row + 1, col + 1));
                        GetUISquare(row, col).Foreground = Brushes.Red;
                    }
                }
            }
        }

        private ComboBox GetUISquare(int row, int col)
        {
            if (col == 0 && row == 0)
                return Box00;
            else if (col == 1 && row == 0)
                return Box10;
            else if (col == 2 && row == 0)
                return Box20;
            else if (col == 3 && row == 0)
                return Box30;
            else if (col == 4 && row == 0)
                return Box40;
            else if (col == 5 && row == 0)
                return Box50;
            else if (col == 6 && row == 0)
                return Box60;
            else if (col == 7 && row == 0)
                return Box70;
            else if (col == 8 && row == 0)
                return Box80;
            else if (col == 0 && row == 1)
                return Box01;
            else if (col == 1 && row == 1)
                return Box11;
            else if (col == 2 && row == 1)
                return Box21;
            else if (col == 3 && row == 1)
                return Box31;
            else if (col == 4 && row == 1)
                return Box41;
            else if (col == 5 && row == 1)
                return Box51;
            else if (col == 6 && row == 1)
                return Box61;
            else if (col == 7 && row == 1)
                return Box71;
            else if (col == 8 && row == 1)
                return Box81;
            else if (col == 0 && row == 2)
                return Box02;
            else if (col == 1 && row == 2)
                return Box12;
            else if (col == 2 && row == 2)
                return Box22;
            else if (col == 3 && row == 2)
                return Box32;
            else if (col == 4 && row == 2)
                return Box42;
            else if (col == 5 && row == 2)
                return Box52;
            else if (col == 6 && row == 2)
                return Box62;
            else if (col == 7 && row == 2)
                return Box72;
            else if (col == 8 && row == 2)
                return Box82;
            else if (col == 0 && row == 3)
                return Box03;
            else if (col == 1 && row == 3)
                return Box13;
            else if (col == 2 && row == 3)
                return Box23;
            else if (col == 3 && row == 3)
                return Box33;
            else if (col == 4 && row == 3)
                return Box43;
            else if (col == 5 && row == 3)
                return Box53;
            else if (col == 6 && row == 3)
                return Box63;
            else if (col == 7 && row == 3)
                return Box73;
            else if (col == 8 && row == 3)
                return Box83;
            else if (col == 0 && row == 4)
                return Box04;
            else if (col == 1 && row == 4)
                return Box14;
            else if (col == 2 && row == 4)
                return Box24;
            else if (col == 3 && row == 4)
                return Box34;
            else if (col == 4 && row == 4)
                return Box44;
            else if (col == 5 && row == 4)
                return Box54;
            else if (col == 6 && row == 4)
                return Box64;
            else if (col == 7 && row == 4)
                return Box74;
            else if (col == 8 && row == 4)
                return Box84;
            else if (col == 0 && row == 5)
                return Box05;
            else if (col == 1 && row == 5)
                return Box15;
            else if (col == 2 && row == 5)
                return Box25;
            else if (col == 3 && row == 5)
                return Box35;
            else if (col == 4 && row == 5)
                return Box45;
            else if (col == 5 && row == 5)
                return Box55;
            else if (col == 6 && row == 5)
                return Box65;
            else if (col == 7 && row == 5)
                return Box75;
            else if (col == 8 && row == 5)
                return Box85;
            else if (col == 0 && row == 6)
                return Box06;
            else if (col == 1 && row == 6)
                return Box16;
            else if (col == 2 && row == 6)
                return Box26;
            else if (col == 3 && row == 6)
                return Box36;
            else if (col == 4 && row == 6)
                return Box46;
            else if (col == 5 && row == 6)
                return Box56;
            else if (col == 6 && row == 6)
                return Box66;
            else if (col == 7 && row == 6)
                return Box76;
            else if (col == 8 && row == 6)
                return Box86;
            else if (col == 0 && row == 7)
                return Box07;
            else if (col == 1 && row == 7)
                return Box17;
            else if (col == 2 && row == 7)
                return Box27;
            else if (col == 3 && row == 7)
                return Box37;
            else if (col == 4 && row == 7)
                return Box47;
            else if (col == 5 && row == 7)
                return Box57;
            else if (col == 6 && row == 7)
                return Box67;
            else if (col == 7 && row == 7)
                return Box77;
            else if (col == 8 && row == 7)
                return Box87;
            else if (col == 0 && row == 8)
                return Box08;
            else if (col == 1 && row == 8)
                return Box18;
            else if (col == 2 && row == 8)
                return Box28;
            else if (col == 3 && row == 8)
                return Box38;
            else if (col == 4 && row == 8)
                return Box48;
            else if (col == 5 && row == 8)
                return Box58;
            else if (col == 6 && row == 8)
                return Box68;
            else if (col == 7 && row == 8)
                return Box78;
            else if (col == 8 && row == 8)
                return Box88;
            else
                return new ComboBox();
        }
    }
}
