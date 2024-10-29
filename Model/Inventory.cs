using Store.Model.Extensions;

namespace Store.Model
{
    public class Inventory
    {
        private Dictionary<Product, int> _products;

        public Inventory()
        {
            _products = new Dictionary<Product, int>();
        }

        public IEnumerable<KeyValuePair<Product, int>> Products => _products;

        public void AddProduct(Product product)
        {
            if (_products.ContainsKey(product))
            {
                _products[product]++;
            }
            else
            {
                _products.Add(product, 1);
            }
        }

        public void AddRange(List<Product> products) 
        {
            foreach (var product in products) 
            {
                AddProduct(product);
            }
        }

        public void RemoveProduct(Product product)
        {
            if (_products.ContainsKey(product))
            {
                var count = _products[product];

                if (count >= 1)
                {
                    _products[product]--;

                    if (--count == 0)
                    {
                        _products.Remove(product);
                    }
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void RemoveMany(List<Product> products) 
        {
            foreach (var product in products) 
            { 
                RemoveProduct(product);
            }
        }

        public bool Contains(Product product) 
        { 
            return _products.ContainsKey(product);
        }

        public int GetCount(Product product) 
        {
            return _products[product];
        }

        public List<Product> ConvertToList() 
        { 
            var productsList = new List<Product>();

            foreach (var item in _products) 
            {
                productsList.AddMany(item.Key, (uint)item.Value);
            }

            return productsList;
        }
    }
}
