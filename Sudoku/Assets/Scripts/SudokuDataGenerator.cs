using Random = System.Random;

public class SudokuDataGenerator
{
    private int _sizeOfBoard = 9;

    Random _random = new Random();
    private SudokuSolver _solver = new SudokuSolver();

    /// <summary>
    /// Generates completely filled sudoku grid.
    /// </summary>
    /// <returns>Returns a complete sudoku grid.</returns>
    public int[,] GenerateSudokuData()
    {
        int[,] sudokuGrid = new int[_sizeOfBoard, _sizeOfBoard];

        for (int row = 0; row < _sizeOfBoard; row++)
        {
            for (int column = 0; column < _sizeOfBoard; column++)
            {
                while (sudokuGrid[row, column] == 0)
                {
                    int numberToTry = _random.Next(1, 10);

                    if (_solver.IsValid(sudokuGrid, row, column, numberToTry))
                    {
                        sudokuGrid[row, column] = numberToTry;

                        if (!_solver.HasAnySolution(sudokuGrid))
                        {
                            sudokuGrid[row, column] = 0;
                        }
                    }
                    else
                    {
                        sudokuGrid[row, column] = 0;
                    }
                }
            }
        }

        return sudokuGrid;
    }
}