using Store.Domain.Commands;

namespace Store.Tests.Commands
{
    [TestClass]
    [TestCategory("Domain/Commands")]
    public class CreateOrderCommandTest
    {
        [TestMethod]
        public void Invalid_command_should_not_create_an_order()
        {
            var command = new CreateOrderCommand(string.Empty, "12345678", string.Empty);
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(false, command.IsValid);
        }
    }
}