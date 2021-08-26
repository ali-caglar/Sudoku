using UnityEngine;

public class SudokuBoard : GridCreator
{
    /// The sudoku data that contains the grid
    [Header("Sudoku Data")] 
    [SerializeField] private SudokuData _sudokuData;
    
    /// The cell that selected by the player
    public SudokuCell CurrentlySelectedSudokuCell { get; private set; }

    protected override void Start()
    {
        base.Start();
        CreateSudokuGame();
    }

    public void SelectSudokuCell(SudokuCell sudokuCell)
    {
        CurrentlySelectedSudokuCell = sudokuCell;
    }

    /// <summary>
    /// This will create the game by handling the values of the sudoku cells.
    /// </summary>
    private void CreateSudokuGame()
    {
        int[,] completeSudokuGrid = _sudokuData.CompleteSudokuGrid;
        int[,] playableSudokuGrid = _sudokuData.PlayableSudokuGrid;
        
        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int column = 0; column < _numberOfColumns; column++)
            {
                GameObject slot = _slotsOfGrid[row, column];
                SudokuCell sudokuCell = slot.AddComponent<SudokuCell>();

                // Sending initial data to cell
                int cellAnswer = completeSudokuGrid[row, column];
                sudokuCell.SetCellData(row, column, cellAnswer);
                
                // Creating the game
                int cellValue = playableSudokuGrid[row, column];
                sudokuCell.EnterValue(cellValue);
                
                if (cellValue != 0)
                {
                    sudokuCell.SetCellAsClue();
                }
            }
        }
    }
}