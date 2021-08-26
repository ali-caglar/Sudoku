using UnityEngine;
using TMPro;

public class SudokuInputManager : GridCreator
{
    protected override void Start()
    {
        base.Start();
        CreateNumberButtons();
    }
    
    /// <summary>
    /// Creates the input numbers.
    /// </summary>
    private void CreateNumberButtons()
    {
        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int column = 0; column < _numberOfColumns; column++)
            {
                GameObject slot = _slotsOfGrid[row, column];
                
                TextMeshProUGUI numberTMP = slot.GetComponentInChildren<TextMeshProUGUI>();
                numberTMP.text = $"{column + 1}"; // This will return button number

                SudokuInputButton inputButton = slot.AddComponent<SudokuInputButton>();
                inputButton.SetNumber(column + 1);
            }
        }
    }
}