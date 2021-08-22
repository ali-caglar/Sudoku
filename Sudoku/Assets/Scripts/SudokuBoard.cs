using UnityEngine;
using UnityEngine.UI;

public class SudokuBoard : MonoBehaviour
{
    /// The sudoku data that contains the grid
    [Header("Sudoku Data")] 
    [SerializeField] private SudokuData _sudokuData;

    /// The number of rows to display
    [Header("Board Size")] 
    [SerializeField] private int _numberOfRows = 9;

    /// The number of columns to display
    [SerializeField] private int _numberOfColumns = 9;

    /// The internal margin between the top of the sudoku board and the first cell
    [Header("Board Padding")] 
    [SerializeField] private int _paddingTop = 20;

    /// The internal margin between the right of the sudoku board and the last cell
    [SerializeField] private int _paddingRight = 20;

    /// The internal margin between the bottom of the sudoku board and the last cell
    [SerializeField] private int _paddingBottom = 20;

    /// The internal margin between the left of the sudoku board and the first cell
    [SerializeField] private int _paddingLeft = 20;

    /// The prefab of the sudoku cell
    [Header("Sudoku Cell")] 
    [SerializeField] private GameObject _cellPrefab;

    /// The horizontal and vertical margin to apply between cell's rows and columns
    [SerializeField] private Vector2 _cellMargin = new Vector2(5, 5);

    /// The grid layout used to display the sudoku board in rows and columns
    public GridLayoutGroup SudokuBoardGrid { get; private set; }

    /// The horizontal and vertical size of the cells
    private Vector2 _cellSize = new Vector2(50, 50);

    /// The rect transform object of this gameobject
    private RectTransform _rectTransform;

    /// The slots that created by the board
    private GameObject[,] _slotsOfSudokuBoard;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        ResizeCell();
        AddGridLayoutGroup();
        ResizeBoard();
        CreateSudokuBoard();
        CreateSudokuGame();
    }

    /// <summary>
    /// Resizes the sudoku cells, depends the screen size, the padding and margin.
    /// </summary>
    private void ResizeCell()
    {
        Canvas rootCanvas = GetComponentInParent<Canvas>().rootCanvas;

        float widthOfScreen = Screen.width / rootCanvas.scaleFactor;
        float widthOfMargin = _paddingLeft + _cellMargin.x * (_numberOfColumns - 1) + _paddingRight;

        float newSize = (widthOfScreen - widthOfMargin) / _numberOfColumns;

        _cellSize = new Vector2(newSize, newSize);
    }

    /// <summary>
    /// Adds a grid layout group if there isn't one.
    /// </summary>
    private void AddGridLayoutGroup()
    {
        if (GetComponentInChildren<GridLayoutGroup>() == null)
        {
            GameObject boardGrid = new GameObject("SudokuBoardGrid");
            boardGrid.transform.parent = transform;
            boardGrid.transform.position = transform.position;
            boardGrid.transform.localScale = Vector3.one;

            SudokuBoardGrid = boardGrid.AddComponent<GridLayoutGroup>();
        }

        if (SudokuBoardGrid == null)
        {
            SudokuBoardGrid = GetComponentInChildren<GridLayoutGroup>();
        }

        SudokuBoardGrid.padding.top = _paddingTop;
        SudokuBoardGrid.padding.right = _paddingRight;
        SudokuBoardGrid.padding.bottom = _paddingBottom;
        SudokuBoardGrid.padding.left = _paddingLeft;
        SudokuBoardGrid.cellSize = _cellSize;
        SudokuBoardGrid.spacing = _cellMargin;
    }

    /// <summary>
    /// Resizes the sudoku board's panel, depends the number of rows/columns, the padding and margin.
    /// </summary>
    private void ResizeBoard()
    {
        float newWidth = _paddingLeft + _cellSize.x * _numberOfColumns + _cellMargin.x * (_numberOfColumns - 1) +
                         _paddingRight;
        float newHeight = _paddingTop + _cellSize.y * _numberOfRows + _cellMargin.y * (_numberOfRows - 1) +
                          _paddingBottom;

        Vector2 newSize = new Vector2(newWidth, newHeight);
        _rectTransform.sizeDelta = newSize;
        SudokuBoardGrid.GetComponent<RectTransform>().sizeDelta = newSize;
    }

    /// <summary>
    /// Creates the sudoku slots.
    /// </summary>
    private void CreateSudokuBoard()
    {
        _slotsOfSudokuBoard = new GameObject[_numberOfRows, _numberOfColumns];
        
        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int column = 0; column < _numberOfColumns; column++)
            {
                GameObject slot = Instantiate(_cellPrefab, SudokuBoardGrid.transform);

                slot.transform.position = transform.position;
                slot.GetComponent<RectTransform>().localScale = Vector3.one;
                slot.name = $"Slot {row},{column}";
                
                _slotsOfSudokuBoard[row, column] = slot;
            }
        }
    }

    /// <summary>
    /// This will create the game by handling the values of the sudoku cells.
    /// </summary>
    private void CreateSudokuGame()
    {
        int[,] completeSudokuGrid = _sudokuData.CompleteSudokuGrid;
        int[,] playableSudokuGrid = _sudokuData.PlayableSudokuGrid;
        
        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int column = 0; column < _numberOfColumns; column++)
            {
                GameObject slot = _slotsOfSudokuBoard[row, column];
                SudokuCell sudokuCell = slot.AddComponent<SudokuCell>();

                // Sending initial data to cell
                int cellAnswer = completeSudokuGrid[row, column];
                sudokuCell.SetCellData(row, column, cellAnswer);
                
                // Creating the game
                int cellValue = playableSudokuGrid[row, column];
                sudokuCell.EnterValue(cellValue);
                
                if (cellValue != 0)
                {
                    sudokuCell.SetCellAsClue();
                }
            }
        }
    }
}