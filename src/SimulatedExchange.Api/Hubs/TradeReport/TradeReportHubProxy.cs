using Microsoft.AspNetCore.SignalR;
using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Messages;
using System.Threading.Tasks;

namespace SimulatedExchange.Api.Hubs
{
    public class TradeReportHubProxy : IMessageHandler<OrderReportMessage>
    {
        private readonly IHubContext<TradeReportHub, ITradeReportHub> hub;

        public TradeReportHubProxy(IHubContext<TradeReportHub, ITradeReportHub> hub)
        {
            this.hub = hub;
        }

        public async Task Handle(OrderReportMessage message)
        {
            await hub.Clients.All.ReceiveTradeReport(message);
        }
    }
}
