using System.Drawing;

namespace Store.View.Interfaces
{
    public interface IUIElement
    {
        public Point LocalPosition { get; }
        public int Width { get; }
        public int Height { get; }
        public void Show();
    }
}
