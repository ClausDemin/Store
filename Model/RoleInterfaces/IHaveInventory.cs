namespace Store.Model.RoleInterfaces
{
    public interface IHaveInventory
    {
        public IEnumerable<KeyValuePair<Product, int>> InventoryInfo { get; }
    }
}
