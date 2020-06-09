using Newtonsoft.Json;
using PuzzleGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGenerator.Service
{
    public class Generator
    {
        string _conURL = "http://www.cs.utep.edu/cheon/ws/sudoku/new/?size=9&level={0}";

        /// <summary>
        /// Generate a new, unqiue, and valid puzzle.
        /// Column values range from 0 - 8 and are denoted by X.
        /// Row values range from 0 - 8 and are denoted by Y.
        /// </summary>
        /// <param name="difficulty">1 (Easy), 2 (Medium), 3 (Hard)</param>
        /// <returns>Returns "NULL" if difficulty is out of range OR on failure to connect.
        /// Else returns a puzzle model.</returns>
        public SudokuPuzzle GenerateNewPuzzle(int difficulty)
        {
            //Validate Difficulty
            if (difficulty != 1 && difficulty != 2 && difficulty != 3)
                return null;

            //Format the _conUrl with the desired difficulty
            string url = string.Format(_conURL, difficulty);

            //Catch connection errors
            try
            {
                //Open Connection and get object
                using (WebClient wc = new WebClient())
                {
                    //Call API
                    string json = wc.DownloadString(url);

                    //Desearialze
                    SudokuPuzzle puzzle = JsonConvert.DeserializeObject<SudokuPuzzle>(json);

                    //Return serialized object
                    return puzzle;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
