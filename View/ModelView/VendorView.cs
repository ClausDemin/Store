using Store.Model;
using Store.Presenter;
using Store.View.AbstractView;
using Store.View.Infrastructure;
using Store.View.TableView;
using System.Drawing;

namespace Store.View.ModelView
{
    public class VendorView: AbstractTableView
    {
        private Table _vendorStorageData;
        private Table _productInCartAmountData;
        private VendorPresenter _presenter;
        private VendorTableDataFactory _dataFactory;

        public VendorView(Point localPosition, int heigth, VendorPresenter presenter) 
        {
            LocalPosition = localPosition;
            _presenter = presenter;

            _dataFactory = new VendorTableDataFactory();
            _vendorStorageData = _dataFactory.CreateVendorTableData(LocalPosition, heigth, _presenter);
            _productInCartAmountData = _dataFactory.CreateProductsInCartAmountData(LocalPosition, heigth, _vendorStorageData, _presenter);
        }

        public override Point LocalPosition { get; protected set; }
        public int FilledRowsCount => _vendorStorageData.FilledRowsCount;
        public override int Width => _vendorStorageData.Width + _productInCartAmountData.Width;
        public override int RowsCount => _vendorStorageData.RowsCount;
        
        public override void Show() 
        { 
            _vendorStorageData.Show();
            _productInCartAmountData.Show();
        }

        public void Update()
        {
            _vendorStorageData = _dataFactory.CreateVendorTableData(LocalPosition, RowsCount, _presenter);
            _productInCartAmountData = _dataFactory.CreateProductsInCartAmountData(LocalPosition, RowsCount, _vendorStorageData, _presenter);
        }

        public string GetValue(int row, int column) 
        { 
            return _vendorStorageData.GetValue(row, column);
        }

        public void SetPosition(Point position) 
        { 
            LocalPosition = position;
            _vendorStorageData.LocalPosition = position;
            _productInCartAmountData.LocalPosition = new Point(position.X + _vendorStorageData.Width, position.Y);
        }
    }
}
