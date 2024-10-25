//using MDisasaterDampener.Models;
//using Microsoft.Data.SqlClient;

namespace MDisasaterDampener.Services
{
    public class MessagesServices
    {
        /* public List<MessagesViewModel> ReadMessages(int userId)
         {
             List<MessagesViewModel> messages = new List<MessagesViewModel>();

             using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 connection.Open();
                 string query = @"SELECT M.Id, M.Body, M.DateSent, V.V_Id, U.User_Id, U.Name, VR.VLR_Id
                          FROM MESSAGES M
                          JOIN VOLUNTEERS V ON M.V_Id = V.V_Id
                          JOIN USERS U ON V.User_Id = U.User_Id
                          JOIN VOLUNTEER_REQUEST VR ON V.VLR_Id = VR.VLR_Id
                          WHERE U.User_Id = @User_Id";

                 using (SqlCommand command = new SqlCommand(query, connection))
                 {
                     command.Parameters.AddWithValue("@User_Id", userId);

                     SqlDataReader reader = command.ExecuteReader();
                     while (reader.Read())
                     {
                         MessagesViewModel message = new MessagesViewModel
                         {
                             Id = int.Parse(reader["Id"].ToString()),
                             Body = reader["Body"].ToString(),
                             DateSent = DateOnly.FromDateTime(DateTime.Parse(reader["DateSent"].ToString())),
                             Vid = new VolunteerViewModel
                             {
                                 Id = int.Parse(reader["V_Id"].ToString()),
                                 Uid = new UserViewModel
                                 {
                                     id = int.Parse(reader["User_Id"].ToString()),
                                     username = reader["Username"].ToString(),
                                 },
                                 Vrid = new VolunteerRequestViewModel
                                 {
                                     Id = int.Parse(reader["VLR_Id"].ToString())
                                 }
                             }
                         };
                         messages.Add(message);
                     }
                 }
             }

             return messages;
         }*/

    }
}
