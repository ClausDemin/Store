using Store.Model;
using Store.View.Interfaces;
using System.Drawing;

namespace Store.View.AbstractView
{
    public abstract class AbstractTableView: IUIElement
    {
        public abstract Point LocalPosition { get; protected set; }
        public abstract int Width { get; }
        public abstract int RowsCount { get; }
        public int Height => RowsCount + 1;

        public abstract void Show();

        public virtual void Redraw()
        {
            ClearOutput();
            Show();
        }

        protected virtual void ClearOutput() 
        {
            var position = new Point(LocalPosition.X + Width, LocalPosition.Y + RowsCount);

            for (int i = position.Y; i >= LocalPosition.Y; i--)
            {
                Console.SetCursorPosition(LocalPosition.X, i);

                for (int j = LocalPosition.X; j < position.X; j++)
                {
                    Console.Write(" ");
                }
            }
        }
    }
}
