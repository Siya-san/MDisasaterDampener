using System.Data;

namespace MDisasaterDampener.Services.Interfaces
{
    public interface IDatabaseServices
    {
        // Opens a connection to the database and returns the connection object.
        IDbConnection GetConnection();

        // Executes a query that does not return a result set (e.g., INSERT, UPDATE, DELETE).
        int ExecuteNonQuery(string query, object parameters);

        // Executes a query that returns a single value (e.g., COUNT, SUM).
        object ExecuteScalar(string query, object parameters);

        // Executes a query that returns a result set (e.g., SELECT).
        IDataReader ExecuteReader(string query, object parameters);

    }
}
