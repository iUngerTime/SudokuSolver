using PuzzleGenerator.Models;
using PuzzleGenerator.Service;
using SudokuSolverLogic;
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
            //Set the value of class selected value
            if (Convert.ToString((sender as ToggleButton).Content) == "X")
                m_selectedValue = 0;
            else
                m_selectedValue = Convert.ToInt32((sender as ToggleButton).Content);

            //Iterate through all puzzle btns
            foreach (Grid Square in PuzzleSquares.Children.OfType<Grid>())
            {
                foreach (ToggleButton btn in Square.Children.OfType<ToggleButton>())
                {
                    if (btn.Content.ToString() == m_selectedValue.ToString())
                    {
                        btn.IsChecked = true;
                    }
                    else
                    {
                        btn.IsChecked = false;
                    }
                }
            }
        }

        private void GridButtonClick(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).Content.ToString() == m_selectedValue.ToString())
                (sender as ToggleButton).Content = "0";
            else
                (sender as ToggleButton).Content = m_selectedValue.ToString();
        }

        private void ClearAllSquaresClick(object sender, RoutedEventArgs e)
        {
            ClearSquaresOfContent();
        }

        void ClearSquaresOfContent()
        {
            //Iterate through all puzzle btns
            foreach (Grid Square in PuzzleSquares.Children.OfType<Grid>())
            {
                foreach (ToggleButton btn in Square.Children.OfType<ToggleButton>())
                {
                    btn.Content = "0";
                }
            }
        }

        private void SolvePuzzleClick(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidatePuzzle();

            if (isValid)
            {
                SolvePuzzle();
            }
            else
            {
                MessageBox.Show("You have made a mistake in entry.");
            }
        }

        private void SolvePuzzle()
        {
            Board solver = new Board();

            //Assign Values
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (GetUISquare(row, col).Content.ToString() != "0")
                        solver.SetSquareValue(row + 1, col + 1, Convert.ToInt32(GetUISquare(row, col).Content.ToString()));
                }
            }

            //Solves Entire Board
            solver.SolveEntireBoard();

            //Update Board
            UpdateBoard(solver);
        }


        /// <summary>
        /// Updates UI board
        /// </summary>
        /// <param name="solver">Needs the logic to update</param>
        private void UpdateBoard(Board solver)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (GetUISquare(row, col).Content.ToString() == "0")
                    {
                        int? content = solver.GetSquareValue(row + 1, col + 1);

                        if (content != null)
                        {
                            GetUISquare(row, col).Content = Convert.ToString(content);
                        }
                    }
                }
            }
        }

        private bool ValidatePuzzle()
        {
            //Check All Squares for validity
            bool valid = CheckQuadrents();

            ////Check All rows for validity
            //if (valid)
            //{
            //   valid = CheckRows();

            //   //Check all columns for validity
            //   if (valid)
            //      valid = CheckColumns();
            //}         

            return valid;
        }

        private bool CheckColumns()
        {
            throw new NotImplementedException();
        }

        private bool CheckRows()
        {
            throw new NotImplementedException();
        }

        private bool CheckQuadrents()
        {
            bool isValid = true;

            //Iterate through all Squares
            foreach (Grid Square in PuzzleSquares.Children.OfType<Grid>())
            {
                //Check 1 - 9
                for (int i = 1; i < 10 && isValid; i++)
                {
                    int count = 0;

                    //Loop every button in the square
                    foreach (ToggleButton btn in Square.Children.OfType<ToggleButton>())
                    {
                        if (btn.Content.ToString() == i.ToString())
                        {
                            count++;
                        }
                    }

                    //Make sure there is only one
                    if (count > 1)
                        isValid = false;
                }
            }

            return isValid;
        }

        private ToggleButton GetUISquare(int row, int col)
        {
            if (col == 0 && row == 0)
                return b0;
            else if (col == 1 && row == 0)
                return b1;
            else if (col == 2 && row == 0)
                return b2;
            else if (col == 3 && row == 0)
                return b3;
            else if (col == 4 && row == 0)
                return b4;
            else if (col == 5 && row == 0)
                return b5;
            else if (col == 6 && row == 0)
                return b6;
            else if (col == 7 && row == 0)
                return b7;
            else if (col == 8 && row == 0)
                return b8;
            else if (col == 0 && row == 1)
                return b9;
            else if (col == 1 && row == 1)
                return b10;
            else if (col == 2 && row == 1)
                return b11;
            else if (col == 3 && row == 1)
                return b12;
            else if (col == 4 && row == 1)
                return b13;
            else if (col == 5 && row == 1)
                return b14;
            else if (col == 6 && row == 1)
                return b15;
            else if (col == 7 && row == 1)
                return b16;
            else if (col == 8 && row == 1)
                return b17;
            else if (col == 0 && row == 2)
                return b18;
            else if (col == 1 && row == 2)
                return b19;
            else if (col == 2 && row == 2)
                return b20;
            else if (col == 3 && row == 2)
                return b21;
            else if (col == 4 && row == 2)
                return b22;
            else if (col == 5 && row == 2)
                return b23;
            else if (col == 6 && row == 2)
                return b24;
            else if (col == 7 && row == 2)
                return b25;
            else if (col == 8 && row == 2)
                return b26;
            else if (col == 0 && row == 3)
                return b27;
            else if (col == 1 && row == 3)
                return b28;
            else if (col == 2 && row == 3)
                return b29;
            else if (col == 3 && row == 3)
                return b30;
            else if (col == 4 && row == 3)
                return b31;
            else if (col == 5 && row == 3)
                return b32;
            else if (col == 6 && row == 3)
                return b33;
            else if (col == 7 && row == 3)
                return b34;
            else if (col == 8 && row == 3)
                return b35;
            else if (col == 0 && row == 4)
                return b36;
            else if (col == 1 && row == 4)
                return b37;
            else if (col == 2 && row == 4)
                return b38;
            else if (col == 3 && row == 4)
                return b39;
            else if (col == 4 && row == 4)
                return b40;
            else if (col == 5 && row == 4)
                return b41;
            else if (col == 6 && row == 4)
                return b42;
            else if (col == 7 && row == 4)
                return b43;
            else if (col == 8 && row == 4)
                return b44;
            else if (col == 0 && row == 5)
                return b45;
            else if (col == 1 && row == 5)
                return b46;
            else if (col == 2 && row == 5)
                return b47;
            else if (col == 3 && row == 5)
                return b48;
            else if (col == 4 && row == 5)
                return b49;
            else if (col == 5 && row == 5)
                return b50;
            else if (col == 6 && row == 5)
                return b51;
            else if (col == 7 && row == 5)
                return b52;
            else if (col == 8 && row == 5)
                return b53;
            else if (col == 0 && row == 6)
                return b54;
            else if (col == 1 && row == 6)
                return b55;
            else if (col == 2 && row == 6)
                return b56;
            else if (col == 3 && row == 6)
                return b57;
            else if (col == 4 && row == 6)
                return b58;
            else if (col == 5 && row == 6)
                return b59;
            else if (col == 6 && row == 6)
                return b60;
            else if (col == 7 && row == 6)
                return b61;
            else if (col == 8 && row == 6)
                return b62;
            else if (col == 0 && row == 7)
                return b63;
            else if (col == 1 && row == 7)
                return b64;
            else if (col == 2 && row == 7)
                return b65;
            else if (col == 3 && row == 7)
                return b66;
            else if (col == 4 && row == 7)
                return b67;
            else if (col == 5 && row == 7)
                return b68;
            else if (col == 6 && row == 7)
                return b69;
            else if (col == 7 && row == 7)
                return b70;
            else if (col == 8 && row == 7)
                return b71;
            else if (col == 0 && row == 8)
                return b72;
            else if (col == 1 && row == 8)
                return b73;
            else if (col == 2 && row == 8)
                return b74;
            else if (col == 3 && row == 8)
                return b75;
            else if (col == 4 && row == 8)
                return b76;
            else if (col == 5 && row == 8)
                return b77;
            else if (col == 6 && row == 8)
                return b78;
            else if (col == 7 && row == 8)
                return b79;
            else if (col == 8 && row == 8)
                return b80;
            else
                return new ToggleButton();
        }

        private void GenerateEasyPuzzle(object sender, RoutedEventArgs e)
        {
            ClearSquaresOfContent();
            SudokuPuzzle puzzle = new Generator().GenerateNewPuzzle(1);
            LoadGeneratedPuzzle(puzzle);
        }
        private void GenerateMediumPuzzle(object sender, RoutedEventArgs e)
        {
            ClearSquaresOfContent();
            SudokuPuzzle puzzle = new Generator().GenerateNewPuzzle(2);
            LoadGeneratedPuzzle(puzzle);
        }
        private void GenerateHardPuzzle(object sender, RoutedEventArgs e)
        {
            ClearSquaresOfContent();
            SudokuPuzzle puzzle = new Generator().GenerateNewPuzzle(3);
            LoadGeneratedPuzzle(puzzle);
        }

        private void LoadGeneratedPuzzle(SudokuPuzzle newPuzzle)
        {
            foreach (Square square in newPuzzle.squares)
            {
                GetUISquare(square.x, square.y).Content = square.value.ToString();
            }
        }
    }
}
