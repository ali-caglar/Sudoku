using UnityEngine;
using UnityEngine.UI;
using Sudoku.Enums;

namespace Sudoku.Components
{
    public class GridCreator : MonoBehaviour
    {
        /// The prefab of the slot
        [Header("Slot")] 
        [SerializeField] protected GameObject _slotPrefab;

        /// The type of layout
        [Header("Layout")]
        [SerializeField] protected LayoutType _layoutType;
    
        /// The number of rows to display
        [Header("Grid Size")] 
        [SerializeField] protected int _numberOfRows = 9;

        /// The number of columns to display
        [SerializeField] protected int _numberOfColumns = 9;

        /// The internal margin between the top of the grid and the first slot
        [Header("Grid Padding")] 
        [SerializeField] protected int _paddingTop = 20;

        /// The internal margin between the right of the grid and the last slot
        [SerializeField] protected int _paddingRight = 20;

        /// The internal margin between the bottom of the grid and the last slot
        [SerializeField] protected int _paddingBottom = 20;

        /// The internal margin between the left of the grid and the first slot
        [SerializeField] protected int _paddingLeft = 20;
    
        /// The horizontal and vertical margin to apply between slot's rows and columns
        [SerializeField] protected Vector2 _slotMargin = new Vector2(5, 5);

        /// The horizontal and vertical size of the cells
        protected Vector2 _slotSize = new Vector2(50, 50);

        /// The grid layout used to display the grid in rows and columns
        protected GridLayoutGroup _gridLayoutGroup;

        /// The grid layout used to display the grid in rows
        protected HorizontalLayoutGroup _horizontalLayoutGroup;
    
        /// The rect transform object of this gameobject
        protected RectTransform _rectTransform;

        /// The slots that created by the grid
        protected GameObject[,] _slotsOfGrid;

        /// Returns the gameobject of the selected layout type
        protected GameObject _layoutGroupGameObject => _layoutType == LayoutType.Grid ? _gridLayoutGroup.gameObject : _horizontalLayoutGroup.gameObject;


        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        protected virtual void Start()
        {
            ResizeSlot();
            AddLayoutGroup();
            SetLayoutGroupSettings();
            ResizeBoard();
            CreateGrid();
        }

        /// <summary>
        /// Resizes the grid slots, depends the screen size, the padding and margin.
        /// </summary>
        protected virtual void ResizeSlot()
        {
            Canvas rootCanvas = GetComponentInParent<Canvas>().rootCanvas;

            float widthOfScreen = Screen.width / rootCanvas.scaleFactor;
            float widthOfMargin = _paddingLeft + _slotMargin.x * (_numberOfColumns - 1) + _paddingRight;
            float newSize = (widthOfScreen - widthOfMargin) / _numberOfColumns;

            _slotSize = new Vector2(newSize, newSize);
        }
    
        /// <summary>
        /// Adds a layout group if there isn't one.
        /// </summary>
        protected virtual void AddLayoutGroup()
        {
            if (GetComponentInChildren<LayoutGroup>() == null)
            {
                GameObject grid = new GameObject($"{gameObject.name} Grid");
                grid.transform.parent = transform;
                grid.transform.position = transform.position;
                grid.transform.localScale = Vector3.one;

                if (_layoutType == LayoutType.Grid)
                {
                    _gridLayoutGroup = grid.AddComponent<GridLayoutGroup>();
                }
                else
                {
                    _horizontalLayoutGroup = grid.AddComponent<HorizontalLayoutGroup>();
                }
            }
        }

        protected virtual void SetLayoutGroupSettings()
        {
            if (_layoutType == LayoutType.Grid)
            {
                if (_gridLayoutGroup == null)
                {
                    _gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();
                }
            
                _gridLayoutGroup.padding.top = _paddingTop;
                _gridLayoutGroup.padding.right = _paddingRight;
                _gridLayoutGroup.padding.bottom = _paddingBottom;
                _gridLayoutGroup.padding.left = _paddingLeft;
                _gridLayoutGroup.spacing = _slotMargin;
                _gridLayoutGroup.cellSize = _slotSize;
            }
            else
            {
                if (_horizontalLayoutGroup == null)
                {
                    _horizontalLayoutGroup = GetComponentInChildren<HorizontalLayoutGroup>();
                }
            
                _horizontalLayoutGroup.padding.top = _paddingTop;
                _horizontalLayoutGroup.padding.right = _paddingRight;
                _horizontalLayoutGroup.padding.bottom = _paddingBottom;
                _horizontalLayoutGroup.padding.left = _paddingLeft;
                _horizontalLayoutGroup.spacing = _slotMargin.x;
            }
        }
    
        /// <summary>
        /// Resizes the grid's panel, depends the number of rows/columns, the padding and margin.
        /// </summary>
        protected virtual void ResizeBoard()
        {
            float newWidth = _paddingLeft + _slotSize.x * _numberOfColumns + _slotMargin.x * (_numberOfColumns - 1) +
                             _paddingRight;
            float newHeight = _paddingTop + _slotSize.y * _numberOfRows + _slotMargin.y * (_numberOfRows - 1) +
                              _paddingBottom;

            Vector2 newSize = new Vector2(newWidth, newHeight);
            _rectTransform.sizeDelta = newSize;
            _layoutGroupGameObject.GetComponent<RectTransform>().sizeDelta = newSize;
        }
    
        /// <summary>
        /// Creates the slots.
        /// </summary>
        protected virtual void CreateGrid()
        {
            _slotsOfGrid = new GameObject[_numberOfRows, _numberOfColumns];
        
            for (int row = 0; row < _numberOfRows; row++)
            {
                for (int column = 0; column < _numberOfColumns; column++)
                {
                    GameObject slot = Instantiate(_slotPrefab, _layoutGroupGameObject.transform);

                    slot.transform.position = transform.position;
                    slot.GetComponent<RectTransform>().localScale = Vector3.one;
                    slot.name = $"Slot {row},{column}";
                
                    _slotsOfGrid[row, column] = slot;
                }
            }
        }
    }
}