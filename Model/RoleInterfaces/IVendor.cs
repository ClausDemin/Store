using Store.Model.Infrastructure;

namespace Store.Model.RoleInterfaces
{
    public interface IVendor : IWalletHolder, IHaveInventory
    {
        public IEnumerable<KeyValuePair<Product, int>> ProductsCart { get; }
        
        public IEnumerable<KeyValuePair<Product, int>> InventoryInfo { get; }

        public float Balance { get; }

        public bool GetPayment(ICustomer customer);

        public void AddRange(List<Product> products);

        public bool TryAddProductInCart(Product product);

        public bool TryRemoveProductFromCart(Product product);

        public float GetPaymentAmount(List<Product> productsCart);
    }
}
