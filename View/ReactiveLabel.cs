using Store.View.AbstractView;
using Store.View.Interfaces;
using System.Drawing;

namespace Store.View
{
    public class ReactiveLabel : AbstractLabel
    {
        private Func<string> _propertyGetter;

        public ReactiveLabel(Point position, Func<string> propertyGetter) 
        { 
            LocalPosition = position;
            _propertyGetter = propertyGetter;
        }

        public override string Text => _propertyGetter.Invoke();
    }
}
