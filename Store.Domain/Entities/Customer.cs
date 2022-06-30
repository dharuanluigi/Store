namespace Store.Domain.Entities
{
    public class Customer : Entity
    {
        public string Name { get; }

        public string Email { get; }

        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}