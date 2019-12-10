namespace SimulatedExchange.DataAccess.Storages
{
    public partial class MySQLMementoStorage
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

