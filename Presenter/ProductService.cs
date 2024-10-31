using Store.Model;
using Store.Model.Infrastructure;

namespace Store.Presenter
{
    public class ProductService
    {
        private ProductsFactory _productFactory;

        public ProductService() 
        {
            _productFactory = new ProductsFactory();
        }

        public List<Product> CreateProductList(uint minProductsCount, uint maxProductsCount) 
        { 
            return _productFactory.CreateProductList(minProductsCount, maxProductsCount);
        }

        public Products FindProduct(string productName) 
        {
            return _productFactory.GetProductType(productName);
        }

        public Product GetProduct(Products productType) 
        { 
            return _productFactory.CreateProduct(productType);
        }
    }
}
