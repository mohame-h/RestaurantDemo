using System.Data;

namespace Repository.QueriesConnectionFactory
{
    public interface IQueryConnection
    {
        IDbConnection OpenConnection();
    }
}
