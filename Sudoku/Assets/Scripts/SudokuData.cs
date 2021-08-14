using UnityEngine;

[CreateAssetMenu(fileName = "Sudoku Data", menuName = "Sudoku/Sudoku Data")]
public class SudokuData : ScriptableObject
{
    /// Returns 2d array of the sudoku data
    public int[,] SudokuGrid => _sudokuGrid.GetSudokuData();
    
    private SerializedSudokuGrid _sudokuGrid;
    private SudokuDataGenerator _generator = new SudokuDataGenerator();

    [ContextMenu("Generate Grid")]
    private void GenerateSudokuGrid()
    {
        int[,] sudokuArray = _generator.GenerateSudokuData();
        _sudokuGrid.SetSudokuData(sudokuArray);
    }
}