using MDisasaterDampener.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace MDisasaterDampener.Services
{
    public class UserViewModelSevices
    {
        public void Insert(UserViewModel user)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");


            // Open a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO users (isAdmin, firstName, lastName, email, password) VALUES (@isAdmin, @FirstName, @LastName, @Email, @Password)";
                SqlCommand command = new SqlCommand(query, connection);
           
                command.Parameters.AddWithValue("@FirstName", user.firstName);
                command.Parameters.AddWithValue("@LastName", user.lastName);
                command.Parameters.AddWithValue("@Username", user.username);
                command.Parameters.AddWithValue("@Email", user.email);
                command.Parameters.AddWithValue("@Password", EncryptPassword(user.password));
                command.ExecuteNonQuery();
            }
        }
        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));


                StringBuilder stringBuilder = new StringBuilder();


                for (int i = 0; i < data.Length; i++)
                {
                    stringBuilder.Append(data[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        public static bool ValidatePassword(string inputPassword, string hashedPassword)
        {
            string hashedInput = EncryptPassword(inputPassword);
            return String.Equals(hashedInput, hashedPassword, StringComparison.Ordinal);
        }
       /* public UserViewModel Login(UserViewModel user)
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");


            // Open a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM users WHERE email=@Email ";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", user.Email);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader != null)
                        {
                            if (ValidatePassword(user.Password, reader["password"].ToString()))
                            {
                                user.FirstName = reader["firstName"].ToString();
                                user.LastName = reader["lastName"].ToString();
                                user.Email = reader["email"].ToString();
                                user.Password = reader["password"].ToString();
                                return user;
                            }
                        }
                    }
                    return null;
                }


            

        }}*/

    }
}
