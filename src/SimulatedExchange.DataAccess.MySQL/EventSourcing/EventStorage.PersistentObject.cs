namespace SimulatedExchange.DataAccess.EventSourcing
{
    internal partial class EventStorage
    {
        private class PersistentObject
        {
            public string Id { get; set; }
            public string AggregateId { get; set; }
            public string Event { get; set; }
            public string EventType { get; set; }
            public int Version { get; set; }
        }
    }
}
