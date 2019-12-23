using Microsoft.AspNetCore.SignalR;
using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Messages;
using System.Threading.Tasks;

namespace SimulatedExchange.Api.Hubs
{
    public class TradeReportHubProxy :
        IMessageHandler<NewOrderMessage>,
        IMessageHandler<CancelOrderMessage>,
        IMessageHandler<DealOrderMessage>
    {
        private readonly IHubContext<TradeReportHub> hub;

        public TradeReportHubProxy(IHubContext<TradeReportHub> hub)
        {
            this.hub = hub;
        }

        public async Task Handle(NewOrderMessage message)
        {
            await SendMessage("New", message.State);
        }

        public async Task Handle(CancelOrderMessage message)
        {
            await SendMessage("Cancel", message.State);
        }

        public async Task Handle(DealOrderMessage message)
        {
            await SendMessage("Deal", message.State);
        }

        private async Task SendMessage(string eventName, OrderState state)
        {
            await hub.Clients.All.SendAsync(eventName, state);
        }
    }
}
