using Microsoft.AspNetCore.SignalR;
using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Messages;
using System.Threading.Tasks;

namespace SimulatedExchange.Api.Hubs
{
    public class TradeReportHub : Hub, IMessageHandler<OrderReportMessage>
    {
        public async Task Handle(OrderReportMessage message)
        {
            await Clients.All.SendAsync("TRADE_REPORT_CHANNEL", message);
        }
    }
}
