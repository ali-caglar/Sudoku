using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SudokuNumberInput : MonoBehaviour
{
    [SerializeField] private GameObject _numberButton;

    /// The internal margin between the top of the buttons grid and the first button
    [Header("Grid Padding")] [SerializeField]
    private int _paddingTop = 20;

    /// The internal margin between the right of the buttons grid and the last button
    [SerializeField] private int _paddingRight = 20;

    /// The internal margin between the bottom of the buttons grid and the last button
    [SerializeField] private int _paddingBottom = 20;

    /// The internal margin between the left of the buttons grid and the first button
    [SerializeField] private int _paddingLeft = 20;

    /// The margin to apply between buttons
    [SerializeField] private float _margin = 10;

    /// The horizontal and vertical size of the buttons
    private Vector2 _buttonSize = new Vector2(50, 50);
    
    /// The rect transform object of this gameobject
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    /// The grid layout used to display the sudoku input numbers
    public HorizontalLayoutGroup SudokuInputGrid { get; private set; }

    private void Start()
    {
        ResizeButton();
        AddGridLayoutGroup();
        ResizeBoard();
        CreateNumberButtons();
    }

    /// <summary>
    /// Resizes the input numbers, depends the screen size, the padding and margin.
    /// </summary>
    private void ResizeButton()
    {
        Canvas rootCanvas = GetComponentInParent<Canvas>().rootCanvas;

        float widthOfScreen = Screen.width / rootCanvas.scaleFactor;
        float widthOfMargin = _paddingLeft + _margin * (9 - 1) + _paddingRight;

        float newSize = (widthOfScreen - widthOfMargin) / 9;

        _buttonSize = new Vector2(newSize, newSize);
    }

    /// <summary>
    /// Adds a horizontal layout group if there isn't one.
    /// </summary>
    private void AddGridLayoutGroup()
    {
        if (GetComponentInChildren<HorizontalLayoutGroup>() == null)
        {
            GameObject inputGrid = new GameObject("SudokuInputGrid");
            inputGrid.transform.parent = transform;
            inputGrid.transform.position = transform.position;
            inputGrid.transform.localScale = Vector3.one;

            SudokuInputGrid = inputGrid.AddComponent<HorizontalLayoutGroup>();
        }

        if (SudokuInputGrid == null)
        {
            SudokuInputGrid = GetComponentInChildren<HorizontalLayoutGroup>();
        }

        SudokuInputGrid.padding.top = _paddingTop;
        SudokuInputGrid.padding.right = _paddingRight;
        SudokuInputGrid.padding.bottom = _paddingBottom;
        SudokuInputGrid.padding.left = _paddingLeft;
        SudokuInputGrid.spacing = _margin;
    }

    /// <summary>
    /// Resizes the input buttons' panel, depends the padding and margin.
    /// </summary>
    private void ResizeBoard()
    {
        float newWidth = _paddingLeft + _buttonSize.x * 9 + _margin * (9 - 1) + _paddingRight;
        float newHeight = _paddingTop + _buttonSize.y + _margin + _paddingBottom;

        Vector2 newSize = new Vector2(newWidth, newHeight);
        _rectTransform.sizeDelta = newSize;
        SudokuInputGrid.GetComponent<RectTransform>().sizeDelta = newSize;
    }

    /// <summary>
    /// Creates the input numbers.
    /// </summary>
    private void CreateNumberButtons()
    {
        for (int i = 1; i <= 9; i++)
        {
            GameObject numberSlot = Instantiate(_numberButton, SudokuInputGrid.transform);

            numberSlot.transform.position = transform.position;
            numberSlot.GetComponent<RectTransform>().localScale = Vector3.one;
            numberSlot.name = $"Number {i}";

            TextMeshProUGUI numberTMP = numberSlot.GetComponentInChildren<TextMeshProUGUI>();
            numberTMP.text = i.ToString();
        }
    }
}