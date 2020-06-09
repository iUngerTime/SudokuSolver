using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGenerator.Models
{
    public class SudokuPuzzle
    {
        public int size { get; set; }
        public List<Square> squares { get; set; }
    }
}
