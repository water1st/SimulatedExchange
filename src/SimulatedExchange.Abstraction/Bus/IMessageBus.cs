using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedExchange.Bus
{
    public interface IMessageBus
    {
        Task SendAsync<TMessage>(TMessage message)
            where TMessage : class;
    }
}
