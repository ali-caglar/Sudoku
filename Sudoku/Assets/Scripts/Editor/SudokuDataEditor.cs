using UnityEngine;
using UnityEditor;
using Sudoku.ScriptableObjects;

namespace Sudoku.Editor
{
    [CustomEditor(typeof(SudokuData))]
    public class SudokuDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SudokuData sudokuData = (SudokuData) target;
            if (GUILayout.Button("Generate Sudoku Data"))
            {
                sudokuData.GenerateSudokuGrid();
            }

        }
    }
}