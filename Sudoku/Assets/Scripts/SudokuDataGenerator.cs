using System;
using System.Collections.Generic;
using Extensions;
using Random = System.Random;

public class SudokuDataGenerator
{
    Random _random = new Random();
    private SudokuSolver _solver = new SudokuSolver();

    /// <summary>
    /// Generates completely filled sudoku grid.
    /// </summary>
    /// <returns>Returns a complete sudoku grid.</returns>
    public int[,] GenerateSudokuData()
    {
        int[,] sudokuGrid = new int[Sudoku.RowCount, Sudoku.ColumnCount];

        for (int row = 0; row < Sudoku.RowCount; row++)
        {
            for (int column = 0; column < Sudoku.ColumnCount; column++)
            {
                // We keep trying until it's filled
                while (sudokuGrid[row, column] == Sudoku.EmptyCell)
                {
                    // Producing a random number(1-9) to try
                    int numberToTry = _random.Next(1, 10);

                    // If there's no conflict (row-column-region) assign the number
                    // Otherwise keep the cell empty
                    if (_solver.IsValid(sudokuGrid, row, column, numberToTry))
                    {
                        sudokuGrid[row, column] = numberToTry;

                        // Check if there's any solution
                        // If not remove the assigned number
                        if (!_solver.HasAnySolution(sudokuGrid))
                        {
                            sudokuGrid[row, column] = Sudoku.EmptyCell;
                        }
                    }
                    else
                    {
                        sudokuGrid[row, column] = Sudoku.EmptyCell;
                    }
                }
            }
        }

        return sudokuGrid;
    }

    /// <summary>
    /// It takes a complete sudoku grid and resets random cells to make a solvable sudoku grid.
    /// </summary>
    /// <param name="completeSudokuGrid"></param>
    /// <returns>Returns a solvable sudoku grid data</returns>
    public int[,] PrepareSudokuDataToBePlayable(int[,] completeSudokuGrid)
    {
        int[,] copiedSudokuGrid = (int[,]) completeSudokuGrid.Clone();
        
        // We create a random list of cell indexes
        List<int> randomCellOrder = ReturnRandomCellIndexList();

        // Checking every cell
        while (randomCellOrder.Count > 0)
        {
            // Getting a index number from a shuffled list and then removing it to not check again
            int cellIndex = randomCellOrder[0];
            int row = (int) Math.Floor((double) (cellIndex / Sudoku.RowCount));
            int column = cellIndex % Sudoku.ColumnCount;
            randomCellOrder.RemoveAt(0);

            // Backup the removed number
            int removedNumber = copiedSudokuGrid[row, column];
            // Resetting the cell
            copiedSudokuGrid[row, column] = Sudoku.EmptyCell;

            // If grid has no unique solution we putting the number back
            if (!_solver.HasUniqueSolution(copiedSudokuGrid))
            {
                copiedSudokuGrid[row, column] = removedNumber;
            }
        }

        return copiedSudokuGrid;
    }

    /// <summary>
    /// Returns a shuffled list that contains numbers from 0 to 80. 
    /// </summary>
    /// <returns></returns>
    private List<int> ReturnRandomCellIndexList()
    {
        List<int> randomList = new List<int>();

        for (int i = 0; i < Sudoku.TotalCellCount; i++)
        {
            randomList.Add(i);
        }

        randomList.Shuffle();

        return randomList;
    }
}