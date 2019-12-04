using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace SimulatedExchange.DataAccess.Databases
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly IConfiguration configuration;

        public DatabaseConnectionFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection Create(string name)
        {
            var connectionString = configuration.GetConnectionString(name);
            switch (name)
            {
                case DatabaseConnectionNames.MYSQL_CONNECTION:
                    return new MySqlConnection(connectionString);
                default:
                    throw new Exception($"不支持name:\"{name}\"的数据库链接");
            }
        }
    }
}
