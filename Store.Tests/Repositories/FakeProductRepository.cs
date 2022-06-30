using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            var products = new List<Product>();

            for (var i = 1; i <= 5; i++)
            {
                products.Add(new Product($"Product {i}", 10, i % 2 != 0));
            }

            return products;
        }
    }
}