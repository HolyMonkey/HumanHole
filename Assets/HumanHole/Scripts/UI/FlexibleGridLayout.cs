using UnityEngine;
using UnityEngine.UI;

namespace HumanHole.Scripts.UI
{
    public class FlexibleGridLayout : LayoutGroup
    {
        [SerializeField] private int _rows;
        [SerializeField] private int _columns;
        [SerializeField] private Vector2 _cellSize;
        [SerializeField] private Vector2 _spacing;
        [SerializeField] private FitType _fitType;
        [SerializeField] private bool _fitX;
        [SerializeField] private bool _fitY;
    
        public enum Alignment {
            Horizontal,
            Vertical
        }
        public enum FitType {
            Uniform,
            Width,
            Height,
            FixedRows,
            FixedColumns,
            FixedBoth
        }
    
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
        
            CalculateRowsAndColumnsCount();
        
            switch (_fitType)
            {
                case FitType.Width:
                case FitType.FixedColumns:
                    _rows = Mathf.CeilToInt(transform.childCount / (float)_columns);
                    break;
                case FitType.Height:
                case FitType.FixedRows:
                    _columns = Mathf.CeilToInt(transform.childCount / (float)_rows);
                    break;
            }
        
            CalculateContainerHeightAndWidth();
            SetChildrenAlongAxis();
        }

        private void CalculateRowsAndColumnsCount()
        {
            if (_fitType == FitType.Width || _fitType == FitType.Height || _fitType == FitType.Uniform)
            {
                float sqrRt = Mathf.Sqrt(transform.childCount);
                _rows = Mathf.CeilToInt(sqrRt);
                _columns = Mathf.CeilToInt(sqrRt);
            }
        }

        private void CalculateContainerHeightAndWidth()
        {
            //Получим ширину и высоту контейнера
            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            //Определим размер для каждого дочернего элемента
        
            float cellWidth = ((parentWidth / (float) _columns) - (_spacing.x / (float) _columns) * (_columns - 1) - (padding.left / (float) _columns) - (padding.right / (float) _columns));
            float cellHeight = ((parentHeight / (float) _rows) - (_spacing.y/(float)_rows) * (_rows - 1) - (padding.top / (float) _rows) - (padding.bottom / (float) _rows));

            _cellSize.x = _fitX ? cellWidth : _cellSize.x;
            _cellSize.y = _fitY ? cellHeight: _cellSize.y;
        }

        private void SetChildrenAlongAxis()
        {
            int columnCount = 0;
            int rowCount = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                rowCount = i / _columns;
                columnCount = i % _columns;
                var xPosition = (_cellSize.x * columnCount) + (_spacing.x * columnCount) + padding.left;
                var yPosition = (_cellSize.y * rowCount) + (_spacing.y * rowCount) + padding.top;

                var item = rectChildren[i];
            
                SetChildAlongAxis(item, 0, xPosition, _cellSize.x);
                SetChildAlongAxis(item, 1, yPosition, _cellSize.y);
            }
        }

        public override void CalculateLayoutInputVertical()
        {
        
        }

        public override void SetLayoutHorizontal()
        {
        
        }

        public override void SetLayoutVertical()
        {
        
        }
    }
}
