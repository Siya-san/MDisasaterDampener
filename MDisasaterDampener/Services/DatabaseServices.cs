using Microsoft.Data.SqlClient;
using System.Data;
using MDisasaterDampener.Services.Interfaces;
using Dapper;



namespace MDisasaterDampener.Services
{
    public class DatabaseServices(IConfiguration configuration) : IDatabaseServices
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        private readonly string _connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")
                                ?? Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
#pragma warning restore CS8601 // Possible null reference assignment.

        // Method to create and return a database connection
        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Executes a query that doesn't return results (e.g., INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, object parameters)
        {
            using IDbConnection connection = GetConnection();
            connection.Open();
            return connection.Execute(query, parameters);
        }

        // Executes a query that returns a single scalar value (e.g., COUNT, SUM)
        public object ExecuteScalar(string query, object parameters)
        {
            using IDbConnection connection = GetConnection();
            connection.Open();
#pragma warning disable CS8603 // Possible null reference return.
            return connection.ExecuteScalar(query, parameters);
#pragma warning restore CS8603 // Possible null reference return.
        }

        // Executes a query that returns a result set (e.g., SELECT)
        public IDataReader ExecuteReader(string query, object parameters)
        {
            IDbConnection connection = GetConnection();
            connection.Open();
            return connection.ExecuteReader(query, parameters);
        }
    }
}

