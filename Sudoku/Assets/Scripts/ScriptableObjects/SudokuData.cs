using UnityEngine;
using Sudoku.Core;
using Sudoku.Enums;

namespace Sudoku.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Sudoku Data", menuName = "Sudoku/Sudoku Data")]
    public class SudokuData : ScriptableObject
    {
        [SerializeField] private SerializedSudokuGrid _sudokuGrid;
        
        /// Returns 2d array of the complete sudoku data
        public int[,] CompleteSudokuGrid => _sudokuGrid.GetSudokuData(SudokuDataType.Complete);
    
        /// Returns 2d array of the playable sudoku data
        public int[,] PlayableSudokuGrid => _sudokuGrid.GetSudokuData(SudokuDataType.Playable);
    
        private SudokuDataGenerator _generator = new SudokuDataGenerator();

        public void GenerateSudokuGrid()
        {
            int[,] completeSudokuData = _generator.GenerateSudokuData();
            int[,] playableSudokuData = _generator.PrepareSudokuDataToBePlayable(completeSudokuData);
        
            _sudokuGrid.SetSudokuData(SudokuDataType.Complete, completeSudokuData);
            _sudokuGrid.SetSudokuData(SudokuDataType.Playable, playableSudokuData);
        }
    }
}