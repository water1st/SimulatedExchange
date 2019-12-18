using SimulatedExchange.Api.DTO;
using SimulatedExchange.Applications.DTO;

namespace SimulatedExchange.Api.Mapper
{
    public class OrderMapper : IOrderMapper
    {
        public GerOrderListResponse Map(OrderList orderLists)
        {
            var result = new GerOrderListResponse();

            foreach (var data in orderLists)
            {
                var item = new DTO.OrderListItem
                {
                    TotalAmount = data.TotalAmount,
                    Id = data.Id,
                    PairSymbols = data.PairSymbols,
                    Price = data.Price,
                    Status = data.Status
                };

                result.Add(item);
            }

            return result;
        }
    }
}
