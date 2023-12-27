using Domain.Abstractions;
using Domain.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class ContactInformationRepository : IContactInformationRepository
    {
        public void AddContactInformation(ContactInformation contactInformation)
        {
            try
            {
                using SqlConnection connection = new(Connection.GetConnectionString());
                string sql = "INSERT INTO ContactInformation VALUES (@PersonalData, @Email, @Street, @Streetnumber, @FlatNumber, @PostalCode, @City";
                using SqlCommand command = new(sql, connection);
                command.Parameters.Add("@PersonalData", SqlDbType.VarChar);
                command.Parameters["@PersonalData"].Value = contactInformation.PersonalData;
                command.Parameters.Add("@Email", SqlDbType.VarChar);
                command.Parameters["@Email"].Value = contactInformation.Email;
                command.Parameters.Add("@Street", SqlDbType.VarChar);
                command.Parameters["@Street"].Value = contactInformation.Address.Street;
                command.Parameters.Add("@StreetNumber", SqlDbType.VarChar);
                command.Parameters["@StreetNumber"].Value = contactInformation.Address.StreetNumber;
                command.Parameters.Add("@Flatnumber", SqlDbType.VarChar);
                command.Parameters["@FlatNumber"].Value = contactInformation.Address.FlatNumber;
                command.Parameters.Add("@PostalCode", SqlDbType.VarChar);
                command.Parameters["@PostalCode"].Value = contactInformation.Address.PostalCode;
                command.Parameters.Add("@City", SqlDbType.VarChar);
                command.Parameters["@City"].Value = contactInformation.Address.City;
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
        }

        public List<ContactInformation> GetAll()
        {
            List<ContactInformation> result = new();
            try
            {
                using SqlConnection connection = new(Connection.GetConnectionString());
                string sql = "SELECT * FROM ContactInformation";

                using SqlCommand command = new(sql, connection);
                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Address address = new()
                    {
                        Street = reader.GetString(3),
                        StreetNumber = reader.GetString(4),
                        FlatNumber = reader.GetString(5),
                        PostalCode = reader.GetString(6),
                        City = reader.GetString(7)
                    };
                    ContactInformation contact = new()
                    {
                        Id = reader.GetInt32(0),
                        PersonalData = reader.GetString(1),
                        Email = reader.GetString(2),
                        Address = address
                    };
                    result.Add(contact);
                }
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
            return result;
        }
    }
}