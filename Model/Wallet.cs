namespace Store.Model
{
    public class Wallet
    {
        public Wallet(float money = 0)
        {
            Balance = money;
        }

        public float Balance { get; private set; }

        public void IncreaseBalance(float money)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(money);

            Balance += money;
        }

        public void WithdrawMoney(float paymentAmount)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(paymentAmount);

            if (IsPaymentAvailable(paymentAmount))
            {
                Balance -= paymentAmount;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public bool IsPaymentAvailable(float paymentAmount)
        {
            return Balance >= paymentAmount;
        }
    }
}
