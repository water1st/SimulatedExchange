using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using SimulatedExchange.ClientAdapter.Abstraction.Handlers;
using SimulatedExchange.ClientAdapter.Messages;
using System.Collections.Concurrent;
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
        private readonly IMemoryCache cache;

        public TradeReportHubProxy(IHubContext<TradeReportHub> hub, IMemoryCache cache)
        {
            this.hub = hub;
            this.cache = cache;
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
            if (cache.TryGetValue(Constants.TradeServiceConnectedKey, out bool connectioned))
            {
                if (connectioned)
                {
                    try
                    {
                        await hub.Clients.All.SendAsync(eventName, state);
                        return;
                    }
                    catch { }
                }
            }
            SaveUnsendMessage(new TradeMessage { Method = eventName, Parameter = state });
        }

        private void SaveUnsendMessage(TradeMessage message)
        {
            var queue = cache.GetOrCreate(Constants.TeadeReportingUnsendMessageCacheKey, entry => new ConcurrentQueue<TradeMessage>());
            queue.Enqueue(message);
        }

    }
}
