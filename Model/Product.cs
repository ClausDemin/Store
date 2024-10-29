using Store.Model.Infrastructure;

namespace Store.Model
{
    public class Product
    {
        public Product(Products productType, string name, float cost)
        {
            ProductType = productType;
            Name = name;
            Cost = cost;
        }

        public Products ProductType { get; private set; }
        public string Name { get; private set; }
        public float Cost { get; private set; }

        public override bool Equals(object? obj)
        {
            return ProductType == ((Product)obj).ProductType;
        }

        public override int GetHashCode()
        {
            return (int)ProductType;
        }
    }
}
