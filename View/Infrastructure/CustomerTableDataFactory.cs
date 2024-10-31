using Store.Model;
using Store.Presenter;
using Store.View.TableView;
using System.Drawing;

namespace Store.View.Infrastructure
{
    public class CustomerTableDataFactory
    {
        public Table CreateCustomerData(Point localPosition, int height, CustomerPresenter presenter)
        {
            var customerInventoryData = new Table(localPosition, height, ExtractInventoryData(presenter.Inventory));
            customerInventoryData.SetHeader(["Ваш инвентарь", "количество"]);
            customerInventoryData.SetColumnAlignment(0, TextAlignmentOptions.Left);
            customerInventoryData.SetColumnAlignment(1, TextAlignmentOptions.Center);

            return customerInventoryData;
        }

        private string[,] ExtractInventoryData(IReadOnlyDictionary<Product, int> inventory)
        {
            var extractedData = new List<List<string>>();

            foreach (var productData in inventory)
            {
                var row = new List<string>()
                {
                    productData.Key.Name,
                    productData.Value.ToString()
                };

                extractedData.Add(row);
            }

            if (extractedData.Count == 0)
            {
                extractedData.Add(new List<string>() { string.Empty, string.Empty });
            }

            return Utils.ConvertToStringArray(extractedData);
        }
    }
}
