using Flunt.Validations;

namespace Store.Domain.Entities
{
    public class Product : Entity
    {
        private const int MINIMUM_PRODUCT_PRICE = 0;

        public string Title { get; }

        public decimal Price { get; }

        public bool IsActive { get; }

        public Product(string title, decimal price, bool isActive)
        {
            AddNotifications(
                new Contract<Product>()
                    .Requires()
                    .IsNotNullOrWhiteSpace(title, "Product.Title", "Product title must not be empty")
                    .IsGreaterThan(price, MINIMUM_PRODUCT_PRICE, "Product.Price", "Product price must be greater than 0")
                    .IsNotNull(isActive, "Product.IsActive", "Product is active must be informed")
            );
            Title = title;
            Price = price;
            IsActive = isActive;
        }
    }
}