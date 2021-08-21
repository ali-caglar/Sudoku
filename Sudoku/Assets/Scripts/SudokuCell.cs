using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SudokuCell : Selectable
{
    /// The cell's location
    public Vector2 ID { get; private set; }

    /// The cell's number
    public int Number { get; private set; }

    private TextMeshProUGUI _numberTMP;

    protected override void Awake()
    {
        base.Awake();
        _numberTMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetID(int row, int column)
    {
        ID = new Vector2(row, column);
    }

    public void SetNumber(int number)
    {
        Number = number;
        _numberTMP.text = number.ToString();
    }
}