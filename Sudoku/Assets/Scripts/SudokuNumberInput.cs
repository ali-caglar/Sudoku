using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SudokuNumberInput : MonoBehaviour
{
    [SerializeField] private GameObject _numberButton;
    
    /// The grid layout used to display the sudoku input numbers
    public HorizontalLayoutGroup SudokuInputGrid { get; private set; }
    
    private void Start()
    {
        AddGridLayoutGroup();
        CreateNumberButtons();
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
        
        // SudokuInputGrid.padding.top = _paddingTop;
        // SudokuInputGrid.padding.right = _paddingRight;
        // SudokuInputGrid.padding.bottom = _paddingBottom;
        // SudokuInputGrid.padding.left = _paddingLeft;
        // SudokuInputGrid.cellSize = _cellSize;
        // SudokuInputGrid.spacing = _cellMargin;
    }

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