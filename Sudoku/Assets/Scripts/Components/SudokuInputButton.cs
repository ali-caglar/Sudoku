using UnityEngine;
using UnityEngine.UI;

namespace Sudoku.Components
{
    public class SudokuInputButton : MonoBehaviour
    {
        /// The number to input to the selected cell
        private int _number;

        /// The button component of this gameobject
        private Button _thisButton;
    
        /// The sudoku board that contains all of the sudoku cells 
        private SudokuBoard _sudokuBoard;

        private void Awake()
        {
            _thisButton = GetComponent<Button>();
            _sudokuBoard = FindObjectOfType<SudokuBoard>();
        }

        private void OnEnable() => _thisButton.onClick.AddListener(InputNumber);
        private void OnDisable() => _thisButton.onClick.RemoveListener(InputNumber);

        /// <summary>
        /// Assigning the button's number.
        /// </summary>
        /// <param name="number"></param>
        public void SetNumber(int number)
        {
            _number = number;
        }

        /// <summary>
        /// Input the value to the selected cell.
        /// </summary>
        private void InputNumber()
        {
            if (_sudokuBoard.CurrentlySelectedSudokuCell != null)
            {
                _sudokuBoard.CurrentlySelectedSudokuCell.EnterValue(_number);
            }
        }
    }
}