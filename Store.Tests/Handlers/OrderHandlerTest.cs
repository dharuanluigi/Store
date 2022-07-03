using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories.Interfaces;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers
{
    [TestClass]
    [TestCategory("Domain/Handlers")]
    public class OrderHandlerTest
    {
        private readonly ICustomerRepository _customerRespository;

        private readonly IDeliveryFeeRepository _deliveryFeeRepository;

        private readonly IDiscountRepository _discountRepository;

        private readonly IProductRepository _productRespository;

        private readonly IOrderRepository _orderRespository;

        public OrderHandlerTest()
        {
            _customerRespository = new FakeCustomerRepository();
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
            _discountRepository = new FakeDiscountRepository();
            _productRespository = new FakeProductRepository();
            _orderRespository = new FakeOrderRepository();
        }

        [TestMethod]
        public void Valid_command_should_create_an_success_order()
        {
            var orderHandler = new OrderHandler(_customerRespository, _deliveryFeeRepository, _discountRepository, _productRespository, _orderRespository);
            var createOrderCommand = new CreateOrderCommand("12345678910", "12345678", "12345");
            createOrderCommand.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            orderHandler.Handle(createOrderCommand);

            Assert.IsTrue(orderHandler.IsValid);
        }

        [TestMethod]
        public void Invalid_command_should_not_create_an_success_order()
        {
            var orderHandler = new OrderHandler(_customerRespository, _deliveryFeeRepository, _discountRepository, _productRespository, _orderRespository);
            orderHandler.Handle(null!);
            Assert.IsFalse(orderHandler.IsValid);
        }

        [TestMethod]
        public void An_order_without_items_no_orders_should_made()
        {
            var orderHandler = new OrderHandler(_customerRespository, _deliveryFeeRepository, _discountRepository, _productRespository, _orderRespository);
            var createOrderCommand = new CreateOrderCommand("12345678910", "12345678", "12345");
            orderHandler.Handle(createOrderCommand);

            Assert.IsFalse(orderHandler.IsValid);
        }

        [TestMethod]
        public void An_invalid_promocode_the_order_should_create_successfully()
        {
            var orderHandler = new OrderHandler(_customerRespository, _deliveryFeeRepository, _discountRepository, _productRespository, _orderRespository);
            var createOrderCommand = new CreateOrderCommand("12345678910", "12345678", null!);
            createOrderCommand.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            orderHandler.Handle(createOrderCommand);

            Assert.IsTrue(orderHandler.IsValid);
        }

        [TestMethod]
        public void Invalid_zipcode_should_not_create_order_successfully()
        {
            var orderHandler = new OrderHandler(_customerRespository, _deliveryFeeRepository, _discountRepository, _productRespository, _orderRespository);
            var createOrderCommand = new CreateOrderCommand("12345678910", null!, "12345");
            createOrderCommand.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            orderHandler.Handle(createOrderCommand);

            Assert.IsFalse(orderHandler.IsValid);
        }

        [TestMethod]
        public void Invalid_customer_not_create_an_order()
        {
            var orderHandler = new OrderHandler(_customerRespository, _deliveryFeeRepository, _discountRepository, _productRespository, _orderRespository);
            var createOrderCommand = new CreateOrderCommand(null!, "12345678", "12345");
            createOrderCommand.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            orderHandler.Handle(createOrderCommand);

            Assert.IsFalse(orderHandler.IsValid);
        }
    }
}