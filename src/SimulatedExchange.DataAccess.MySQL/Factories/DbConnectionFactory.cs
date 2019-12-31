using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using SimulatedExchange.DataAccess.Options;
using System.Data;

namespace SimulatedExchange.DataAccess
{
    internal class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IOptions<MySQLOptions> options;

        public DbConnectionFactory(IOptions<MySQLOptions> options)
        {
            this.options = options;
        }

        public IDbConnection Create(ConnectionType type)
        {
            string connectionString = string.Empty;
            switch (type)
            {
                case ConnectionType.EventSourcing:
                    connectionString = options.Value.EventSourcingDbConnectionString;
                    break;
                case ConnectionType.Reporting:
                    connectionString = options.Value.ReporttingDbConnectionString;
                    break;
            }

            var result = new MySqlConnection(connectionString);
            return result;
        }
    }
}
