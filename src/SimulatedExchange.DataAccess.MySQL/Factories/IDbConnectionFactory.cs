using System.Data;

namespace SimulatedExchange.DataAccess
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create(ConnectionType type);
    }
}
