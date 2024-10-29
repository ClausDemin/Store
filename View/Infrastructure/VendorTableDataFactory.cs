using Store.Model;
using Store.Presenter;
using Store.View.TableView;
using System.Drawing;

namespace Store.View.Infrastructure
{
    public class VendorTableDataFactory
    {
        public Table CreateVendorTableData(Point localPosition, int heigth, VendorPresenter presenter) 
        {
            var vendorStorageData = new Table(localPosition, heigth, ExtractInventoryData(presenter.Storage));
            vendorStorageData.SetHeader(["Товар", "Стоимость", "В наличии"]);
            vendorStorageData.SetColumnAlignment(1, TextAlignmentOptions.Center);
            vendorStorageData.SetColumnAlignment(2, TextAlignmentOptions.Center);

            return vendorStorageData;
        }

        public Table CreateProductsInCartAmountData(Point localPosition, int heigth, Table vendorStorageData, VendorPresenter presenter) 
        {
            var productsCartPosition = new Point(localPosition.X + vendorStorageData.Width, localPosition.Y);
            var productInCartAmountData = new Table(productsCartPosition, heigth, GetProductsAmountData(presenter, vendorStorageData));
            productInCartAmountData.SetHeader(["В корзине"]);
            productInCartAmountData.SetColumnAlignment(0, TextAlignmentOptions.Center);

            return productInCartAmountData;
        }

        private  string[,] ExtractInventoryData(IEnumerable<KeyValuePair<Product, int>> inventory)
        {
            var extractedData = new List<List<string>>();

            foreach (var productData in inventory)
            {
                var row = new List<string>()
                {
                    productData.Key.Name,
                    productData.Key.Cost.ToString(),
                    productData.Value.ToString()
                };

                extractedData.Add(row);
            }

            if (extractedData.Count == 0)
            {
                extractedData.Add(new List<string>() { string.Empty, string.Empty, string.Empty });
            }

            return Utils.ConvertToStringArray(extractedData);
        }

        private string[,] GetProductsAmountData(VendorPresenter presenter, Table vendorStorageData)
        {
            var rowsCount = vendorStorageData.RowsCount;

            var result = new string[rowsCount, 1];

            for (int i = 0; i < rowsCount; i++)
            {
                string productAmount = string.Empty;

                try
                {
                    productAmount = presenter
                        .GetProductAmountFromCart(vendorStorageData.GetValue(i, 0))
                        .ToString();
                }
                catch
                {
                    productAmount = string.Empty;
                }

                result[i, 0] = productAmount.ToString();
            }

            return result;
        }
    }
}
