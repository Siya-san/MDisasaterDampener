using MDisasaterDampener.Models;
using MDisasaterDampener.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace MDisasaterDampener.Services
{
#pragma warning disable CA1305 // Specify IFormatProvider
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
    public class VolunteerServices : IVolunteerServices
    {
        public void CreateRequest(VolunteerRequestViewModel volunteerRequest)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string query = "INSERT INTO VOLUNTEER_REQUEST (Number_Volunteers, Date, Description, Rid) " +
                           "VALUES (@Number_Volunteers, @Date, @Description, @Rid)";
            SqlCommand command = new(query, connection);
            _ = command.Parameters.AddWithValue("@Number_Volunteers", volunteerRequest.Number_Volunteers);
            _ = command.Parameters.AddWithValue("@Date", volunteerRequest.Date);
            _ = command.Parameters.AddWithValue("@Description", volunteerRequest.Description);
            _ = command.Parameters.AddWithValue("@Rid", volunteerRequest.Rid.Id);

            _ = command.ExecuteNonQuery();
        }

        public VolunteerRequestViewModel ViewRequest(int id)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            VolunteerRequestViewModel volunteerRequest = new();

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                string query = "SELECT VR.Id, VR.Number_Volunteers, VR.Date, VR.Description, " +
                               "RE.Id AS ReliefEffortId, RE.Title, RE.Description AS ReliefEffortDescription " +
                               "FROM VOLUNTEER_REQUEST VR " +
                               "JOIN RELIEF_EFFORT RE ON VR.Rid = RE.Id " +
                               "WHERE VR.Id = @Id";

                SqlCommand command = new(query, connection);
                _ = command.Parameters.AddWithValue("@Id", id);

                using SqlDataReader reader = command.ExecuteReader();

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

            return volunteerRequest;
        }

        public List<VolunteerRequestViewModel> ReadRequest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            List<VolunteerRequestViewModel> volunteerRequests = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT VR.Id, VR.Number_Volunteers, VR.Date, VR.Description, RE.Id AS ReliefEffortId, RE.Title, RE.Description AS ReliefEffortDescription " +
                                     "FROM VOLUNTEER_REQUEST VR " +
                                     "JOIN RELIEF_EFFORT RE ON VR.Rid = RE.Id";

                SqlCommand command = new(selectQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    VolunteerRequestViewModel volunteerRequest = new()
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

        public void CreateVolunteer(int Uid, int Vrid)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string query = "INSERT INTO VOLUNTEERS (Uid, Vrid) " +
                           "VALUES (@Uid, @Vrid)";
            SqlCommand command = new(query, connection);
            _ = command.Parameters.AddWithValue("@Uid", Uid);
            _ = command.Parameters.AddWithValue("@Vrid", Vrid);


            _ = command.ExecuteNonQuery();
        }
        public void UpdateNumberVolunteers(int Id)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            VolunteerRequestViewModel volunteerRequest = ViewRequest(Id);
            using SqlConnection connection = new(connectionString);
            connection.Open();
            string query = "UPDATE VOLUNTEER_REQUEST SET Number_Volunteers = @Number_Volunteers WHERE Id = @Id";
            using SqlCommand command = new(query, connection);

            _ = command.Parameters.AddWithValue("@Id", Id);
            _ = command.Parameters.AddWithValue("@Number_Volunteers", volunteerRequest.Number_Volunteers - 1);


            _ = command.ExecuteNonQuery();

        }
    }
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CA1305 // Specify IFormatProvider
}
