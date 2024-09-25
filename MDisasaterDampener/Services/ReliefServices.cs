using MDisasaterDampener.Models;
using Microsoft.Data.SqlClient;

namespace MDisasaterDampener.Services
{
    public class ReliefServices
    {
        public void Insert(ReliefEffortViewModel reliefEffort)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO RELIEF_EFFORT (Title, Description) " +
                               "VALUES (@Title, @Description)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", reliefEffort.Title);
                command.Parameters.AddWithValue("@Description", reliefEffort.Description);
            
                command.ExecuteNonQuery();
            }
        }

        public List<ReliefEffortViewModel> Read()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            List<ReliefEffortViewModel> reliefEfforts = new List<ReliefEffortViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM RELIEF_EFFORT";

                SqlCommand command = new SqlCommand(selectQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ReliefEffortViewModel reliefEffort = new ReliefEffortViewModel
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Title =reader["Title"].ToString(),
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
            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            ReliefEffortViewModel reliefEffort = new ReliefEffortViewModel();
            // Open a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM RELIEF_EFFORT WHERE Id=@Id ";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
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
        }
    }
}
