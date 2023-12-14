using Domain.Abstractions;
using Domain.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void AddOffer(int id, int offerID)
        {
            List<User> users = GetAll();
            foreach (User user in users)
            {
                if (user.Id == id)
                {
                    user.Inquiries.Add(offerID);
                    return;
                }
            }
        }

        public void AddUser(User user)
        {
            try
            {
                using SqlConnection connection = new(Connection.GetConnectionString());
                string sql = $"INSERT INTO Users VALUES (@FirstName, @LastName, @CompanyName, @Email, @Street, @StreetNumber, @FlatNumber, @PostalCode, @City, " +
                    $"@DefaultStreet, @DefaultStreetnumber, @DefaultFlatNumber, @DefaultPostalCode, @DefaultCity)";
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
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
        }

        public List<User> GetAll()
        {
            List<User> result = new();
            try
            {
                using SqlConnection connection = new(Connection.GetConnectionString());
                string sql = "SELECT * FROM Users";

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
                        DefaultSourceAddress = defaultAddress
                    };
                    result.Add(user);
                }
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
            return result;
        }
    }
}