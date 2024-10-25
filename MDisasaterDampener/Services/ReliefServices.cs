using MDisasaterDampener.Models;
using MDisasaterDampener.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace MDisasaterDampener.Services
{
    public class ReliefServices : IReliefServices
    {
#pragma warning disable CA1305 // Specify IFormatProvider
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
        public void Insert(ReliefEffortViewModel reliefEffort)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string query = "INSERT INTO RELIEF_EFFORT (Title, Description) " +
                           "VALUES (@Title, @Description)";
            SqlCommand command = new(query, connection);
            _ = command.Parameters.AddWithValue("@Title", reliefEffort.Title);
            _ = command.Parameters.AddWithValue("@Description", reliefEffort.Description);

            _ = command.ExecuteNonQuery();
        }

        public List<ReliefEffortViewModel> Read()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            List<ReliefEffortViewModel> reliefEfforts = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM RELIEF_EFFORT";

                SqlCommand command = new(selectQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ReliefEffortViewModel reliefEffort = new()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),

                    };
                    reliefEfforts.Add(reliefEffort);
                }

                reader.Close();
            }
            return reliefEfforts;
        }
        public ReliefEffortViewModel View(int id)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            ReliefEffortViewModel reliefEffort = new();
            // Open a connection to the database
            using SqlConnection connection = new(connectionString);
            connection.Open();
            string query = "SELECT * FROM RELIEF_EFFORT WHERE Id=@Id ";
            SqlCommand command = new(query, connection);
            _ = command.Parameters.AddWithValue("@id", id);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader != null)
                {
                    reliefEffort.Id = int.Parse(reader["id"].ToString());

                    reliefEffort.Title = reader["Title"].ToString();
                    reliefEffort.Description = reader["Description"].ToString();


                }
            }
            return reliefEffort;
        }
    }
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CA1305 // Specify IFormatProvider
}
