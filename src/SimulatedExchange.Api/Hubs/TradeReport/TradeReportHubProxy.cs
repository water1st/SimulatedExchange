using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using SimulatedExchange.ClientAdapter.Abstraction.Handlers;
using SimulatedExchange.ClientAdapter.Messages.Orders;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SimulatedExchange.Api.Hubs
{
    public class TradeReportHubProxy :
        IMessageHandler<OrderReportingMessage>
    {
        private readonly IHubContext<TradeReportHub> hub;
        private readonly IMemoryCache cache;

        public TradeReportHubProxy(IHubContext<TradeReportHub> hub, IMemoryCache cache)
        {
            this.hub = hub;
            this.cache = cache;
        }


        private async Task SendMessage(OrderReportingMessage message)
        {
            if (cache.TryGetValue(Constants.TradeServiceConnectedKey, out bool connectioned))
            {
                if (connectioned)
                {
                    try
                    {
                        await hub.Clients.All.SendAsync(message.Event.ToString(), message.State);
                        return;
                    }
                    catch { }
                }
            }
            SaveUnsendMessage(message);
        }

        private void SaveUnsendMessage(OrderReportingMessage message)
        {
            var queue = cache.GetOrCreate(Constants.TeadeReportingUnsendMessageCacheKey, entry => new ConcurrentQueue<OrderReportingMessage>());
            queue.Enqueue(message);
        }

        public async Task Handle(OrderReportingMessage message)
        {
            await SendMessage(message);
        }
    }
}
