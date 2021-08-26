using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "Sudoku Data", menuName = "Sudoku/Sudoku Data")]
public class SudokuData : ScriptableObject
{
    /// Returns 2d array of the complete sudoku data
    public int[,] CompleteSudokuGrid => _sudokuGrid.GetSudokuData(SudokuDataType.Complete);
    
    /// Returns 2d array of the playable sudoku data
    public int[,] PlayableSudokuGrid => _sudokuGrid.GetSudokuData(SudokuDataType.Playable);
    
    private SerializedSudokuGrid _sudokuGrid;
    private SudokuDataGenerator _generator = new SudokuDataGenerator();

    [ContextMenu("Generate Grid")]
    private void GenerateSudokuGrid()
    {
        int[,] completeSudokuData = _generator.GenerateSudokuData();
        int[,] playableSudokuData = _generator.PrepareSudokuDataToBePlayable(completeSudokuData);
        
        _sudokuGrid.SetSudokuData(SudokuDataType.Complete, completeSudokuData);
        _sudokuGrid.SetSudokuData(SudokuDataType.Playable, playableSudokuData);
    }
}