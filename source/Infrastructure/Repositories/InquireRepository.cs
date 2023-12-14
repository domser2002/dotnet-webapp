using Domain.Abstractions;
using Domain.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class InquireRepository : IInquireRepository
    {
        public void AddInquiry(Inquiry inquiry)
        {
            try
            {
                using SqlConnection connection = new(Connection.GetConnectionString());
                string sql = $"INSERT INTO Inquiries VALUES (@Length, @Width, @Height, @Weight, @PickupDate, @DeliveryDate, @Street, @StreetNumber, @FlatNumber, @PostalCode, @City, " +
                    $"@DestinationStreet, @DestinationStreetnumber, @DestinationFlatNumber, @DestinationPostalCode, @DestinationCity, @Priority, @Weekend, @Active)";
                using SqlCommand command = new(sql, connection);
                command.Parameters.Add("@Length", SqlDbType.Float);
                command.Parameters["@Length"].Value = inquiry.Package.Length;
                command.Parameters.Add("@Width", SqlDbType.Float);
                command.Parameters["@Width"].Value = inquiry.Package.Width;
                command.Parameters.Add("@Height", SqlDbType.Float);
                command.Parameters["@Height"].Value = inquiry.Package.Height;
                command.Parameters.Add("@Weight", SqlDbType.Float);
                command.Parameters["@Weight"].Value = inquiry.Package.Weight;
                command.Parameters.Add("@PickupDate", SqlDbType.DateTime);
                command.Parameters["@PickupDate"].Value = inquiry.PickupDate;
                command.Parameters.Add("@DeliveryDate", SqlDbType.DateTime);
                command.Parameters["@DeliveryDate"].Value = inquiry.DeliveryDate;
                command.Parameters.Add("@Street", SqlDbType.VarChar);
                command.Parameters["@Street"].Value = inquiry.SourceAddress.Street;
                command.Parameters.Add("@StreetNumber", SqlDbType.VarChar);
                command.Parameters["@Streetnumber"].Value = inquiry.SourceAddress.StreetNumber;
                command.Parameters.Add("@FlatNumber", SqlDbType.VarChar);
                command.Parameters["@FlatNumber"].Value = inquiry.SourceAddress.FlatNumber;
                command.Parameters.Add("@PostalCode", SqlDbType.VarChar);
                command.Parameters["@PostalCode"].Value = inquiry.SourceAddress.PostalCode;
                command.Parameters.Add("@City", SqlDbType.VarChar);
                command.Parameters["@City"].Value = inquiry.SourceAddress.City;
                command.Parameters.Add("@DestinationStreet", SqlDbType.VarChar);
                command.Parameters["@DestinationStreet"].Value = inquiry.DestinationAddress.Street;
                command.Parameters.Add("@DestinationStreetnumber", SqlDbType.VarChar);
                command.Parameters["@DestinationStreetnumber"].Value = inquiry.DestinationAddress.StreetNumber;
                command.Parameters.Add("@DestinationFlatNumber", SqlDbType.VarChar);
                command.Parameters["@DestinationFlatNumber"].Value = inquiry.DestinationAddress.FlatNumber;
                command.Parameters.Add("@DestinationPostalCode", SqlDbType.VarChar);
                command.Parameters["@DestinationPostalCode"].Value = inquiry.DestinationAddress.PostalCode;
                command.Parameters.Add("@DestinationCity", SqlDbType.VarChar);
                command.Parameters["@DestinationCity"].Value = inquiry.DestinationAddress.City;
                command.Parameters.Add("@Priority", SqlDbType.Int);
                command.Parameters["@Priority"].Value = inquiry.Priority;
                command.Parameters.Add("@Weekend", SqlDbType.Bit);
                command.Parameters["@Weekend"].Value = inquiry.DeliveryAtWeekend;
                command.Parameters.Add("@Active", SqlDbType.Bit);
                command.Parameters["@Active"].Value = inquiry.Active;
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
        }

        public List<Inquiry> GetAll()
        {
            List<Inquiry> result = new();
            try
            {
                using SqlConnection connection = new(Connection.GetConnectionString());
                string sql = "SELECT * FROM Inquiries";

                using SqlCommand command = new(sql, connection);
                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Package package = new()
                    {
                        Length = reader.GetFloat(2),
                        Width = reader.GetFloat(3),
                        Height = reader.GetFloat(4),
                        Weight = reader.GetFloat(5)
                    };
                    Address sourceAddress = new()
                    {
                        Street = reader.GetString(8),
                        StreetNumber = reader.GetString(9),
                        FlatNumber = reader.GetString(10),
                        PostalCode = reader.GetString(11),
                        City = reader.GetString(12)
                    };
                    Address destinationAddress = new()
                    {
                        Street = reader.GetString(13),
                        StreetNumber = reader.GetString(14),
                        FlatNumber = reader.GetString(15),
                        PostalCode = reader.GetString(16),
                        City = reader.GetString(17)
                    };
                    Inquiry inquiry = new()
                    {
                        Id = reader.GetInt32(0),
                        Package = package,
                        PickupDate = reader.GetDateTime(6),
                        DeliveryDate = reader.GetDateTime(7),
                        SourceAddress = sourceAddress,
                        DestinationAddress = destinationAddress,
                        Priority = (Priority)reader.GetInt32(18),
                        DeliveryAtWeekend = reader.GetBoolean(19),
                        Active = reader.GetBoolean(20)
                    };
                    result.Add(inquiry);
                }
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
            return result;
        }
    }
}
