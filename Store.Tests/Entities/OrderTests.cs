using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities
{
    [TestClass]
    [TestCategory("Domain/Entities")]
    public class OrderTests
    {
        private readonly Customer _customer;

        private readonly Product _product;

        private readonly Discount _discount;

        private const decimal DELIVERY_FEE = 10m;

        private const decimal PRODUCT_PRICE = 10;

        private const bool PRODUCT_IS_ACTIVE = true;

        private const decimal PRODUCT_NO_DISCOUNT_AMOUNT = 0;

        public OrderTests()
        {
            _customer = new Customer("Dharuan", "dharuan@email.com");
            _product = new Product("Product", PRODUCT_PRICE, PRODUCT_IS_ACTIVE);
            _discount = new Discount(PRODUCT_NO_DISCOUNT_AMOUNT, DateTime.UtcNow);
        }

        [TestMethod]
        public void New_order_request_should_generate_a_number_with_eight_digits()
        {
            const int numberLengthOrder = 8;

            var order = new Order(_customer, DELIVERY_FEE, _discount);
            Assert.AreEqual(numberLengthOrder, order.Number.Length);
        }

        [TestMethod]
        public void New_order_request_it_status_should_be_waiting_payment()
        {
            var order = new Order(_customer, DELIVERY_FEE, _discount);
            Assert.AreEqual(EOrderStatus.WaitingPayment, order.Status);
        }

        [TestMethod]
        public void When_payment_was_made_order_status_should_be_waiting_delivery()
        {
            var order = new Order(_customer, DELIVERY_FEE, _discount);
            order.AddItem(_product, 1);
            order.Pay(20);
            Assert.AreEqual(EOrderStatus.WaitingDelivery, order.Status);
        }

        [TestMethod]
        public void When_order_was_canceled_order_status_should_be_canceled()
        {
            var order = new Order(_customer, DELIVERY_FEE, _discount);
            order.Cancel();
            Assert.AreEqual(EOrderStatus.Canceled, order.Status);
        }

        [TestMethod]
        public void New_item_without_a_product_it_should_not_be_added_in_order()
        {
            var order = new Order(_customer, DELIVERY_FEE, _discount);
            order.AddItem(null!, 1);
            Assert.AreEqual(0, order.Items.Count);
        }

        [TestMethod]
        public void New_item_with_quantity_lesser_or_equal_zero_it_should_not_be_added_in_order()
        {
            const int TOTAL_PRODUCT_ADDED = 0;

            var order = new Order(_customer, DELIVERY_FEE, _discount);
            order.AddItem(_product, -1);
            Assert.AreEqual(TOTAL_PRODUCT_ADDED, order.Items.Count);
        }

        [TestMethod]
        public void New_valid_order_had_fifty_in_total_price()
        {
            var order = new Order(_customer, DELIVERY_FEE, _discount);
            order.AddItem(_product, 4);
            Assert.AreEqual(50, order.Total());
        }

        [TestMethod]
        public void Expired_discount_had_sixty_in_total_price()
        {
            var expiredDiscountOneDayBeforeToday = new Discount(10, DateTime.UtcNow.AddDays(-1));

            var order = new Order(_customer, DELIVERY_FEE, expiredDiscountOneDayBeforeToday);
            order.AddItem(_product, 5);

            Assert.AreEqual(60, order.Total());
        }

        [TestMethod]
        public void Invalid_discount_should_had_sixty_in_total_price()
        {
            var order = new Order(_customer, DELIVERY_FEE, null!);
            order.AddItem(_product, 5);

            Assert.AreEqual(60, order.Total());
        }

        [TestMethod]
        public void Valid_discount_with_10_value_the_total_order_price_should_be_fifty()
        {
            var validDiscountFiveDaysAfterTodayWithTenValue = new Discount(10, DateTime.UtcNow.AddDays(5));

            var order = new Order(_customer, DELIVERY_FEE, validDiscountFiveDaysAfterTodayWithTenValue);
            order.AddItem(_product, 5);

            Assert.AreEqual(50, order.Total());
        }

        [TestMethod]
        public void Delivery_fee_with_10_value_the_order_total_price_should_be_sixty()
        {
            var order = new Order(_customer, DELIVERY_FEE, _discount);
            order.AddItem(_product, 5);

            Assert.AreEqual(60, order.Total());
        }

        [TestMethod]
        public void Order_without_a_client_should_be_invalid()
        {
            var order = new Order(null!, DELIVERY_FEE, _discount);

            Assert.IsFalse(order.IsValid);
        }
    }
}