using Store.Model.Infrastructure;
using Store.Model.RoleInterfaces;

namespace Store.Model
{
    public class Vendor: IVendor
    {
        private Inventory _productsCart;
        private Inventory _storage;
       
        private Wallet _wallet;
        
        public Vendor(List<Product> storage)
        {
            _storage = new Inventory();
            _storage.AddRange(storage);
            
            _productsCart = new Inventory();
            _wallet = new Wallet();
       
        }
              
        public float Balance => _wallet.Balance;

        public IEnumerable<KeyValuePair<Product, int>> InventoryInfo => _storage.Products;
        
        public IEnumerable<KeyValuePair<Product, int>> ProductsCart => _productsCart.Products;

        public bool TryAddProductInCart(Product product) 
        {
            if (IsAbleToAdd(product)) 
            {
                _productsCart.AddProduct(product);

                return true;    
            }

            return false;
        }

        public bool TryRemoveProductFromCart(Product product) 
        {
            if (IsAbleToRemove(product)) 
            { 
                _productsCart.RemoveProduct(product);

                return true;
            }
            return false;
        }

        public bool GetPayment(ICustomer customer)
        {
            var productsCart = _productsCart.ConvertToList();
            var paymentAmount = GetPaymentAmount(productsCart);

            if (customer.DoPayment(productsCart, paymentAmount))
            {
                _wallet.IncreaseBalance(paymentAmount);

                SellProducts(productsCart);

                _productsCart = new Inventory();

                return true;
            }

            return false;
        }

        public void AddRange(List<Product> products)
        {
            _storage.AddRange(products);
        }

        public float GetPaymentAmount(List<Product> productsCart)
        {
            float paymentAmount = 0;

            foreach (Product product in productsCart)
            {
                paymentAmount += product.Cost;
            }

            return paymentAmount;
        }

        private void SellProducts(List<Product> productsCart) 
        {
            foreach (var product in productsCart) 
            { 
                _storage.RemoveProduct(product);
            }
        }

        private bool IsAbleToAdd(Product product) 
        {
            if (_storage.Contains(product))
            {
                if (_productsCart.Contains(product))
                {
                    var remainingCount = _storage.GetCount(product);
                    var alreadyInCartCount = _productsCart.GetCount(product);

                    if (remainingCount > alreadyInCartCount)
                    {
                        return true;
                    }
                }
                else 
                { 
                    return true;
                }
            }

            return false;
        }

        private bool IsAbleToRemove(Product product) 
        {
            return _productsCart.Contains(product);
        }
    }
}
