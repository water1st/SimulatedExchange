using SimulatedExchange.Events;
using System.Threading.Tasks;

namespace SimulatedExchange.Bus
{
    public interface IEventBus
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event;
    }
}
