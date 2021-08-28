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
        SetColors();
        _sudokuBoard = GetComponentInParent<SudokuBoard>();
        _numberTMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SudokuBoard.OnCellSelected += CheckHighlight;
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        SudokuBoard.OnCellSelected -= CheckHighlight;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        SelectThisCell();
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
        CheckAnswer();
        _sudokuBoard.SelectSudokuCell(null);
    }

    /// <summary>
    /// This will make the cell as a clue.
    /// Also prevents the clickable state. 
    /// </summary>
    public void SetCellAsClue()
    {
        IsClue = true;
        interactable = false;
        _numberTMP.color = Sudoku.ClueTextColor;
    }

    /// <summary>
    /// Changes the cell's the text.
    /// </summary>
    /// <param name="number"></param>
    private void ChangeText(int number)
    {
        _numberTMP.text = number != 0 ? number.ToString() : "";
    }

    /// <summary>
    /// Setting pre-defined colors.
    /// </summary>
    private void SetColors()
    {
        ColorBlock colorBlock = this.colors;
        colorBlock.normalColor = Sudoku.NormalColor;
        colorBlock.disabledColor = Sudoku.ClueColor;
        colorBlock.selectedColor = Sudoku.SelectedColor;
        colorBlock.highlightedColor = Sudoku.HighlightColor;
        this.colors = colorBlock;
    }

    /// <summary>
    /// When this cell clicked, inform the board that this cell is chosen.
    /// </summary>
    private void SelectThisCell()
    {
        if (_sudokuBoard.CurrentlySelectedSudokuCell != this)
        {
            _sudokuBoard.SelectSudokuCell(this);
        }
        else
        {
            _sudokuBoard.SelectSudokuCell(null);
        }
    }

    /// <summary>
    /// Checks the answer and if it's wrong changes the color of the text. 
    /// </summary>
    private void CheckAnswer()
    {
        _numberTMP.color = Value == Answer ? Sudoku.AnswerTextColor : Sudoku.WrongAnswerTextColor;
    }

    /// <summary>
    /// Compares the cell's and the selected cell id,
    /// if it's in the same row, column or inside the same region, highlights the cell.
    /// </summary>
    /// <param name="selectedCellID">Row and column indexes of the selected sudoku cell</param>
    private void CheckHighlight(Vector2 selectedCellID)
    {
        // Set cell's state to normal unless it's the selected one
        DoStateTransition(selectedCellID == ID ? SelectionState.Selected : SelectionState.Normal, true);

        int row = (int) selectedCellID.x;
        int column = (int) selectedCellID.y;
        
        // Checking ID if it's in the same row
        if (row == ID.x)
        {
            DoStateTransition(SelectionState.Highlighted, true);
        }
        // Checking ID if it's in the same column
        if (column == ID.y)
        {
            DoStateTransition(SelectionState.Highlighted, true);
        }
        
        // Calculating the region's first and last indexes
        int regionStartOfRowIndex = row - (row % Sudoku.RegionRowCount);
        int regionStartOfColumnIndex = column - (column % Sudoku.RegionColumnCount);

        int regionEndOfRowIndex = regionStartOfRowIndex + Sudoku.RegionRowCount;
        int regionEndOfColumnIndex = regionStartOfColumnIndex + Sudoku.RegionColumnCount;

        // Checking ID if it's inside the region
        if (ID.x >= regionStartOfRowIndex && ID.x < regionEndOfRowIndex 
                                          && ID.y >= regionStartOfColumnIndex && ID.y < regionEndOfColumnIndex)
        {
            DoStateTransition(SelectionState.Highlighted, true);
        }
    }
}