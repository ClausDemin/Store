using Store.View.Infrastructure;

namespace Store.View.TableView
{
    internal class TableCell
    {
        public TableCell(string text, TextAlignmentOptions alignmentOptions = TextAlignmentOptions.Left) 
        { 
            Text = text;
        } 

        public string Text { get; set; }
        public int Length => Text.Length;
        public TextAlignmentOptions Alignment { get; set; }
    }
}
