using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories
{
    public class FakeDiscountRepository : IDiscountRepository
    {
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).

        public Discount? Get(string code)
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
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