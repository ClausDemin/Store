using System.Drawing;
using Store.View.Infrastructure;
using Store.View.Interfaces;

namespace Store.View.TableView
{
    public class Table : IUIElement
    {
        private TableCell[] _header;
        private TableCell[,] _table;

        private char _borderSymbol;

        public Table(Point localPosition, int rowsCount, string[,] tableData, char borderSymbol = '|')
        {
            LocalPosition = localPosition;
            FilledRowsCount = rowsCount + (tableData.GetLength(0) - rowsCount);

            rowsCount = rowsCount > tableData.GetLength(0) ? rowsCount : tableData.GetLength(0);

            _table = new TableCell[rowsCount, tableData.GetLength(1)];
            _header = new TableCell[tableData.GetLength(1)];

            IsBordered = true;
            _borderSymbol = borderSymbol;

            FillTable(tableData);
        }

        public Point LocalPosition { get; set; }
        public int FilledRowsCount { get; }
        public int RowsCount => _table.GetLength(0);
        public int ColumnsCount => _table.GetLength(1);
        public int Width => GetTableWidth();
        public bool IsBordered { get; }
        public int BorderLength => IsBordered ? LeftBorder.Length : 0;

        private string LeftBorder => $"{_borderSymbol} ";
        private string RightBorder => $" {_borderSymbol}";

        public int Height => RowsCount;

        public void SetValue(int row, int column, string text)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(row);
            ArgumentOutOfRangeException.ThrowIfNegative(column);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, RowsCount);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(column, ColumnsCount);

            _table[row, column].Text = text;
        }

        public string GetValue(int row, int column)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(row);
            ArgumentOutOfRangeException.ThrowIfNegative(column);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, RowsCount);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(column, ColumnsCount);

            return _table[row, column].Text;
        }

        public void SetHeader(string[] columnsHeaders)
        {
            ArgumentOutOfRangeException.ThrowIfNotEqual(columnsHeaders.Length, ColumnsCount);

            for (int i = 0; i < ColumnsCount; i++)
            {
                _header[i] = new TableCell(columnsHeaders[i], TextAlignmentOptions.Center);
            }
        }

        public void Show()
        {
            var text = GetText();

            for (int i = 0; i < text.GetLength(0); i++)
            {
                Console.SetCursorPosition(LocalPosition.X, LocalPosition.Y + i);

                for (int j = 0; j < text.GetLength(1); j++)
                {
                    var output = IsBordered ? $"{LeftBorder}{text[i, j]}{RightBorder}" : $" {text[i, j]} ";

                    Console.Write(output);
                }
            }
        }

        public void SetColumnAlignment(int column, TextAlignmentOptions alignmentOptions)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(column);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(column, ColumnsCount);

            for (int i = 0; i < RowsCount; i++)
            {
                _table[i, column].Alignment = alignmentOptions;
            }
        }

        private void FillTable(string[,] tableData)
        {
            var textHeight = tableData.GetLength(0);

            for (int i = 0; i < textHeight; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    _table[i, j] = new TableCell(tableData[i, j]);
                }
            }

            for (int i = textHeight; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    _table[i, j] = new TableCell(string.Empty);
                }
            }
        }

        private int GetColumnWidth(int column)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(column);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(column, ColumnsCount);

            int columnWidth = 0;

            if (_header[column].Length > columnWidth)
            {
                columnWidth = _header[column].Length;
            }

            for (int i = 0; i < RowsCount; i++)
            {
                int cellLength = _table[i, column].Text.Length;

                if (cellLength > columnWidth)
                {
                    columnWidth = cellLength;
                }
            }

            return columnWidth;
        }

        private int GetTableWidth()
        {
            int width = 0;

            for (int i = 0; i < ColumnsCount; i++)
            {
                width += GetColumnWidth(i);
            }

            width = IsBordered ? width + (LeftBorder.Length + RightBorder.Length) * ColumnsCount : width;

            return width;
        }

        private string[,] GetText()
        {
            string[,] result = new string[RowsCount + 1, ColumnsCount];

            ExtractTextFromHeader(result);

            ExtractTextFromTable(result);

            return result;
        }

        private void ExtractTextFromTable(string[,] table)
        {
            for (int i = 0; i < ColumnsCount; i++)
            {
                int columnWidth = GetColumnWidth(i);

                for (int j = 0; j < RowsCount; j++)
                {
                    var alignedText = TextAlignment.AlignText(_table[j, i].Text, columnWidth, _table[j, i].Alignment);

                    table[j + 1, i] = alignedText;
                }
            }
        }

        private void ExtractTextFromHeader(string[,] table)
        {
            for (int i = 0; i < ColumnsCount; i++)
            {
                int columnWidth = GetColumnWidth(i);

                var alignedText = TextAlignment.AlignText(_header[i].Text, columnWidth, TextAlignmentOptions.Center);

                table[0, i] = alignedText;
            }
        }
    }
}
