using System.Data;

namespace SimulatedExchange.DataAccess.Databases
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection Create(string name);
    }
}
