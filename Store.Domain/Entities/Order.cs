using Flunt.Validations;
using Store.Domain.Enums;

namespace Store.Domain.Entities
{
    public class Order : Entity
    {
        private const byte NUMBER_LENGTH = 8;

        private const decimal NO_PRICE = 0;

        private readonly IList<OrderItem> _items;

        public string Number { get; }

        public Customer Customer { get; }

        public DateTime Date { get; }

        public EOrderStatus Status { get; private set; }

        public decimal DeliveryFee { get; }

        public Discount Discount { get; }

#pragma warning disable S2365 // Properties should not make collection or array copies
        public IReadOnlyCollection<OrderItem> Items => _items.ToList();
#pragma warning restore S2365 // Properties should not make collection or array copies

        public Order(Customer customer, decimal deliveryFee, Discount discount)
        {
            AddNotifications(
                new Contract<Customer>()
                    .Requires()
                    .IsNotNull(customer, "Order.Customer", "Customer must not be null")
                    .IsNotNull(discount, "Order.Discount", "Discount must not be null")
            );

            Number = Guid.NewGuid().ToString()[..NUMBER_LENGTH];
            Customer = customer;
            Date = DateTime.UtcNow;
            Status = EOrderStatus.WaitingPayment;
            DeliveryFee = deliveryFee;
            Discount = discount;
            _items = new List<OrderItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            var item = new OrderItem(product, quantity);
            if (item.IsValid)
            {
                _items.Add(item);
            }
        }

        public decimal Total()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Total();
            }

            total += DeliveryFee;
            total -= (Discount?.Value()) ?? NO_PRICE;

            return total;
        }

        public void Pay(decimal amount)
        {
            if (amount == Total())
            {
                Status = EOrderStatus.WaitingDelivery;
            }
        }

        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
        }
    }
}