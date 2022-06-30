using Store.Domain.Entities;
using System.Linq.Expressions;

namespace Store.Domain.Queries
{
    public static class ProductQueries
    {
        public static Expression<Func<Product, bool>> GetActiveProducts()
        {
            return p => p.IsActive;
        }

        public static Expression<Func<Product, bool>> GetInactiveProducts()
        {
            return p => !p.IsActive;
        }
    }
}