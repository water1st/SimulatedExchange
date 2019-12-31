using System.Data;

namespace SimulatedExchange.DataAccess
{
    internal interface IDbConnectionFactory
    {
        IDbConnection Create(ConnectionType type);
    }
}
