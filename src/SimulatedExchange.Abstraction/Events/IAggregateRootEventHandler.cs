namespace SimulatedExchange.Events
{
    public interface IAggregateRootEventHandler<TEvent>
        where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}
