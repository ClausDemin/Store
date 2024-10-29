using Store.View.AbstractView;
using Store.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.View
{
    public class Label : AbstractLabel
    {
        private string _text;

        public Label(Point position, string text)
        {
            LocalPosition = position;
            _text = text;
        }

        public override string Text => _text;

        public void SetText(string text) 
        { 
            _text = text;
        }
    }
}
