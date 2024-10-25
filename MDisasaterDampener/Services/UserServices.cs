using Dapper;
using MDisasaterDampener.Models;
using MDisasaterDampener.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;
using System.Text;


namespace MDisasaterDampener.Services
{
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
#pragma warning disable IDE0046 // Convert to conditional expression
#pragma warning disable CA1305 // Specify IFormatProvider
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
    public class UserServices(IDatabaseServices service) : IUserServices
    {
        private readonly IDatabaseServices databaseServices = service;

        public void Register(RegisterViewModel user)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
     .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            try
            {
                using SqlConnection connection = new(connectionString);
                connection.Open();
                string query = "INSERT INTO USERS (FirstName, LastName, Username, Email, Password) VALUES (@FirstName, @LastName, @Username, @Email, @Password)";
                SqlCommand command = new(query, connection);

                _ = command.Parameters.AddWithValue("@FirstName", user.firstName);
                _ = command.Parameters.AddWithValue("@LastName", user.lastName);
                _ = command.Parameters.AddWithValue("@Username", user.username);
                _ = command.Parameters.AddWithValue("@Email", user.email);

                // Log or check the encrypted password
                string encryptedPassword = EncryptPassword(user.password);
                Console.WriteLine($"Encrypted Password: {encryptedPassword}");

                _ = command.Parameters.AddWithValue("@Password", encryptedPassword);

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


            byte[] data = SHA256.HashData(Encoding.UTF8.GetBytes(password));


            StringBuilder stringBuilder = new();


            for (int i = 0; i < data.Length; i++)
            {
                _ = stringBuilder.Append(data[i].ToString("x2"));
            }

            return stringBuilder.ToString();

        }

        public static bool ValidatePassword(string inputPassword, string hashedPassword)
        {
            string hashedInput = EncryptPassword(inputPassword);
            return string.Equals(hashedInput, hashedPassword, StringComparison.Ordinal);
        }

        public UserViewModel? Login(LoginViewModel returningUser)

        {
            string query = "SELECT * FROM USERS WHERE Username = @Username";
            var parameters = new { Username = returningUser.username };

            using IDbConnection connection = databaseServices.GetConnection();

            IEnumerable<UserViewModel> users = connection.Query<UserViewModel>(query, parameters);
            UserViewModel? user = users.FirstOrDefault();


            if (user != null && ValidatePassword(returningUser.password, user.password))
            {
                return user;
            }



            return null;
        }

        public void ChangeUsername(UserViewModel user, int id)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            using SqlConnection connection = new(connectionString);

            connection.Open();
            string query = "UPDATE USERS SET Username = @Username WHERE Id = @Id";
            using SqlCommand command = new(query, connection);

            _ = command.Parameters.AddWithValue("@Id", id);
            _ = command.Parameters.AddWithValue("@password", EncryptPassword(user.password));

            _ = command.ExecuteNonQuery();


        }
        public void ChangeEmail(UserViewModel user, int id)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            using SqlConnection connection = new(connectionString);

            connection.Open();
            string query = "UPDATE USERS SET Email = @Email WHERE Id = @Id";
            using SqlCommand command = new(query, connection);

            _ = command.Parameters.AddWithValue("@Id", id);
            _ = command.Parameters.AddWithValue("@Email", EncryptPassword(user.password));

            _ = command.ExecuteNonQuery();


        }
        public void ChangePassword(UserViewModel user, int id)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            using SqlConnection connection = new(connectionString);

            connection.Open();
            string query = "UPDATE USERS SET Password = @Password WHERE Id = @Id";
            using SqlCommand command = new(query, connection);

            _ = command.Parameters.AddWithValue("@Id", id);
            _ = command.Parameters.AddWithValue("@Password", EncryptPassword(user.password));

            _ = command.ExecuteNonQuery();


        }


    }
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable IDE0046 // Convert to conditional expression
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
}
