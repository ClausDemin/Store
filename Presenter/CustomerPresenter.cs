using Store.Model;
using Store.Model.RoleInterfaces;

namespace Store.Presenter
{
    public class CustomerPresenter
    {
        public CustomerPresenter(int moneyAmount)
        {
            Customer = new Customer(moneyAmount);
        }

        public ICustomer Customer { get; private set; }
        public float Balance => Customer.Balance;

        public IEnumerable<KeyValuePair<Product, int>> Inventory => Customer.InventoryInfo;

        public void DoPayment(List<Product> productCart, float paymentAmount)
        {
            Customer.DoPayment(productCart, paymentAmount);
        }

        public int GetProductsCount()
        {
            int count = 0;

            if (Inventory != null)
            {
                count = Inventory.Count();
            }

            return count;
        }

        public string GetCustomerBalanceData()
        {
            string data = $"Ваш баланс: {Balance}";
            return data;
        }
    }
}
