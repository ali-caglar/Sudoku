using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SerializedSudokuGrid
{
    /// Contains all numbers of the sudoku grid
    private List<int> _completeSudokuGridData;

    /// Contains the playable sudoku grid's data
    private List<int> _playableSudokuGridData;

    /// <summary>
    /// Converts the list that contains the sudoku to 2d array.
    /// </summary>
    /// <returns>Returns 2d array of the sudoku data</returns>
    public int[,] GetSudokuData(SudokuDataType dataType)
    {
        if (_completeSudokuGridData == null || _completeSudokuGridData.Count < Sudoku.TotalCellCount
                                    || _playableSudokuGridData == null || _playableSudokuGridData.Count < Sudoku.TotalCellCount)
        {
            // This will send a console warning and returns an empty grid
            Debug.LogWarning("Sudoku Data is empty.");
            return new int[Sudoku.RowCount, Sudoku.ColumnCount]; 
        }

        int[,] grid = new int[Sudoku.RowCount, Sudoku.ColumnCount];
        List<int> sudokuData = dataType == SudokuDataType.Complete ? _completeSudokuGridData : _playableSudokuGridData;

        // Converting the list to 2d array
        for (int cellIndex = 0; cellIndex < Sudoku.TotalCellCount; cellIndex++)
        {
            int row = (int) Math.Floor((double) (cellIndex / Sudoku.RowCount));
            int column = cellIndex % Sudoku.ColumnCount;

            grid[row, column] = sudokuData[cellIndex];
        }

        return grid;
    }

    /// <summary>
    /// Converts sudoku grid(2d array) to list.
    /// </summary>
    /// <param name="dataType"></param>
    /// <param name="data">Sudoku Grid</param>
    public void SetSudokuData(SudokuDataType dataType, int[,] data)
    {
        List<int> sudokuGrid = new List<int>();
        
        // Converting the 2d array to list
        for (int row = 0; row < Sudoku.RowCount; row++)
        {
            for (int column = 0; column < Sudoku.ColumnCount; column++)
            {
                int cellData = data[row, column];
                sudokuGrid.Add(cellData);
            }
        }
        
        if (dataType == SudokuDataType.Complete)
        {
            _completeSudokuGridData = sudokuGrid;
        }
        else
        {
            _playableSudokuGridData = sudokuGrid;
        }
    }
}