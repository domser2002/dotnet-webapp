using Domain.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using Infrastructure.Repositories;
using Infrastructure;

namespace DBIntegrationTests
{
    [TestClass]
    public class DBIntegrationTests
    {
        private readonly DateTime date1 = new(2025, 3, 1, 7, 0, 0);
        private readonly DateTime date2 = new(2026, 3, 1, 7, 0, 0);
        private readonly Address address = new() { City="Bartodzieje", Street="Uliczna", FlatNumber="1", PostalCode="00-000", StreetNumber="1" };

        [TestMethod]
        public void ConnectToDatabase()
        {
            //Arrange
            using var connection = new SqlConnection(Connection.GetConnectionString());

            // Act
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to open database connection: {ex.Message}");
            }

            // Assert
            Assert.AreEqual(ConnectionState.Open, connection.State, "Database should be connected");
        }

        [TestMethod]
        public void InsertIntoUsers()
        {
            //Arrange
            User user = new() { FirstName = "Koziołek", LastName="Matołek", Address=address, DefaultSourceAddress=address, CompanyName="Firma", Email="adres@mail.com", Auth0Id = "1" };
            using var connection = new SqlConnection(Connection.GetConnectionString());
            string select = "SELECT COUNT(*) FROM Users";

            //Act
            SqlCommand command = new(select, connection);
            connection.Open();
            int amount1 = (int)command.ExecuteScalar();
            UserRepository repo = new();
            repo.AddUser(user);
            int amount2 = (int)command.ExecuteScalar();
            connection.Close();

            //Assert
            Assert.AreEqual(amount1, amount2 - 1, "The number of Users in the database should increase by 1");

        }

        [TestMethod]
        public void InsertIntoContactInformation()
        {
            //Arrange
            ContactInformation contactInformation = new() { Address=address, Email= "adres@mail.com", PersonalData="XXX" };
            using var connection = new SqlConnection(Connection.GetConnectionString());
            string select = "SELECT COUNT(*) FROM ContactInformation";

            //Act
            SqlCommand command = new(select, connection);
            connection.Open();
            int amount1 = (int)command.ExecuteScalar();
            ContactInformationRepository repo = new();
            repo.AddContactInformation(contactInformation);
            int amount2 = (int)command.ExecuteScalar();
            connection.Close();

            //Assert
            Assert.AreEqual(amount1, amount2 - 1, "The number of entries in the ContactInformation database should increase by 1");
        }

        [TestMethod]
        public void InsertIntoInquiries()
        {
            //Arrange
            UserRepository userRepo = new();
            List<User> users = userRepo.GetAll();
            int owner = users[0].Id;
            Package package = new() { Length=1, Weight=1, Height=1, Width=1 };
            Inquiry inquiry = new() { Active=true, DeliveryAtWeekend=true, DeliveryDate=date2, PickupDate=date1, DestinationAddress=address, SourceAddress=address, 
                Priority=Priority.Low, Package=package, OwnerId = owner };
            using var connection = new SqlConnection(Connection.GetConnectionString());
            string select = "SELECT COUNT(*) FROM Inquiries";

            //Act
            SqlCommand command = new(select, connection);
            connection.Open();
            int amount1 = (int)command.ExecuteScalar();
            InquireRepository repo = new();
            repo.AddInquiry(inquiry);
            int amount2 = (int)command.ExecuteScalar();
            connection.Close();

            //Assert
            Assert.AreEqual(amount1, amount2 - 1, "The number of inquiries in the database should increase by 1");
        }

        [TestMethod]
        public void InsertIntoOffers()
        {
            //Arrange
            Offer offer = new() { Active=true, Begins=date1, Ends=date2, CompanyName="Firm", DeliveryTime=1, MaxDimension=10, MaxWeight=10, MinDimension=1, MinWeight=1, Price=1 };
            using var connection = new SqlConnection(Connection.GetConnectionString());
            string select = "SELECT COUNT(*) FROM Offers";

            //Act
            SqlCommand command = new(select, connection);
            connection.Open();
            int amount1 = (int)command.ExecuteScalar();
            OfferRepository repo = new();
            repo.AddOffer(offer);
            int amount2 = (int)command.ExecuteScalar();
            connection.Close();

            //Assert
            Assert.AreEqual(amount1, amount2 - 1, "The number of offers in the database should increase by 1");
        }
    }
}