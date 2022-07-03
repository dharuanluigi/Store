using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories
{
    public class FakeDiscountRepository : IDiscountRepository
    {
        public Discount? Get(string code)
        {
            if (code == "12345")
            {
                return new Discount(10, DateTime.UtcNow.AddDays(5));
            }

            if (code == "11111")
            {
                return new Discount(10, DateTime.UtcNow.AddDays(-5));
            }

            return default(Discount);
        }
    }
}