namespace Store.Domain.Entities
{
    public class OrderItem : Entity
    {
        private const decimal NO_PRICE = 0;

        public Product Product { get; }

        public decimal Price { get; }

        public int Quantity { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public OrderItem(Product product, int quantity)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Product = product;
            Price = (product?.Price) ?? NO_PRICE;
            Quantity = quantity;
        }

        public decimal Total()
        {
            return Price * Quantity;
        }
    }
}