using UnityEngine;

public static class Sudoku
{
    public const int RowCount = 9;
    public const int ColumnCount = 9;
    public const int TotalCellCount = 81;
    public const int EmptyCell = 0;
    public const int RegionRowCount = 3;
    public const int RegionColumnCount = 3;

    public static readonly Color ClueColor = Color.white;
    public static readonly Color NormalColor = Color.white;
    public static readonly Color SelectedColor = Color.white;
    public static readonly Color HighlightColor = Color.cyan;
    public static readonly Color ClueTextColor = Color.black;
    public static readonly Color AnswerTextColor = Color.blue;
    public static readonly Color WrongAnswerTextColor = Color.red;
}