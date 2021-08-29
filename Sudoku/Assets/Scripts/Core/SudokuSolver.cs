using System;

namespace Sudoku.Core
{
    public class SudokuSolver
    {
        /// <summary>
        /// Checks the sudoku grid recursively if has any solution.
        /// </summary>
        /// <param name="sudokuGridToTest"></param>
        /// <param name="cellIndex"></param>
        /// <returns>Returns true if there's any solution to solve the sudoku grid.</returns>
        public bool HasAnySolution(int[,] sudokuGridToTest, int cellIndex = 0)
        {
            // Checking if the board completely filled
            if (cellIndex == SudokuRule.TotalCellCount)
            {
                return true;
            }
        
            int[,] copiedSudokuGrid = (int[,]) sudokuGridToTest.Clone();

            int row = (int) Math.Floor((double) (cellIndex / SudokuRule.RowCount));
            int column = cellIndex % SudokuRule.ColumnCount;

            // Checking the cell has already filled
            if (copiedSudokuGrid[row, column] != SudokuRule.EmptyCell)
            {
                return HasAnySolution(copiedSudokuGrid, cellIndex + 1);
            }

            // Testing the numbers(1-9) if it's lead to any solution
            for (int numberToTry = 1; numberToTry <= 9; numberToTry++)
            {
                // If there's no conflict (row-column-region) assign the number
                if (IsValid(copiedSudokuGrid, row, column, numberToTry))
                {
                    copiedSudokuGrid[row, column] = numberToTry;

                    // Check next cell to see if it's okay
                    // If not reset the cell
                    if (!HasAnySolution(copiedSudokuGrid, cellIndex + 1))
                    {
                        copiedSudokuGrid[row, column] = SudokuRule.EmptyCell;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks the sudoku grid's every cell if it has only one valid solution.
        /// </summary>
        /// <param name="sudokuGridToTest"></param>
        /// <returns>Returns true if there's only one solution.</returns>
        public bool HasUniqueSolution(int[,] sudokuGridToTest)
        {
            int[,] copiedSudokuGrid = (int[,]) sudokuGridToTest.Clone();
        
            // Controlling every cell
            for (int row = 0; row < SudokuRule.RowCount; row++)
            {
                for (int column = 0; column < SudokuRule.ColumnCount ; column++)
                {
                    // Checking if the cell is empty
                    if (copiedSudokuGrid[row, column] == SudokuRule.EmptyCell)
                    {
                        int solutionCount = 0;

                        // Checking every number(1-9) for if it's valid for the cell
                        // If the number is valid we increase the solution count
                        for (int numberToTry = 1; numberToTry <= 9; numberToTry++)
                        {
                            if (IsValid(copiedSudokuGrid, row, column, numberToTry))
                            {
                                solutionCount++;
                                copiedSudokuGrid[row, column] = numberToTry;
                            }
                        }

                        // If there's more than one valid number for the cell we return false
                        if (solutionCount != 1)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
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
            int regionStartOfRowIndex = row - (row % SudokuRule.RegionRowCount);
            int regionStartOfColumnIndex = column - (column % SudokuRule.RegionColumnCount);

            int regionEndOfRowIndex = regionStartOfRowIndex + SudokuRule.RegionRowCount;
            int regionEndOfColumnIndex = regionStartOfColumnIndex + SudokuRule.RegionColumnCount;

            // Checking the region
            for (int x = regionStartOfRowIndex; x < regionEndOfRowIndex; x++)
            {
                for (int y = regionStartOfColumnIndex; y < regionEndOfColumnIndex; y++)
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
}