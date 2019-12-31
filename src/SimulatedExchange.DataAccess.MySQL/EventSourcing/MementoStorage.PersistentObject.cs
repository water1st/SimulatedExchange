namespace SimulatedExchange.DataAccess.EventSourcing
{
    internal partial class MementoStorage
    {
        private class PersistentObject
        {
            public string Id { get; set; }
            public string AggregateId { get; set; }
            public string Memento { get; set; }
            public string MementoType { get; set; }
            public int Version { get; set; }
        }
    }
}
