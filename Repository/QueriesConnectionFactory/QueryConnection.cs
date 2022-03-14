using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Repository.QueriesConnectionFactory
{
    public class QueryConnection : IQueryConnection
    {
        private IConfiguration _configs;
        public QueryConnection(IConfiguration configs)
        {
            _configs = configs;
        }
        public IDbConnection OpenConnection()
        {
            IDbConnection dbConnection = new SqlConnection(_configs.GetConnectionString("DefaultConnection"));
            dbConnection.Open();
            return dbConnection;

        }
    }
}
