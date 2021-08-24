using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SudokuCell : Selectable
{
    /// The cell's location
    public Vector2 ID { get; private set; }

    /// The cell's actual number
    public int Answer { get; private set; }

    /// The cell's current number according to the player's input
    public int Value { get; private set; }

    /// Is this cell a clue at the start of the game 
    public bool IsClue { get; private set; }

    /// The sudoku board that contains all of the sudoku cells 
    private SudokuBoard _sudokuBoard;

    private TextMeshProUGUI _numberTMP;

    protected override void Awake()
    {
        base.Awake();
        _sudokuBoard = GetComponentInParent<SudokuBoard>();
        _numberTMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        _sudokuBoard.SelectSudokuCell(this);
    }

    /// <summary>
    /// This will store the cell's initial data.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="answer"></param>
    public void SetCellData(int row, int column, int answer)
    {
        ID = new Vector2(row, column);
        Answer = answer;
    }

    /// <summary>
    /// This will handle the player's input to the cell.
    /// </summary>
    /// <param name="number"></param>
    public void EnterValue(int number)
    {
        Value = number;
        ChangeText(number);
    }

    /// <summary>
    /// This will make the cell as a clue.
    /// Also prevents the clickable state. 
    /// </summary>
    public void SetCellAsClue()
    {
        IsClue = true;
        interactable = false;
    }

    /// <summary>
    /// Changes the cell's the text.
    /// </summary>
    /// <param name="number"></param>
    private void ChangeText(int number)
    {
        _numberTMP.text = number != 0 ? number.ToString() : "";
    }
}