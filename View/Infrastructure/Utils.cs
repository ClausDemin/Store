namespace Store.View.Infrastructure
{
    public static class Utils
    {
        public static string[,] ConvertToStringArray(List<List<string>> data)
        {
            var result = new string[data.Count, data[0].Count];

            var rowsCount = result.GetLength(0);
            var columnsCount = result.GetLength(1);

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    result[i, j] = data[i][j];
                }
            }

            return result;
        }
    }
}
