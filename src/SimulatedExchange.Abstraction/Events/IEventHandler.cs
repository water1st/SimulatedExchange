using System.Threading.Tasks;

namespace SimulatedExchange.Events
{
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        Task Handle(TEvent @event);
    }
}
