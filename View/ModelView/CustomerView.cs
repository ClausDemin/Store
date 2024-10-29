using Store.Model;
using Store.Presenter;
using Store.View.AbstractView;
using Store.View.Infrastructure;
using Store.View.TableView;
using System.Drawing;

namespace Store.View.ModelView
{
    public class CustomerView : AbstractTableView
    {
        private CustomerPresenter _presenter;
        private Table _customerInventoryData;
        private CustomerTableDataFactory _dataFactory;

        public CustomerView(Point localPosition, int height, CustomerPresenter presenter)
        {
            LocalPosition = localPosition;
            _presenter = presenter;
            _dataFactory = new CustomerTableDataFactory();

            _customerInventoryData = _dataFactory.CreateCustomerData(LocalPosition, height, presenter);
        }

        public override Point LocalPosition { get; protected set; }
        public override int Width => _customerInventoryData.Width;

        public override int RowsCount => _customerInventoryData.RowsCount;

        public override void Show()
        {
            _customerInventoryData.Show();
        }

        public void Update() 
        {
            _customerInventoryData = _dataFactory.CreateCustomerData(LocalPosition, RowsCount, _presenter);
        }

        public void  SetPosition(Point position) 
        { 
            LocalPosition = position;
            _customerInventoryData.LocalPosition = position;
        }
    }
}
