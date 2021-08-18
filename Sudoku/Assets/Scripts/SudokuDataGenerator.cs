using System;
using System.Collections.Generic;
using Extensions;
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
                // We keep trying until it's filled
                while (sudokuGrid[row, column] == 0)
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

    /// <summary>
    /// Returns a shuffled list that contains numbers from 0 to 80. 
    /// </summary>
    /// <returns></returns>
    private List<int> ReturnRandomCellIndexList()
    {
        List<int> randomList = new List<int>();

        for (int i = 0; i < 81; i++)
        {
            randomList.Add(i);
        }

        randomList.Shuffle();

        return randomList;
    }
}