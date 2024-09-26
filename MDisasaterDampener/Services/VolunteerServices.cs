using MDisasaterDampener.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace MDisasaterDampener.Services
{
    public class VolunteerServices
    {
        public void CreateRequest(VolunteerRequestViewModel volunteerRequest)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO VOLUNTEER_REQUEST (Number_Volunteers, Date, Description, Rid) " +
                               "VALUES (@Number_Volunteers, @Date, @Description, @Rid)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Number_Volunteers", volunteerRequest.Number_Volunteers);
                command.Parameters.AddWithValue("@Date", volunteerRequest.Date); 
                command.Parameters.AddWithValue("@Description", volunteerRequest.Description);
                command.Parameters.AddWithValue("@Rid", volunteerRequest.Rid.Id); 

                command.ExecuteNonQuery();
            }
        }

        public VolunteerRequestViewModel ViewRequest(int id)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            VolunteerRequestViewModel volunteerRequest = new VolunteerRequestViewModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT VR.Id, VR.Number_Volunteers, VR.Date, VR.Description, " +
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
                        volunteerRequest.Number_Volunteers = Convert.ToInt32(reader["Number_Volunteers"]);
                        volunteerRequest.Date = DateOnly.ParseExact(reader["Date"].ToString().Split(' ')[0], "yyyy/MM/dd"); 
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

        public List<VolunteerRequestViewModel> ReadRequest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            List<VolunteerRequestViewModel> volunteerRequests = new List<VolunteerRequestViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT VR.Id, VR.Number_Volunteers, VR.Date, VR.Description, RE.Id AS ReliefEffortId, RE.Title, RE.Description AS ReliefEffortDescription " +
                                     "FROM VOLUNTEER_REQUEST VR " +
                                     "JOIN RELIEF_EFFORT RE ON VR.Rid = RE.Id";

                SqlCommand command = new SqlCommand(selectQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    VolunteerRequestViewModel volunteerRequest = new VolunteerRequestViewModel
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Number_Volunteers = int.Parse(reader["Number_Volunteers"].ToString()),
                        Date = DateOnly.ParseExact(reader["Date"].ToString().Split(' ')[0], "yyyy/MM/dd"),
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

        public void CreateVolunteer( int Uid, int Vrid)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO VOLUNTEERS (Uid, Vrid) " +
                               "VALUES (@Uid, @Vrid)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Uid", Uid);
                command.Parameters.AddWithValue("@Vrid", Vrid);


                command.ExecuteNonQuery();
            }
        }
        public void UpdateNumberVolunteers( int Id)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            VolunteerRequestViewModel volunteerRequest = ViewRequest(Id);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE VOLUNTEER_REQUEST SET Number_Volunteers = @Number_Volunteers WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@Number_Volunteers", volunteerRequest.Number_Volunteers-1);
                   

                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
