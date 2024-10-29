using Store.Model;
using Store.Model.Infrastructure;
using Store.Presenter;
using Store.View;
using Store.View.ModelView;
using System.Drawing;

namespace Store
{
    public static class Program
    {
        public static void Main()
        {
            var productPresenter = new ProductPresenter();
            var customer = new CustomerPresenter(100);
            var vendor = new VendorPresenter(productPresenter, 5, 15);

            var mainWindow = new MainWindow(vendor, customer, new Point(0, 0));

            mainWindow.Run();

            Console.Clear();
        }
    }
}
