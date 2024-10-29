namespace Store.View.Infrastructure
{
    internal static class TextAlignment
    {
        private static Dictionary<TextAlignmentOptions, Func<string, int, string>> _alignmentHandlers =
            new Dictionary<TextAlignmentOptions, Func<string, int, string>>()
            {
                {TextAlignmentOptions.Left, AlignAtLeft},
                {TextAlignmentOptions.Center, AlignAtCenter},
                {TextAlignmentOptions.Right, AlignAtRight}
            };

        public static string AlignText(string text, int maxLength,TextAlignmentOptions alignmentOptions) 
        { 
            return _alignmentHandlers[alignmentOptions](text, maxLength);
        }

        private static string AlignAtRight(string text, int maxLength)
        {
            string output = string.Empty;

            int whiteSpacesCount = maxLength - text.Length;

            output = AddWhiteSpaces(output, whiteSpacesCount);

            output += text;

            return output;
        }

        private static string AlignAtLeft(string text, int maxLength)
        {
            string output = text;

            int whiteSpacesCount = maxLength - text.Length;

            output = AddWhiteSpaces(output, whiteSpacesCount);

            return output;
        }

        private static string AlignAtCenter(string text, int maxLength)
        {
            string output = string.Empty;

            int whiteSpacesCount = maxLength - text.Length;

            if (IsEven(whiteSpacesCount))
            {
                output = AddWhiteSpaces(output, whiteSpacesCount / 2);

                output += text;

                output = AddWhiteSpaces(output, whiteSpacesCount / 2);
            }
            else
            {
                output = AddWhiteSpaces(output, whiteSpacesCount / 2);

                output += text;

                output = AddWhiteSpaces(output, whiteSpacesCount / 2 + whiteSpacesCount % 2);
            }

            return output;
        }

        private static string AddWhiteSpaces(string text, int whiteSpacesCount)
        {
            for (int i = 0; i < whiteSpacesCount; i++)
            {
                text += " ";
            }

            return text;
        }

        private static bool IsEven(int number)
        {
            return number % 2 == 0;
        }
    }
}
