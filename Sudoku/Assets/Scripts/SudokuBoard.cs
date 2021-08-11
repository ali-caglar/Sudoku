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

    /// The internal margin between the top of the inventory panel and the first slots
    [Header("Board Padding")]
    [SerializeField] private int _paddingTop = 20;

    /// The internal margin between the right of the inventory panel and the last slots
    [SerializeField] private int _paddingRight = 20;

    /// The internal margin between the bottom of the inventory panel and the last slots
    [SerializeField] private int _paddingBottom = 20;

    /// The internal margin between the left of the inventory panel and the first slots
    [SerializeField] private int _paddingLeft = 20;

    /// The prefab of the sudoku slot
    [Header("Slots")] 
    [SerializeField] private GameObject _slotPrefab;

    /// The horizontal and vertical margin to apply between slots rows and columns
    [SerializeField] private Vector2 _slotMargin = new Vector2(5, 5);

    /// The grid layout used to display the inventory in rows and columns
    public GridLayoutGroup SudokuBoardGrid { get; private set; }

    /// The horizontal and vertical size of the slots
    private Vector2 _slotSize = new Vector2(50, 50);

    /// The rect transform object of this gameobject
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        ResizeSlot();
        AddGridLayoutGroup();
        ResizeBoard();
        CreateSlots();
    }

    /// <summary>
    /// Resizes the sudoku slots, depends the screen size, the padding and margin
    /// </summary>
    private void ResizeSlot()
    {
        Canvas rootCanvas = GetComponentInParent<Canvas>().rootCanvas;

        float widthOfScreen = Screen.width / rootCanvas.scaleFactor;
        float widthOfMargin = _paddingLeft + _slotMargin.x * (_numberOfColumns - 1) + _paddingRight;

        float newSize = (widthOfScreen - widthOfMargin) / _numberOfColumns;

        _slotSize = new Vector2(newSize, newSize);
    }

    /// <summary>
    /// Adds a grid layout group if there isn't one
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
        SudokuBoardGrid.cellSize = _slotSize;
        SudokuBoardGrid.spacing = _slotMargin;
    }

    /// <summary>
    /// Resizes the sudoku board's panel, depends the number of rows/columns, the padding and margin
    /// </summary>
    private void ResizeBoard()
    {
        float newWidth = _paddingLeft + _slotSize.x * _numberOfColumns + _slotMargin.x * (_numberOfColumns - 1) +
                         _paddingRight;
        float newHeight = _paddingTop + _slotSize.y * _numberOfRows + _slotMargin.y * (_numberOfRows - 1) +
                          _paddingBottom;

        Vector2 newSize = new Vector2(newWidth, newHeight);
        _rectTransform.sizeDelta = newSize;
        SudokuBoardGrid.GetComponent<RectTransform>().sizeDelta = newSize;
    }

    /// <summary>
    /// Creates the slots
    /// </summary>
    private void CreateSlots()
    {
        for (int x = 0; x < _numberOfRows; x++)
        {
            for (int y = 0; y < _numberOfColumns; y++)
            {
                GameObject slot = Instantiate(_slotPrefab, SudokuBoardGrid.transform);

                slot.transform.position = transform.position;
                slot.GetComponent<RectTransform>().localScale = Vector3.one;
                slot.name = $"Slot {x},{y}";

                SudokuSlot sudokuSlot = slot.AddComponent<SudokuSlot>();
                
                sudokuSlot.SetID(x, y);
                sudokuSlot.SetNumber(0);
            }
        }
    }
}