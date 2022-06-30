using Flunt.Validations;

namespace Store.Domain.Entities
{
    public class Customer : Entity
    {
        public string Name { get; }

        public string Email { get; }

        public Customer(string name, string email)
        {
            AddNotifications(
                new Contract<Customer>()
                    .Requires()
                    .IsNotNullOrWhiteSpace(name, "Customer.Name", "A customer must be have a name")
                    .IsNotNullOrWhiteSpace(email, "Customer.Email", "A customer must be have an email")
            );

            Name = name;
            Email = email;
        }
    }
}