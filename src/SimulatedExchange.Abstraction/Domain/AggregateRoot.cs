using SimulatedExchange.Events;
using SimulatedExchange.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SimulatedExchange.Domain
{
    public abstract class AggregateRoot :
        IEventProvider, IOriginator
    {
        private readonly ConcurrentQueue<Event> uncommitEvents;

        public AggregateRoot()
        {
            uncommitEvents = new ConcurrentQueue<Event>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; private set; }

        protected void ApplyEvent(Event @event)
        {
            ApplyEvent(@event, true);
        }

        private void ApplyEvent(Event @event, bool isNew)
        {
            dynamic aggregateRoot = this;
            aggregateRoot.Handle(Converter.ChangeType(@event, @event.GetType()));

            if (isNew)
            {
                uncommitEvents.Enqueue(@event);
            }
        }

        public void RestoreEvents(IEnumerable<Event> history)
        {
            foreach (var @event in history)
            {
                ApplyEvent(@event, false);
            }
            var last = history.Last();
            Version = last.Version;
            EventVersion = Version;
            Id = last.AggregateId;
        }

        public void MarkEventCommited()
        {
            while (uncommitEvents.TryDequeue(out _)) { }
        }

        public abstract BaseMemento GetMemento();
        public abstract void SetMemento(BaseMemento memento);

        public IEnumerable<Event> UncommittedEvent => uncommitEvents;
    }
}
