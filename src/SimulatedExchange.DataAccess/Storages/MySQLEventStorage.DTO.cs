namespace SimulatedExchange.DataAccess.Storages
{
    public partial class MySQLEventStorage
    {
        private class DTO
        {
            public string Id { get; set; }
            public string AggregateId { get; set; }
            public string Event { get; set; }
            public string EventType { get; set; }
            public int Version { get; set; }
        }
    }
}
