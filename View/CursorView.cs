using Store.Presenter;
using Store.View.Infrastructure;
using Store.View.Interfaces;
using Store.View.TableView;
using System.Drawing;

namespace Store.View
{
    public class CursorView: IUIElement
    {
        private Table _cursorColumn;
        private VendorPresenter _presenter;

        public CursorView(Point localPosition, int rowsCount, VendorPresenter presenter, char cursorSymbol = '>')
        {
            _cursorColumn = new Table(localPosition, rowsCount, GetCursorColumn(rowsCount, cursorSymbol));
            _cursorColumn.SetHeader([" "]);
            _cursorColumn.SetColumnAlignment(0, TextAlignmentOptions.Right);

            _presenter = presenter;
            LocalPosition = localPosition;
            CursorSymbol = cursorSymbol;

            CursorPositionLeft = _cursorColumn.Width - _cursorColumn.BorderLength - 1;
        }

        public Point LocalPosition { get; }
        public char CursorSymbol { get; }
        public int RowsCount => _cursorColumn.RowsCount;
        public int Width => _cursorColumn.Width;
        public int Height => RowsCount;

        public int CursorPositionLeft { get; private set; }
        public int CursorPositionTop { get; private set; }

        private string[,] GetCursorColumn(int rowsCount, char cursorSymbol) 
        {
            string[,] cursorColumn = new string[rowsCount, 1];

            cursorColumn[0, 0] = cursorSymbol.ToString();

            for (int i = 1; i < rowsCount; i++) 
            {
                cursorColumn[i,0] = string.Empty;
            }

            return cursorColumn;
        }

        public void Show() 
        { 
            _cursorColumn.Show();
        }

        public void MoveCursor(CursorMovement direction) 
        {
            if (IsMovementAvailable(CursorPositionTop, direction)) 
            {
                _cursorColumn.SetValue(CursorPositionTop, 0, " ");
                
                CursorPositionTop += (int)direction;

                _cursorColumn.SetValue(CursorPositionTop, 0, CursorSymbol.ToString());

                Show();
            }
        }

        private bool IsMovementAvailable(int cursorPositionTop, CursorMovement direction) 
        {
            var wishedPosition = (cursorPositionTop + (int)direction);

            return wishedPosition >= 0 && wishedPosition < RowsCount;
        }
    }

    public enum CursorMovement
    {
        Up = -1,
        Down = 1
    }
}
