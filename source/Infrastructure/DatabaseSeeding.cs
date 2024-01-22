using Infrastructure.FakeRepositories;
using Infrastructure.Repositories;
using Microsoft.Data.SqlClient;

namespace Infrastructure
{
    public static class DatabaseSeeding
    {
        public static void SeedDatabase(string? connection = null)
        {
            string con;
            if (connection is null) con = Connection.GetConnectionString();
            else con = connection;
            FakeUserRepository users = new();
            FakeOfferRepository offers = new();
            FakeContactInformationRepository contactInformation = new();
            FakeRequestRepository requestRepository = new();
            UserRepository databaseUsers = new(con);
            OfferRepository databaseOffers = new(con);
            ContactInformationRepository databaseContactInformation = new(con);
            RequestRepository databaseRequests = new(con);
            foreach(var user in users.GetAll()) { databaseUsers.AddUser(user); }
            //foreach (var offer in offers.GetAll()) { databaseOffers.AddOffer(offer); }
            foreach (var contact in contactInformation.GetAll()) { databaseContactInformation.AddContactInformation(contact); }
            foreach (var request in requestRepository.GetAll()) { databaseRequests.Add(request); }
        }

        public static void Clear(string? connection = null)
        {
            try
            {
                SqlConnection con;
                if (connection is null) con = new(Connection.GetConnectionString());
                else con = new(connection);
                SqlCommand command1 = new("DELETE FROM Users", con);
                SqlCommand command2 = new("DELETE FROM Offers", con);
                SqlCommand command3 = new("DELETE FROM Inquiries", con);
                SqlCommand command4 = new("DELETE FROM ContactInformation", con);
                SqlCommand command5 = new("DELETE FROM Requests", con);
                con.Open();
                command5.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command4.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
        }
    }
}
