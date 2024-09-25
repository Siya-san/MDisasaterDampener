using MDisasaterDampener.Models;
using Microsoft.Data.SqlClient;

namespace MDisasaterDampener.Services
{
    public class VolunteerServices
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
        public VolunteerRequestViewModel View(int id)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            VolunteerRequestViewModel volunteerRequest = new VolunteerRequestViewModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT VR.Id, VR.NumVolunteers, VR.Date, VR.Description, " +
                               "RE.Id AS ReliefEffortId, RE.Title, RE.Description AS ReliefEffortDescription " +
                               "FROM VOLUNTEER_REQUEST VR " +
                               "JOIN RELIEF_EFFORT RE ON VR.Rid = RE.Id " +
                               "WHERE VR.Id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())  
                    {
                        volunteerRequest.Id = Convert.ToInt32(reader["Id"]);
                        volunteerRequest.NumVolunteers = Convert.ToInt32(reader["NumVolunteers"]);
                        volunteerRequest.Date = DateOnly.Parse(reader["Date"].ToString());
                        volunteerRequest.Description = reader["Description"].ToString();

                        volunteerRequest.Rid = new ReliefEffortViewModel
                        {
                            Id = Convert.ToInt32(reader["ReliefEffortId"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["ReliefEffortDescription"].ToString()
                        };
                    }
                }
            }

            return volunteerRequest;
        }

        public List<VolunteerRequestViewModel> Read()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            List<VolunteerRequestViewModel> volunteerRequests = new List<VolunteerRequestViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT VR.Id, VR.NumVolunteers, VR.Date, VR.Description, RE.Id AS ReliefEffortId, RE.Title, RE.Description AS ReliefEffortDescription " +
                                     "FROM VOLUNTEER_REQUEST VR " +
                                     "JOIN RELIEF_EFFORT RE ON VR.Rid = RE.Id";

                SqlCommand command = new SqlCommand(selectQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    VolunteerRequestViewModel volunteerRequest = new VolunteerRequestViewModel
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        NumVolunteers = int.Parse(reader["NumVolunteers"].ToString()),
                        Date = DateOnly.Parse(reader["Date"].ToString()),
                        Description = reader["Description"].ToString(),
                        Rid = new ReliefEffortViewModel
                        {
                            Id = int.Parse(reader["ReliefEffortId"].ToString()),
                            Title = reader["Title"].ToString(),
                            Description = reader["ReliefEffortDescription"].ToString()
                        }
                    };
                    volunteerRequests.Add(volunteerRequest);
                }

                reader.Close();
            }
            return volunteerRequests;
        }
    }

}
