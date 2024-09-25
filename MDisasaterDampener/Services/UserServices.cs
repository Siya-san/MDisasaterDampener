using MDisasaterDampener.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;


namespace MDisasaterDampener.Services
{
    public class UserServices
    {
        public void Register(RegisterViewModel user)
        {
            var configuration = new ConfigurationBuilder()
     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
     .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO USERS (FirstName, LastName, Username, Email, Password) VALUES (@FirstName, @LastName, @Username, @Email, @Password)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FirstName", user.firstName);
                    command.Parameters.AddWithValue("@LastName", user.lastName);
                    command.Parameters.AddWithValue("@Username", user.username);
                    command.Parameters.AddWithValue("@Email", user.email);

                    // Log or check the encrypted password
                    string encryptedPassword = EncryptPassword(user.password);
                    Console.WriteLine($"Encrypted Password: {encryptedPassword}");

                    command.Parameters.AddWithValue("@Password", encryptedPassword);

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        Console.WriteLine("User registered successfully.");
                    }
                    else
                    {
                        Console.WriteLine("User registration failed.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
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
            return System.String.Equals(hashedInput, hashedPassword, StringComparison.Ordinal);
        }
        public UserViewModel Login(LoginViewModel returningUser)
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            UserViewModel user=new UserViewModel();
            // Open a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM USERS WHERE Username=@Username ";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", returningUser.username);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader != null)
                        {
                            if (ValidatePassword(returningUser.password, reader["Password"].ToString()))
                            {
                                user.id = int.Parse(reader["Id"].ToString());
                                user.firstName = reader["FirstName"].ToString();
                                user.lastName = reader["LastName"].ToString();
                                user.username = reader["Username"].ToString();
                                user.email = reader["Email"].ToString();
                                user.password = reader["Password"].ToString();
                                return user;
                            }
                        }
                    }
                    return null;
                }




            }

        }
        public void ChangeUsername(UserViewModel User, int Id)
        {

            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE USERS SET Username = @Username WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@password", EncryptPassword(User.password));
                    command.Parameters.AddWithValue("@defaultPw", false);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void ChangeEmail( UserViewModel User, int Id)
        {

            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE USERS SET Email = @Email WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@Email", EncryptPassword(User.password));
                  
                    command.ExecuteNonQuery();
                }
            }
        } public void ChangePassword(UserViewModel User, int Id)
        {

            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE USERS SET Password = @Password WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@Password", EncryptPassword(User.password));
                  
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
