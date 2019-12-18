using SimulatedExchange.Api.DTO;
using SimulatedExchange.Applications.DTO;

namespace SimulatedExchange.Api.Mapper
{
    public interface IOrderMapper
    {
        GerOrderListResponse Map(OrderList orderLists);

        
    }
}
