using Store.Model.Infrastructure;
using Store.Model.RoleInterfaces;
using System.Collections.ObjectModel;

namespace Store.Model
{
    public class Customer : ICustomer
    {
        private Inventory _inventory;
        private Wallet _wallet;

        public Customer(float moneyAmount = 0)
        {
            _inventory = new Inventory();
            _wallet = new Wallet(moneyAmount);
        }

        public float Balance => _wallet.Balance;

        public IReadOnlyDictionary<Product, int> InventoryInfo => _inventory.Products;

        public bool DoPayment(List<Product> productCart, float paymentAmount)
        {
            if (_wallet.IsPaymentAvailable(paymentAmount))
            {
                _wallet.WithdrawMoney(paymentAmount);

                foreach (Product product in productCart)
                {
                    _inventory.AddProduct(product);
                }

                return true;
            }

            return false;
        }
    }
}
