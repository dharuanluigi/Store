using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class CreateOrderCommand : Notifiable<Notification>, ICommand
    {
        private const int CUSTOMER_DOCUMENT_LENGTH = 11;

        private const int ZIP_CODE_LENGTH = 8;

        public string Customer { get; set; }

        public string ZipCode { get; set; }

        public string PromoCode { get; set; }

        public IList<CreateOrderItemCommand> Items { get; set; }

        public CreateOrderCommand(string customer, string zipCode, string promoCode)
        {
            Customer = customer;
            ZipCode = zipCode;
            PromoCode = promoCode;
            Items = new List<CreateOrderItemCommand>();
        }

        public void Validate()
        {
            AddNotifications(
                new Contract<CreateOrderCommand>()
                    .Requires()
                    .IsTrue(Customer.Length == CUSTOMER_DOCUMENT_LENGTH, "CreateOrderCommand.Customer", "Invalid informed customer, document should have 11 caracters")
                    .IsTrue(ZipCode.Length == ZIP_CODE_LENGTH, "CreateOrderComand.ZipCode", "Invalid informed zip code, the zip code should contains 8 caracters")
            );
        }
    }
}