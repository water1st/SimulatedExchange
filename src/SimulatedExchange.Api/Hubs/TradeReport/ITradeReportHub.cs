using SimulatedExchange.Domain.Orders;
using System.Threading.Tasks;

namespace SimulatedExchange.Api.Hubs
{
    public interface ITradeReportHub
    {
        Task ReceiveTradeReport(OrderReportMessage message);
    }
}
