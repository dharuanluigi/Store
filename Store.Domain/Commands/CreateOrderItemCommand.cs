using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class CreateOrderItemCommand : Notifiable<Notification>, ICommand
    {
        private const int MAX_ID_LENGTH = 32;

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public CreateOrderItemCommand()
        {
        }

        public CreateOrderItemCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract<CreateOrderItemCommand>()
                    .Requires()
                    .IsGreaterThan(Quantity, 0, "CreateOrderItem.Quantity", "Quantity items should be more than 0")
                    .IsTrue(ProductId.ToString().Length == MAX_ID_LENGTH, "CreateOrderItem.ProductId", "The product identification was invalid. Mus be greater than 32 caracters in id")
            );
        }
    }
}