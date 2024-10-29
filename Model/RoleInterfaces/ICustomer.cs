namespace Store.Model.RoleInterfaces
{
    public interface ICustomer : IWalletHolder, IHaveInventory
    {
        public bool DoPayment(List<Product> productCart, float paymentAmount);
    }
}
