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
            FakeRequestRepository requestRepository = new();
            UserRepository databaseUsers = new();
            OfferRepository databaseOffers = new();
            InquireRepository databaseInquiries = new();
            ContactInformationRepository databaseContactInformation = new();
            RequestRepository databaseRequests = new();
            foreach(var user in users.GetAll()) { databaseUsers.AddUser(user); }
            foreach (var offer in offers.GetAll()) { databaseOffers.AddOffer(offer); }
            foreach (var inquiry in inquiries.GetAll()) { databaseInquiries.AddInquiry(inquiry); }
            foreach (var contact in contactInformation.GetAll()) { databaseContactInformation.AddContactInformation(contact); }
            foreach (var request in requestRepository.GetAll()) { databaseRequests.Add(request); }
        }

        public static void Clear()
        {
            try
            {
                SqlConnection connection = new(Connection.GetConnectionString());
                SqlCommand command1 = new("DELETE FROM Users", connection);
                SqlCommand command2 = new("DELETE FROM Offers", connection);
                SqlCommand command3 = new("DELETE FROM Inquiries", connection);
                SqlCommand command4 = new("DELETE FROM ContactInformation", connection);
                SqlCommand command5 = new("DELETE FROM Requests", connection);
                connection.Open();
                command5.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command4.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
        }
    }
}
