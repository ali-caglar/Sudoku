using System;

public class SudokuSolver
{
    private int _sizeOfBoard = 9;
    
    /// <summary>
    /// Checks the sudoku grid recursively if has any solution.
    /// </summary>
    /// <param name="sudokuGridToTest"></param>
    /// <param name="slotIndex"></param>
    /// <returns>Returns true if there's any solution to solve the sudoku grid.</returns>
    public bool HasAnySolution(int[,] sudokuGridToTest, int slotIndex = 0)
    {
        int[,] copiedSudokuGrid = new int[_sizeOfBoard, _sizeOfBoard];
        Array.Copy(sudokuGridToTest,copiedSudokuGrid, sudokuGridToTest.Length);

        int row = (int) Math.Floor((double) (slotIndex / _sizeOfBoard));
        int column = slotIndex % _sizeOfBoard;

        // Checking if the board completely filled
        if (slotIndex == 81)
        {
            return true;
        }

        // Checking the slot has already filled
        if (copiedSudokuGrid[row, column] != 0)
        {
            return HasAnySolution(copiedSudokuGrid, slotIndex + 1);
        }

        // Testing the numbers(1-9) if it's lead to any solution
        for (int numberToTry = 1; numberToTry <= 9; numberToTry++)
        {
            // If there's no conflict (row-column-region) assign the number
            if (IsValid(copiedSudokuGrid, row, column, numberToTry))
            {
                copiedSudokuGrid[row, column] = numberToTry;

                // Check next slot to see if it's okay
                // If not reset the slot
                if (HasAnySolution(copiedSudokuGrid, slotIndex + 1))
                {
                    return true;
                }
                else
                {
                    copiedSudokuGrid[row, column] = 0;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Checks every row-column-region to see if it's okay to place the number according to sudoku constraints.
    /// </summary>
    /// <param name="sudokuGrid"></param>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="numberToTry"></param>
    /// <returns>Returns true if it's okay to place the number.</returns>
    public bool IsValid(int[,] sudokuGrid, int row, int column, int numberToTry)
    {
        for (int i = 0; i < 9; i++)
        {
            // Checking every column
            if (sudokuGrid[row, i] == numberToTry)
            {
                return false;
            }

            // Checking every row
            if (sudokuGrid[i, column] == numberToTry)
            {
                return false;
            }
        }

        // Calculating the region's first and last indexes
        int regionStartOfRowIndex = row - (row % 3);
        int regionStartOfColumnIndex = column - (column % 3);

        int regionEndOfRowIndex = regionStartOfRowIndex + 2;
        int regionEndOfColumnIndex = regionStartOfColumnIndex + 2;

        // Checking the region
        for (int x = regionStartOfRowIndex; x <= regionEndOfRowIndex; x++)
        {
            for (int y = regionStartOfColumnIndex; y <= regionEndOfColumnIndex; y++)
            {
                if (sudokuGrid[x, y] == numberToTry)
                {
                    return false;
                }
            }
        }

        return true;
    }
}