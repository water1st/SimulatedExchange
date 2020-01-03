using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using SimulatedExchange.ClientAdapter.Messages.Orders;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SimulatedExchange.Api.Hubs
{
    public class TradeReportHub : Hub
    {
        private readonly IMemoryCache cache;

        public TradeReportHub(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public async override Task OnConnectedAsync()
        {
            SetClientConnected();
            await SendUnsendMessaged();
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            SetClientDisconnected();
            return base.OnDisconnectedAsync(exception);
        }

        private void SetClientDisconnected() => cache.Set(Constants.TradeServiceConnectedKey, false);

        private void SetClientConnected() => cache.Set(Constants.TradeServiceConnectedKey, true);

        private async Task SendUnsendMessaged()
        {
            if (cache.TryGetValue(Constants.TeadeReportingUnsendMessageCacheKey, out ConcurrentQueue<OrderReportingMessage> messages))
            {
                while (messages.TryDequeue(out var message))
                {
                    await Clients.All.SendAsync(message.Event.ToString(), message.State);
                }
            }
        }
    }
}
