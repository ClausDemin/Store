using Store.Model.Extensions;

namespace Store.Model.Infrastructure
{
    public class ProductsFactory
    {
        private Dictionary<Products, string> _productsLocale = new Dictionary<Products, string>()
        {
            {Products.PotionOfHealth,  "Зелье здоровья"},
            {Products.PotionOfMana, "Зелье магической энергии" },
            {Products.PotionOfStamina, "Зелье выносливости" },
            {Products.PotionOfAnger, "Зелье злости" },
            {Products.PotionOfLuck, "Зелье удачи" }
        };

        private Dictionary<Products, float> _productsCost = new Dictionary<Products, float>()
        {
            {Products.PotionOfHealth,  5f},
            {Products.PotionOfMana, 5f },
            {Products.PotionOfStamina, 5f },
            {Products.PotionOfAnger, 10f },
            {Products.PotionOfLuck, 15f }
        };

        private Dictionary<string, Products> _nameBindings = new Dictionary<string, Products>();

        public ProductsFactory()
        {
            ApplyBindings();
        }

        public List<Product> CreateProductList(uint minProductsCount, uint maxProductsCount)
        {
            var randomCount = new Random();

            List<Product> products = new List<Product>();

            foreach (var product in _productsCost)
            {
                products.AddMany(new Product(product.Key, _productsLocale[product.Key], product.Value),
                    (uint)randomCount.Next((int)minProductsCount, (int)maxProductsCount + 1));
            }

            return products;
        }

        public Product CreateProduct(Products productType) 
        {
            return new Product(productType, _productsLocale[productType], _productsCost[productType]);
        }

        public Products GetProductType(string productName) 
        {
            if (_nameBindings.ContainsKey(productName))
            {
                return _nameBindings[productName];
            }
            else 
            {
                throw new KeyNotFoundException();
            }
        }

        private void ApplyBindings() 
        {
            foreach (var productNamePair in _productsLocale) 
            {
                _nameBindings.Add(productNamePair.Value, productNamePair.Key);
            }
        }
    }

    public enum Products
    {
        PotionOfHealth,
        PotionOfMana,
        PotionOfStamina,
        PotionOfAnger,
        PotionOfLuck
    }
}
