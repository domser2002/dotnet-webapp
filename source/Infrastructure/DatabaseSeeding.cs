using Domain.Model;
using Infrastructure.FakeRepositories;
using Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
namespace Infrastructure
{
    public static class DatabaseSeeding
    {
        public static void SeedDatabase()
        {
            FakeUserRepository users = new();
            FakeOfferRepository offers = new();
            FakeInquiryRepository inquiries = new();
            FakeContactInformationRepository contactInformation = new();
            UserRepository databaseUsers = new();
            OfferRepository databaseOffers = new();
            InquireRepository databaseInquiries = new();
            ContactInformationRepository databaseContactInformation = new();
            foreach(var user in users.GetAll()) { databaseUsers.AddUser(user); }
            foreach (var offer in offers.GetAll()) { databaseOffers.AddOffer(offer); }
            foreach (var inquiry in inquiries.GetAll()) { databaseInquiries.AddInquiry(inquiry); }
            foreach (var contact in contactInformation.GetAll()) { databaseContactInformation.AddContactInformation(contact); }
        }

        public static void Clear()
        {
            try
            {
                using SqlConnection connection = new(Connection.GetConnectionString());
                string sql1 = "DELETE FROM Users";
                using SqlCommand command1 = new(sql1, connection);
                string sql2 = "DELETE FROM Offers";
                using SqlCommand command2 = new(sql2, connection);
                string sql3 = "DELETE FROM Inquiries";
                using SqlCommand command3 = new(sql3, connection);
                string sql4 = "DELETE FROM ContactInformation";
                using SqlCommand command4 = new(sql4, connection);
                connection.Open();
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                command4.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
        }
    }
}
