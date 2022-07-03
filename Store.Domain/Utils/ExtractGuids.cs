using Store.Domain.Commands;

namespace Store.Domain.Utils
{
    public static class ExtractGuids
    {
        public static IEnumerable<Guid> Extract(IList<CreateOrderItemCommand> items)
        {
            return (from item in items
                    select item.ProductId).ToList();
        }
    }
}