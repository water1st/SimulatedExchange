using SimulatedExchange.Applications.DTO;
using SimulatedExchange.Queries.Orders;

namespace SimulatedExchange.Applications.Mapper
{
    public class OrderServiceMapper : IOrderServiceMapper
    {
        public OrderDetial Map(GetOrderQueryResult detial)
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

        public OrderList Map(GetOrdersQueryResult list)
        {
            var result = new OrderList();
            foreach (var item in list)
            {
                result.Add(Map(item));
            }
            result.PagingInfo = Map(list.PagingInfo);

            return result;
        }


        public OrderListItem Map(GetOrdersQueryResultItem listItem)
        {
            var result = new OrderListItem
            {
                Id = listItem.Id,
                TotalAmount = listItem.TotalAmount,
                Exchange = listItem.Exchange,
                PairSymbols = listItem.PairSymbols,
                Price = listItem.Price,
                Status = listItem.Status,
                Type = listItem.Type,
                Volume = listItem.Volume
            };

            return result;
        }

        public CurrentPagingInfo Map(Queries.PagingInfo pagingInfo)
        {
            var reuslt = new CurrentPagingInfo
            {
                CurrentPageIndex = pagingInfo.CurrentPageIndex,
                PageCount = pagingInfo.PageCount
            };

            return reuslt;
        }
    }
}
