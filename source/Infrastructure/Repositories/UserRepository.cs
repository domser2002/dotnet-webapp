using Domain.Abstractions;
using Domain.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;

        public UserRepository() { connectionString = Connection.GetConnectionString(); }
        public UserRepository(string connection) { connectionString = connection; }

        public void AddRequest(string userID, Request request)
        {
            RequestRepository requestRepo = new(connectionString);
            Request temp = requestRepo.GetById(request.Id);
            int id;
            if (temp is null) id = requestRepo.Add(request);
            else id = request.Id;
            try
            {
                using SqlConnection connection = new(connectionString);
                string sql = $"UPDATE Requests SET OwnerId={userID} WHERE id={id}";
                using SqlCommand command = new(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
        }

        public void AddUser(User user)
        {
            try
            {
                using SqlConnection connection = new(connectionString);
                string sql = $"INSERT INTO Users VALUES (@FirstName, @LastName, @CompanyName, @Email, @Street, @StreetNumber, @FlatNumber, @PostalCode, @City, " +
                    $"@DefaultStreet, @DefaultStreetnumber, @DefaultFlatNumber, @DefaultPostalCode, @DefaultCity, @AuthId)";
                using SqlCommand command = new(sql, connection);
                command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                command.Parameters["@FirstName"].Value = user.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar);
                command.Parameters["@LastName"].Value = user.LastName;
                command.Parameters.Add("@CompanyName", SqlDbType.VarChar);
                command.Parameters["@CompanyName"].Value = user.CompanyName;
                command.Parameters.Add("@Email", SqlDbType.VarChar);
                command.Parameters["@Email"].Value = user.Email;
                command.Parameters.Add("@Street", SqlDbType.VarChar);
                command.Parameters["@Street"].Value = user.Address.Street;
                command.Parameters.Add("@StreetNumber", SqlDbType.VarChar);
                command.Parameters["@Streetnumber"].Value = user.Address.StreetNumber;
                command.Parameters.Add("@FlatNumber", SqlDbType.VarChar);
                command.Parameters["@FlatNumber"].Value = user.Address.FlatNumber;
                command.Parameters.Add("@PostalCode", SqlDbType.VarChar);
                command.Parameters["@PostalCode"].Value = user.Address.PostalCode;
                command.Parameters.Add("@City", SqlDbType.VarChar);
                command.Parameters["@City"].Value = user.Address.City;
                command.Parameters.Add("@DefaultStreet", SqlDbType.VarChar);
                command.Parameters["@DefaultStreet"].Value = user.DefaultSourceAddress.Street;
                command.Parameters.Add("@DefaultStreetnumber", SqlDbType.VarChar);
                command.Parameters["@DefaultStreetnumber"].Value = user.DefaultSourceAddress.StreetNumber;
                command.Parameters.Add("@DefaultFlatNumber", SqlDbType.VarChar);
                command.Parameters["@DefaultFlatNumber"].Value = user.DefaultSourceAddress.FlatNumber;
                command.Parameters.Add("@DefaultPostalCode", SqlDbType.VarChar);
                command.Parameters["@DefaultPostalCode"].Value = user.DefaultSourceAddress.PostalCode;
                command.Parameters.Add("@DefaultCity", SqlDbType.VarChar);
                command.Parameters["@DefaultCity"].Value = user.DefaultSourceAddress.City;
                command.Parameters.Add("@AuthId", SqlDbType.VarChar);
                command.Parameters["@AuthId"].Value = user.Auth0Id;
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
            foreach (Request request in user.Requests) AddRequest(user.Auth0Id, request);
        }

        public List<User> GetAll()
        {
            List<User> result = new();
            try
            {
                using SqlConnection connection = new(connectionString);
                string sql = "SELECT * FROM Users";
                RequestRepository repo = new(connectionString);
                using SqlCommand command = new(sql, connection);
                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Address address = new()
                    {
                        Street = reader.GetString(5),
                        StreetNumber = reader.GetString(6),
                        FlatNumber = reader.GetString(7),
                        PostalCode = reader.GetString(8),
                        City = reader.GetString(9)
                    };
                    Address defaultAddress = new()
                    {
                        Street = reader.GetString(10),
                        StreetNumber = reader.GetString(11),
                        FlatNumber = reader.GetString(12),
                        PostalCode = reader.GetString(13),
                        City = reader.GetString(14)
                    };
                    User user = new()
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        CompanyName = reader.GetString(3),
                        Email = reader.GetString(4),
                        Address = address,
                        DefaultSourceAddress = defaultAddress,
                        Auth0Id = reader.GetString(15)
                    };
                    user.Requests = repo.GetByOwner(user.Auth0Id).ToList();
                    result.Add(user);
                }
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
            return result;
        }

        public User? GetById(string Auth0Id)
        {
            try
            {
                using SqlConnection connection = new(connectionString);
                string sql = "SELECT * FROM Users WHERE AuthId = @AuthId";
                RequestRepository repo = new(connectionString);
                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@AuthId", Auth0Id);
                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                if (!reader.Read()) { return null; }
                Address address = new()
                {
                    Street = reader.GetString(5),
                    StreetNumber = reader.GetString(6),
                    FlatNumber = reader.GetString(7),
                    PostalCode = reader.GetString(8),
                    City = reader.GetString(9)
                };
                Address defaultAddress = new()
                {
                    Street = reader.GetString(10),
                    StreetNumber = reader.GetString(11),
                    FlatNumber = reader.GetString(12),
                    PostalCode = reader.GetString(13),
                    City = reader.GetString(14)
                };
                User user = new()
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    CompanyName = reader.GetString(3),
                    Email = reader.GetString(4),
                    Address = address,
                    DefaultSourceAddress = defaultAddress,
                    Auth0Id = reader.GetString(15)
                };
                user.Requests = repo.GetByOwner(user.Auth0Id).ToList();
                return user;
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); return null; }
        }

        public void Update(User user)
        {
            try
            {
                using SqlConnection connection = new(connectionString);
                var command = new SqlCommand("UPDATE Users SET FirstName = @FirstName, LastName = @LastName, CompanyName = @CompanyName," +
                    "Email = @Email, Street = @Street, StreetNumber = @StreetNumber, FlatNumber = @FlatNumber," +
                    "PostalCode = @PostalCode, City = @City, DefaultAddressStreet = @DefaultStreet, " +
                    "DefaultAddressStreetNumber = @DefaultStreetnumber, DefaultAddressFlatNumber = @DefaultFlatNumber," +
                    "DefaultAddressPostalCode = @DefaultPostalCode, DefaultAddressCity = @DefaultCity  WHERE AuthId = @AuthId", connection);
                command.Parameters.AddWithValue("@AuthId", user.Auth0Id);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@CompanyName", user.CompanyName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Street", user.Address.Street);
                command.Parameters.AddWithValue("@StreetNumber", user.Address.StreetNumber);
                command.Parameters.AddWithValue("@FlatNumber", user.Address.FlatNumber);
                command.Parameters.AddWithValue("@PostalCode", user.Address.PostalCode);
                command.Parameters.AddWithValue("@City", user.Address.City);
                command.Parameters.AddWithValue("@DefaultStreet", user.DefaultSourceAddress.Street);
                command.Parameters.AddWithValue("@DefaultStreetNumber", user.DefaultSourceAddress.StreetNumber);
                command.Parameters.AddWithValue("@DefaultFlatNumber", user.DefaultSourceAddress.FlatNumber);
                command.Parameters.AddWithValue("@DefaultPostalCode", user.DefaultSourceAddress.PostalCode);
                command.Parameters.AddWithValue("@DefaultCity", user.DefaultSourceAddress.City);
                // Add other parameters as needed for additional properties
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
        }
    }
}