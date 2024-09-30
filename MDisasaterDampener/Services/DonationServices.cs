using MDisasaterDampener.Models;
using Microsoft.Data.SqlClient;

namespace MDisasaterDampener.Services
{
    public class DonationServices
    {
        public void CreateFoodDonation(FoodDonationViewModel foodDonation)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO FOOD_DONATION (Category,  Item_Name, Description_and_inner_units, Expiry, Weight, Donation_Date, RE_Id) " +
                               "VALUES (@Category, @Item_Name, @Description_and_inner_units, @Expiry, @Weight, @Donation_Date, @RE_Id)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Category", foodDonation.Category);

                    command.Parameters.AddWithValue("@Item_Name", foodDonation.Item_Name);
                    command.Parameters.AddWithValue("@Description_and_inner_units", foodDonation.Description_and_inner_units);
                    command.Parameters.AddWithValue("@Expiry", foodDonation.Expiry);
                    command.Parameters.AddWithValue("@Weight", foodDonation.Weight);
                    command.Parameters.AddWithValue("@Donation_Date", DateOnly.FromDateTime(DateTime.Now));
                    command.Parameters.AddWithValue("@RE_Id", foodDonation.RE_Id.Id);

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<FoodDonationViewModel> GetAllFoodDonations()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            List<FoodDonationViewModel> foodDonations = new List<FoodDonationViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT FD.FD_Id, FD.Category,  FD.Item_Name, FD.Description_and_inner_units, FD.Expiry, FD.Weight, FD.Donation_Date," +
                               "RE.Id AS ReliefEffortId, RE.Title, RE.Description AS ReliefEffortDescription " +
                               "FROM FOOD_DONATION FD " +
                               "JOIN RELIEF_EFFORT RE ON FD.RE_Id = RE.Id"
                              ;


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        FoodDonationViewModel foodDonation = new FoodDonationViewModel
                        {
                            FD_Id = int.Parse(reader["FD_Id"].ToString()),
                            Category = (FoodDonationViewModel.Categories)int.Parse(reader["Category"].ToString()),

                            Item_Name = reader["Item_Name"].ToString(),
                            Description_and_inner_units = reader["Description_and_inner_units"].ToString(),
                            Expiry = DateOnly.ParseExact(reader["Expiry"].ToString().Split(' ')[0], "yyyy/MM/dd"),
                            Weight = reader["Weight"].ToString(),
                            Donation_Date = DateOnly.ParseExact(reader["Donation_Date"].ToString().Split(' ')[0], "yyyy/MM/dd"),
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
            }
            return foodDonations;
        }
        public void CreateMedicineDonation(MedicineDonationViewModel donation)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO MEDICINE_DONATION (Description, Expiry, Unit_Type, Donation_Date, RE_Id) " +
                               "VALUES ( @Description, @Expiry, @Unit_Type, @Donation_Date, @RE_Id)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                   

                
                    command.Parameters.AddWithValue("@Description", donation.Description);
                    command.Parameters.AddWithValue("@Expiry", donation.Expiry);
                    command.Parameters.AddWithValue("@Unit_Type", donation.Unit_Type);
                    command.Parameters.AddWithValue("@Donation_Date", DateOnly.FromDateTime(DateTime.Now));
                    command.Parameters.AddWithValue("@RE_Id", donation.RE_Id.Id);

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<MedicineDonationViewModel> GetAllMedicineDonations()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            List<MedicineDonationViewModel> donations = new List<MedicineDonationViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MD.MD_Id, MD.Description, MD.Expiry, MD.Unit_Type, MD.Donation_Date," +
                               "RE.Id AS ReliefEffortId, RE.Title, RE.Description AS ReliefEffortDescription " +
                               "FROM MEDICINE_DONATION MD " +
                               "JOIN RELIEF_EFFORT RE ON MD.RE_Id = RE.Id"
                              ;


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        MedicineDonationViewModel donation = new MedicineDonationViewModel
                        {
                            MD_Id = int.Parse(reader["MD_Id"].ToString()),
                           
                            Description= reader["Description"].ToString(),
                            Expiry = DateOnly.ParseExact(reader["Expiry"].ToString().Split(' ')[0], "yyyy/MM/dd"),
                            Unit_Type = Convert.ToInt32(reader["Unit_Type"]),
                            Donation_Date = DateOnly.ParseExact(reader["Donation_Date"].ToString().Split(' ')[0], "yyyy/MM/dd"),
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
            }
            return donations;
        } 
        public void CreateClothingDonation(ClothingDonationViewModel donation)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO CLOTHING_DONATION (Item_Description, Quantity, Material, Donation_Date, RE_Id) " +
                               "VALUES ( @Item_Description, @Quantity, @Material, @Donation_Date, @RE_Id)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                   

                
                    command.Parameters.AddWithValue("@Description", donation.Item_Description);
                    command.Parameters.AddWithValue("@Quantity", donation.Quantity);
                    command.Parameters.AddWithValue("@Material", donation.Material);
                    command.Parameters.AddWithValue("@Donation_Date", DateOnly.FromDateTime(DateTime.Now));
                    command.Parameters.AddWithValue("@RE_Id", donation.RE_Id.Id);

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<ClothingDonationViewModel> GetAllClothingDonations()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            List<ClothingDonationViewModel> donations = new List<ClothingDonationViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CD.CD_Id, CD.Item_Description, CD.Quantity, CD.Material, CD.Donation_Date," +
                               "RE.Id AS ReliefEffortId, RE.Title, RE.Description AS ReliefEffortDescription " +
                               "FROM CLOTHING_DONATION CD " +
                               "JOIN RELIEF_EFFORT RE ON CD.RE_Id = RE.Id"
                              ;


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ClothingDonationViewModel donation = new ClothingDonationViewModel
                        {
                            CD_Id = int.Parse(reader["CD_Id"].ToString()),

                            Item_Description = reader["Item_Description"].ToString(),
                            Material = reader["Material"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            Donation_Date = DateOnly.ParseExact(reader["Donation_Date"].ToString().Split(' ')[0], "yyyy/MM/dd"),
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
            }
            return donations;
        }
    }

    
}
