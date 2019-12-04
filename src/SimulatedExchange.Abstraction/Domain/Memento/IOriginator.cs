namespace SimulatedExchange.Domain
{
    public interface IOriginator
    {
        BaseMemento GetMemento();
        void SetMemento(BaseMemento memento);
    }
}
