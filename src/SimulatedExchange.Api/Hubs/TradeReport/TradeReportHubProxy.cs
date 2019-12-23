using Microsoft.AspNetCore.SignalR;
using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Messages;
using System.Threading.Tasks;

namespace SimulatedExchange.Api.Hubs
{
    public class TradeReportHubProxy :
        IMessageHandler<NewOrderMessage>,
        IMessageHandler<PartialCanceledMessage>,
        IMessageHandler<FullCanceledMessage>,
        IMessageHandler<FullTransactionMessage>,
        IMessageHandler<PartialTransactionMessage>
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

        public async Task Handle(PartialTransactionMessage message)
        {
            await SendMessage("PartialDeal", message.State);
        }

        public async Task Handle(FullTransactionMessage message)
        {
            await SendMessage("FullDeal", message.State);
        }

        public async Task Handle(PartialCanceledMessage message)
        {
            await SendMessage("PartialCancel", message.State);
        }

        public async Task Handle(FullCanceledMessage message)
        {
            await SendMessage("FullCanceled", message.State);
        }

        private async Task SendMessage(string eventName, OrderState state)
        {
            await hub.Clients.All.SendAsync(eventName, state);
        }
    }
}
