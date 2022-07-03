using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories.Interfaces;
using Store.Domain.Utils;

namespace Store.Domain.Handlers
{
    public class OrderHandler : Notifiable<Notification>, IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customerRespository;

        private readonly IDeliveryFeeRepository _deliveryFeeRepository;

        private readonly IDiscountRepository _discountRepository;

        private readonly IProductRepository _productRespository;

        private readonly IOrderRepository _orderRespository;

        public OrderHandler(ICustomerRepository customerRespository, IDeliveryFeeRepository deliveryFeeRepository, IDiscountRepository discountRepository, IProductRepository productRespository, IOrderRepository orderRespository)
        {
            _customerRespository = customerRespository;
            _deliveryFeeRepository = deliveryFeeRepository;
            _discountRepository = discountRepository;
            _productRespository = productRespository;
            _orderRespository = orderRespository;
        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            Validate(command);

            if (!IsValid)
            {
                return new GenericCommandResult(false, "The parameter command is not valid", Notifications);
            }

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Invalid request to create and order", command.Notifications);
            }

            var customer = _customerRespository.Get(command.Customer);

            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);

            var discount = _discountRepository.Get(command.PromoCode);

            var products = _productRespository.Get(ExtractGuids.Extract(command.Items)).ToList();

            var order = new Order(customer!, deliveryFee, discount);

            foreach (var item in command.Items)
            {
                var product = products.Find(p => p.Id == item.ProductId);
                order.AddItem(product!, item.Quantity);
            }

            AddNotifications(order.Notifications);

            if (!IsValid)
            {
                return new GenericCommandResult(false, "Occurred one or more erros to create an order", Notifications);
            }

            _orderRespository.Save(order);
            return new GenericCommandResult(true, $"Order number {order.Number} was made successfully", order);
        }

        public void Validate(CreateOrderCommand command)
        {
            AddNotifications(
                new Contract<OrderHandler>()
                    .Requires()
                    .IsNotNull(command, "OrderHandler.Handle", "The parameter command is not be null")
                    .IsTrue(ValidateCommand(command), "OrderHandler.command.validate", "Command must be valid for order handler should valid too")
            );
        }

        private static bool ValidateCommand(CreateOrderCommand command)
        {
            if (command is null)
            {
                return default;
            }

            command.Validate();

            return command.IsValid;
        }
    }
}