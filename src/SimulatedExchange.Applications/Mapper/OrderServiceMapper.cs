using SimulatedExchange.Applications.DTO;
using SimulatedExchange.Queries.Orders;

namespace SimulatedExchange.Applications.Mapper
{
    public class OrderServiceMapper : IOrderServiceMapper
    {
        public OrderDetial Map(IOrderDetial detial)
        {
            var result = new OrderDetial
            {
                Volume = detial.Volume,
                Exchange = detial.Exchange,
                TotalAmount = detial.TotalAmount,
                Id = detial.Id,
                PairSymbols = detial.PairSymbols,
                Price = detial.Price,
                Status = detial.Status,
                Type = detial.Type
            };

            return result;
        }

        public OrderList Map(IOrderList list)
        {
            var result = new OrderList();
            foreach (var item in list)
            {
                result.Add(Map(item));
            }
            result.PagingInfo = list.Page;

            return result;
        }

        public OrderListItem Map(IOrderListItem listItem)
        {
            var result = new OrderListItem
            {
                Id = listItem.Id,
                TotalAmount = listItem.TotalAmount,
                Exchange = listItem.Exchange,
                PairSymbols = listItem.PairSymbols,
                Price = listItem.Price,
                Status = listItem.Status,
                Type = listItem.Type
            };

            return result;
        }
    }
}
