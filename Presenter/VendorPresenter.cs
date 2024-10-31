using Store.Model;
using Store.Model.Extensions;
using Store.Model.Infrastructure;
using Store.Model.RoleInterfaces;

namespace Store.Presenter
{
    public class VendorPresenter
    {
        private IVendor _vendor;
        private ProductService _presenter;

        public VendorPresenter(ProductService presenter, uint minProductCount, uint maxProductCount)
        {
            _presenter = presenter;
            _vendor = new Vendor(_presenter.CreateProductList(minProductCount, maxProductCount));
        }

        public IReadOnlyDictionary<Product, int> Storage => _vendor.InventoryInfo;
        public IReadOnlyDictionary<Product, int> ProductsCart => _vendor.ProductsCart;

        public float Balance => _vendor.Balance;

        private bool TryAddProductInCart(Products productType)
        {
            var product = _presenter.GetProduct(productType);

            return _vendor.TryAddProductInCart(product);
        }

        public bool TryAddProductInCart(string productName)
        {
            var product = _presenter.FindProduct(productName);

            return TryAddProductInCart(product);
        }

        private bool TryRemoveProductFromCart(Products productType)
        {
            var product = _presenter.GetProduct(productType);

            return _vendor.TryRemoveProductFromCart(product);
        }

        public bool TryRemoveProductFromCart(string productName)
        {
            var product = _presenter.FindProduct(productName);

            return TryRemoveProductFromCart(product);
        }

        public int GetProductAmountFromCart(string productName)
        {
            int productAmount = 0;

            var product = _presenter.GetProduct(_presenter.FindProduct(productName));

            var productCart = ProductsCart.ToDictionary();

            if (productCart.ContainsKey(product))
            {
                productAmount = productCart[product];
            }

            return productAmount;
        }

        public string GetPayment(ICustomer customer)
        {
            string result = string.Empty;

            if (_vendor.GetPayment(customer))
            {
                result = "Успешно оплачено!";
                return result;
            }
            else
            {
                result = "Недостаточно средств на счете!";
            }

            return result;
        }

        public string GerPreliminaryData()
        {
            string data = $"Товаров в корзине: {GetOverallProductsInCartCount()} " +
                $"На общую сумму {GetPaymentAmount()}";

            return data;
        }

        public string GetVendorBalanceData()
        {
            string data = $"Баланс продавца: {Balance}";
            return data;
        }

        public int GetUniqueProductsCount()
        {
            int count = 0;

            if (Storage != null)
            {
                count = Storage.Count();
            }

            return count;
        }

        private int GetOverallProductsInCartCount()
        {
            int total = 0;

            foreach (var product in ProductsCart)
            {
                total += product.Value;
            }

            return total;
        }

        private float GetPaymentAmount()
        {
            var productCart = new List<Product>();

            foreach (var product in ProductsCart)
            {
                productCart.AddMany(product.Key, (uint)product.Value);
            }

            return _vendor.GetPaymentAmount(productCart);
        }
    }
}
