namespace Store.Domain.Entities
{
    public class Product
    {
        public string Title { get; }

        public decimal Price { get; }

        public bool IsActive { get; }

        public Product(string title, decimal price, bool isActive)
        {
            Title = title;
            Price = price;
            IsActive = isActive;
        }
    }
}