using System.Collections.ObjectModel;

namespace Store.Model.RoleInterfaces
{
    public interface IHaveInventory
    {
        public IReadOnlyDictionary<Product, int> InventoryInfo { get; }
    }
}
