using Store.View.Interfaces;
using System.Drawing;

namespace Store.View.AbstractView
{
    public abstract class AbstractLabel : IUIElement
    {
        public Point LocalPosition { get; set; }

        public int Width => Text.Length;
        public abstract string Text { get; }
        public int Height => 1;

        public virtual void Show()
        {
            ClearOutput();

            Console.SetCursorPosition(LocalPosition.X, LocalPosition.Y);
            Console.Write(Text);
        }

        public virtual void ClearOutput()
        {
            for (int i = LocalPosition.X + Width; i > LocalPosition.X; i--)
            {
                Console.SetCursorPosition(i, LocalPosition.Y);
                Console.Write(' ');
            }
        }
    }
}
