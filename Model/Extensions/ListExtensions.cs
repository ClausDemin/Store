namespace Store.Model.Extensions
{
    public static class ListExtensions
    {
        public static void AddMany<T>(this List<T> list, T item, uint count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(item);
            }
        }
    }
}
