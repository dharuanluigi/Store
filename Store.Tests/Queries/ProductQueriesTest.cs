using Store.Domain.Entities;
using Store.Domain.Queries;
using Store.Tests.Repositories;

namespace Store.Tests.Queries
{
    [TestClass]
    [TestCategory("Domain/Queries")]
    public class ProductQueriesTest
    {
        private readonly IEnumerable<Product> _products;

        public ProductQueriesTest()
        {
            _products = GetProductsMock();
        }

        [TestMethod]
        public void Query_active_products_should_returns_three()
        {
            var activedProducts = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
            Assert.AreEqual(3, activedProducts.Count());
        }

        [TestMethod]
        public void Query_inactive_products_should_returns_two()
        {
            var inactiveProducts = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
        }

        private IEnumerable<Product> GetProductsMock()
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