using System;
using System.Collections.Generic;
using UnityEngine;
using Sudoku.Core;
using Sudoku.Enums;

namespace Sudoku.ScriptableObjects
{
    [Serializable]
    public struct SerializedSudokuGrid
    {
        /// Contains all numbers of the sudoku grid
        [SerializeField] private List<int> _completeSudokuGridData;

        /// Contains the playable sudoku grid's data
        [SerializeField] private List<int> _playableSudokuGridData;

        /// <summary>
        /// Converts the list that contains the sudoku to 2d array.
        /// </summary>
        /// <returns>Returns 2d array of the sudoku data</returns>
        public int[,] GetSudokuData(SudokuDataType dataType)
        {
            if (_completeSudokuGridData == null || _completeSudokuGridData.Count < SudokuRule.TotalCellCount
                                                || _playableSudokuGridData == null || _playableSudokuGridData.Count < SudokuRule.TotalCellCount)
            {
                // This will send a console warning and returns an empty grid
                Debug.LogWarning("Sudoku Data is empty.");
                return new int[SudokuRule.RowCount, SudokuRule.ColumnCount]; 
            }

            int[,] grid = new int[SudokuRule.RowCount, SudokuRule.ColumnCount];
            List<int> sudokuData = dataType == SudokuDataType.Complete ? _completeSudokuGridData : _playableSudokuGridData;

            // Converting the list to 2d array
            for (int cellIndex = 0; cellIndex < SudokuRule.TotalCellCount; cellIndex++)
            {
                int row = (int) Math.Floor((double) (cellIndex / SudokuRule.RowCount));
                int column = cellIndex % SudokuRule.ColumnCount;

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
            for (int row = 0; row < SudokuRule.RowCount; row++)
            {
                for (int column = 0; column < SudokuRule.ColumnCount; column++)
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
}