using Store.Presenter;
using Store.View.Interfaces;
using Store.View.ModelView;
using System.Drawing;

namespace Store.View
{
    public class MainWindow : IUIElement
    {
        private CursorView _cursorView;
        private VendorView _vendorView;
        private CustomerView _customerView;

        private ReactiveLabel _vendorBalance;
        private ReactiveLabel _customerBalacne;
        private ReactiveLabel _productCartInfo;
        private Label _paymentStatus;

        private VendorPresenter _vendor;
        private CustomerPresenter _customer;

        public MainWindow(VendorPresenter vendor, CustomerPresenter customer, Point position)
        {
            var viewHeight = Math.Max(vendor.GetUniqueProductsCount(), customer.GetProductsCount());
            LocalPosition = position;
            Height = viewHeight;

            _vendor = vendor;
            _customer = customer;

            _vendorBalance = new ReactiveLabel(position, _vendor.GetVendorBalanceData);           
            _customerBalacne = new ReactiveLabel(position,  _customer.GetCustomerBalanceData);
            _productCartInfo = new ReactiveLabel(position, _vendor.GerPreliminaryData);
            _paymentStatus = new Label(position, string.Empty);

            _cursorView = new CursorView(GetPositionBelow(_vendorBalance), viewHeight, vendor);
            _vendorView = new VendorView(GetPositionAfter(_cursorView), viewHeight, vendor);
            _customerView = new CustomerView(GetPositionAfter(_vendorView), viewHeight, customer);

            _customerBalacne.LocalPosition = GetPositionAbove(_customerView, _customerBalacne);
            _productCartInfo.LocalPosition = GetPositionBelow(_vendorView);
            _paymentStatus.LocalPosition = GetPositionBelow(_productCartInfo);
        }

        public Point LocalPosition { get; }

        public int Width => _cursorView.Width + _vendorView.Width + _customerView.Width;
        public int Height { get; }

        public void Show()
        {
            _cursorView.Show();
            _vendorView.Show();
            _customerView.Show();

            _vendorBalance.Show();
            _customerBalacne.Show();
            _productCartInfo.Show();
            _paymentStatus.Show();
        }

        public void Run()
        {
            Console.CursorVisible = false;

            Show();

            bool isExitRequest = false;

            while (isExitRequest == false)
            {
                isExitRequest = HandleUserInput(isExitRequest);
            }

            Console.Clear();
        }

        private void Update()
        {
            Console.Clear();

            _vendorView.Update();
            _customerView.Update();
            _vendorView.SetPosition(GetPositionAfter(_cursorView));
            _customerView.SetPosition(GetPositionAfter(_vendorView));

            Show();
        }

        private bool HandleUserInput(bool isExitRequest)
        {
            var userControl = Console.ReadKey(true).Key;

            switch (userControl)
            {
                case ConsoleKey.UpArrow:
                    MoveCursor(CursorMovement.Up);
                    break;

                case ConsoleKey.DownArrow:
                    MoveCursor(CursorMovement.Down);
                    break;

                case ConsoleKey.LeftArrow:
                    HandleRemoveFromCartRequest();
                    break;

                case ConsoleKey.RightArrow:
                    HandleAddToCartRequest();
                    break;

                case ConsoleKey.Enter:
                    HandleApplyRequest();
                    break;

                case ConsoleKey.Escape:
                    isExitRequest = true;
                    break;
            }

            return isExitRequest;
        }

        private void HandleAddToCartRequest()
        {
            string productName = _vendorView.GetValue(_cursorView.CursorPositionTop, 0);
            
            if (!string.IsNullOrEmpty(productName.Trim()))
            {
                if (_vendor.TryAddProductInCart(productName))
                {
                    _vendorView.Update();
                    _vendorView.Redraw();
                    _productCartInfo.Show();
                }
            }
        }

        private void HandleRemoveFromCartRequest()
        {
            string productName = _vendorView.GetValue(_cursorView.CursorPositionTop, 0);

            if (!string.IsNullOrEmpty(productName.Trim())) 
            {
                if (_vendor.TryRemoveProductFromCart(productName))
                {
                    _vendorView.Update();
                    _vendorView.Redraw();
                    _productCartInfo.Show();
                }
            }
        }

        private void HandleApplyRequest()
        {
            _paymentStatus.SetText(_vendor.GetPayment(_customer.Customer));
            Update();
        }

        private Point GetPositionAfter(IUIElement element)
        {
            var position = element.LocalPosition;

            return new Point(position.X + element.Width, position.Y);
        }

        private Point GetPositionBelow(IUIElement element, int offset = 1) 
        { 
            return new Point(element.LocalPosition.X, element.LocalPosition.Y + element.Height + offset);
        }

        private Point GetPositionAbove(IUIElement element, IUIElement other, int offset = 1) 
        {
            return new Point(element.LocalPosition.X, element.LocalPosition.Y - other.Height - offset);
        }

        private void MoveCursor(CursorMovement direction)
        {
            _cursorView.MoveCursor(direction);
        }
    }
}
