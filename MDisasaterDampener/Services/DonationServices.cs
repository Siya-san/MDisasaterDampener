using MDisasaterDampener.Models;
using MDisasaterDampener.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace MDisasaterDampener.Services
{
    public class DonationServices : IDonationServices
    {
#pragma warning disable CA1305 // Specify IFormatProvider
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.

        public void CreateFoodDonation(FoodDonationViewModel foodDonation)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");


            using SqlConnection connection = new(connectionString);
            connection.Open();
            string query = "INSERT INTO FOOD_DONATION (Category,  Item_Name, Description_and_inner_units, Expiry, Weight, Donation_Date, RE_Id) " +
                           "VALUES (@Category, @Item_Name, @Description_and_inner_units, @Expiry, @Weight, @Donation_Date, @RE_Id)";
            using SqlCommand command = new(query, connection);
            _ = command.Parameters.AddWithValue("@Category", foodDonation.Category);

            _ = command.Parameters.AddWithValue("@Item_Name", foodDonation.Item_Name);
            _ = command.Parameters.AddWithValue("@Description_and_inner_units", foodDonation.Description_and_inner_units);
            _ = command.Parameters.AddWithValue("@Expiry", foodDonation.Expiry);
            _ = command.Parameters.AddWithValue("@Weight", foodDonation.Weight);
            _ = command.Parameters.AddWithValue("@Donation_Date", DateOnly.FromDateTime(DateTime.Now)).ToString();
            _ = command.Parameters.AddWithValue("@RE_Id", foodDonation.RE_Id.Id);

            _ = command.ExecuteNonQuery();
        }
        public List<FoodDonationViewModel> GetAllFoodDonations()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            List<FoodDonationViewModel> foodDonations = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                string query = "SELECT FD.FD_Id, FD.Category,  FD.Item_Name, FD.Description_and_inner_units, FD.Expiry, FD.Weight, FD.Donation_Date," +
                               "RE.Id AS ReliefEffortId, RE.Title, RE.Description AS ReliefEffortDescription " +
                               "FROM FOOD_DONATION FD " +
                               "JOIN RELIEF_EFFORT RE ON FD.RE_Id = RE.Id"
                              ;


                using SqlCommand command = new(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FoodDonationViewModel foodDonation = new()
                    {
                        FD_Id = int.Parse(reader["FD_Id"].ToString()),
                        Category = (FoodDonationViewModel.Categories)int.Parse(reader["Category"].ToString()),

                        Item_Name = reader["Item_Name"].ToString(),
                        Description_and_inner_units = reader["Description_and_inner_units"].ToString(),
                        Expiry = reader["Expiry"].ToString(),
                        Weight = reader["Weight"].ToString(),
                        Donation_Date = reader["Donation_Date"].ToString(),
                        RE_Id = new ReliefEffortViewModel
                        {
                            Id = Convert.ToInt32(reader["ReliefEffortId"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["ReliefEffortDescription"].ToString()
                        }
                    };
                    foodDonations.Add(foodDonation);
                }
            }
            return foodDonations;
        }
        public void CreateMedicineDonation(MedicineDonationViewModel donation)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string query = "INSERT INTO MEDICINE_DONATION (Description, Expiry, Unit_Type, Donation_Date, RE_Id) " +
                           "VALUES ( @Description, @Expiry, @Unit_Type, @Donation_Date, @RE_Id)";
            using SqlCommand command = new(query, connection);



            _ = command.Parameters.AddWithValue("@Description", donation.Description);
            _ = command.Parameters.AddWithValue("@Expiry", donation.Expiry);
            _ = command.Parameters.AddWithValue("@Unit_Type", donation.Unit_Type);
            _ = command.Parameters.AddWithValue("@Donation_Date", DateOnly.FromDateTime(DateTime.Now)).ToString();
            _ = command.Parameters.AddWithValue("@RE_Id", donation.RE_Id.Id);

            _ = command.ExecuteNonQuery();
        }
        public List<MedicineDonationViewModel> GetAllMedicineDonations()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            List<MedicineDonationViewModel> donations = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                string query = "SELECT MD.MD_Id, MD.Description, MD.Expiry, MD.Unit_Type, MD.Donation_Date," +
                               "RE.Id AS ReliefEffortId, RE.Title, RE.Description AS ReliefEffortDescription " +
                               "FROM MEDICINE_DONATION MD " +
                               "JOIN RELIEF_EFFORT RE ON MD.RE_Id = RE.Id"
                              ;


                using SqlCommand command = new(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    MedicineDonationViewModel donation = new()
                    {
                        MD_Id = int.Parse(reader["MD_Id"].ToString()),

                        Description = reader["Description"].ToString(),
                        Expiry = reader["Expiry"].ToString(),
                        Unit_Type = Convert.ToInt32(reader["Unit_Type"]),
                        Donation_Date = reader["Donation_Date"].ToString(),
                        RE_Id = new ReliefEffortViewModel
                        {
                            Id = Convert.ToInt32(reader["ReliefEffortId"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["ReliefEffortDescription"].ToString()
                        }
                    };

                    donations.Add(donation);
                }
            }
            return donations;
        }
        public void CreateClothingDonation(ClothingDonationViewModel donation)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string query = "INSERT INTO CLOTHING_DONATION (Item_Description, Quantity, Material, Donation_Date, RE_Id) " +
                           "VALUES ( @Item_Description, @Quantity, @Material, @Donation_Date, @RE_Id)";
            using SqlCommand command = new(query, connection);



            _ = command.Parameters.AddWithValue("@Description", donation.Item_Description);
            _ = command.Parameters.AddWithValue("@Quantity", donation.Quantity);
            _ = command.Parameters.AddWithValue("@Material", donation.Material);
            _ = command.Parameters.AddWithValue("@Donation_Date", DateOnly.FromDateTime(DateTime.Now)).ToString();
            _ = command.Parameters.AddWithValue("@RE_Id", donation.RE_Id.Id);

            _ = command.ExecuteNonQuery();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        public List<ClothingDonationViewModel> GetAllClothingDonations()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            List<ClothingDonationViewModel> donations = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                string query = "SELECT CD.CD_Id, CD.Item_Description, CD.Quantity, CD.Material, CD.Donation_Date," +
                               "RE.Id AS ReliefEffortId, RE.Title, RE.Description AS ReliefEffortDescription " +
                               "FROM CLOTHING_DONATION CD " +
                               "JOIN RELIEF_EFFORT RE ON CD.RE_Id = RE.Id"
                              ;


                using SqlCommand command = new(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClothingDonationViewModel donation = new()
                    {
                        CD_Id = int.Parse(reader["CD_Id"].ToString()),

                        Item_Description = reader["Item_Description"].ToString(),
                        Material = reader["Material"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Donation_Date = reader["Donation_Date"].ToString(),
                        RE_Id = new ReliefEffortViewModel
                        {
                            Id = Convert.ToInt32(reader["ReliefEffortId"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["ReliefEffortDescription"].ToString()
                        }
                    };
                    donations.Add(donation);
                }
            }
            return donations;
        }
    }
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CA1305 // Specify IFormatProvider

}
