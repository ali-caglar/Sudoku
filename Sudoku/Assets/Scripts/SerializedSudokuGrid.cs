using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SerializedSudokuGrid
{
    /// Contains all numbers of the sudoku grid 
    private List<int> _sudokuGridData;
    
    /// <summary>
    /// Converts the list that contains the sudoku to 2d array.
    /// </summary>
    /// <returns>Returns 2d array of the sudoku data</returns>
    public int[,] GetSudokuData()
    {
        int[,] grid = new int[9, 9];

        if (_sudokuGridData == null || _sudokuGridData.Count == 0)
        {
            Debug.LogWarning($"Sudoku Data is empty.");
            return grid;
        }

        for (int slotIndex = 0; slotIndex < 81; slotIndex++)
        {
            int row = (int) Math.Floor((double) (slotIndex / 9));
            int column = slotIndex % 9;

            grid[row, column] = _sudokuGridData[slotIndex];
        }

        return grid;
    }

    /// <summary>
    /// Converts sudoku grid(2d array) to list.
    /// </summary>
    /// <param name="data">Sudoku Grid</param>
    public void SetSudokuData(int[,] data)
    {
        _sudokuGridData = new List<int>();

        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                int slot = data[row, column];
                _sudokuGridData.Add(slot);
            }
        }
    }
}