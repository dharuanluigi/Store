namespace Store.Domain.Entities
{
    public class Discount : Entity
    {
        private readonly byte DATETIME_IS_VALID = 0;
        private readonly byte NO_VALUE_TO_RETURNS = 0;

        public decimal Amount { get; }

        public DateTime ExpireDate { get; }

        public Discount(decimal amount, DateTime expireDate)
        {
            Amount = amount;
            ExpireDate = expireDate;
        }

        public bool IsValid()
        {
            return DateTime.Compare(DateTime.UtcNow, ExpireDate) < DATETIME_IS_VALID;
        }

        public decimal Value()
        {
            if (IsValid())
            {
                return Amount;
            }
            else
            {
                return NO_VALUE_TO_RETURNS;
            }
        }
    }
}