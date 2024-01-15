using Domain.Abstractions;
using Domain.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories;

public class RequestRepository : IRequestRepository
{
    public int Add(Request request)
    {
        int index = 0;
        try
        {
            using SqlConnection connection = new(Connection.GetConnectionString());
            string sql = $"INSERT INTO Requests VALUES(NULL, @Length, @Width, @Height, @Weight, @PickupDate, @DeliveryDate, @Street, @StreetNumber, " +
                $"@FlatNumber, @PostalCode, @City, @DestinationStreet, @DestinationStreetnumber, @DestinationFlatNumber, @DestinationPostalCode, @DestinationCity, NULL, " +
                $"@CompanyName, @Price, @Status, @CM) SELECT SCOPE_IDENTITY()";
            using SqlCommand command = new(sql, connection);
            command.Parameters.Add("@Length", SqlDbType.Float);
            command.Parameters["@Length"].Value = request.Package!.Length;
            command.Parameters.Add("@Width", SqlDbType.Float);
            command.Parameters["@Width"].Value = request.Package.Width;
            command.Parameters.Add("@Height", SqlDbType.Float);
            command.Parameters["@Height"].Value = request.Package.Height;
            command.Parameters.Add("@Weight", SqlDbType.Float);
            command.Parameters["@Weight"].Value = request.Package.Weight;
            command.Parameters.Add("@PickupDate", SqlDbType.DateTime);
            command.Parameters["@PickupDate"].Value = request.PickupDate;
            command.Parameters.Add("@DeliveryDate", SqlDbType.DateTime);
            command.Parameters["@DeliveryDate"].Value = request.DeliveryDate;
            command.Parameters.Add("@Street", SqlDbType.VarChar);
            command.Parameters["@Street"].Value = request.SourceAddress!.Street;
            command.Parameters.Add("@StreetNumber", SqlDbType.VarChar);
            command.Parameters["@Streetnumber"].Value = request.SourceAddress.StreetNumber;
            command.Parameters.Add("@FlatNumber", SqlDbType.VarChar);
            command.Parameters["@FlatNumber"].Value = request.SourceAddress.FlatNumber;
            command.Parameters.Add("@PostalCode", SqlDbType.VarChar);
            command.Parameters["@PostalCode"].Value = request.SourceAddress.PostalCode;
            command.Parameters.Add("@City", SqlDbType.VarChar);
            command.Parameters["@City"].Value = request.SourceAddress.City;
            command.Parameters.Add("@DestinationStreet", SqlDbType.VarChar);
            command.Parameters["@DestinationStreet"].Value = request.DestinationAddress!.Street;
            command.Parameters.Add("@DestinationStreetnumber", SqlDbType.VarChar);
            command.Parameters["@DestinationStreetnumber"].Value = request.DestinationAddress.StreetNumber;
            command.Parameters.Add("@DestinationFlatNumber", SqlDbType.VarChar);
            command.Parameters["@DestinationFlatNumber"].Value = request.DestinationAddress.FlatNumber;
            command.Parameters.Add("@DestinationPostalCode", SqlDbType.VarChar);
            command.Parameters["@DestinationPostalCode"].Value = request.DestinationAddress.PostalCode;
            command.Parameters.Add("@DestinationCity", SqlDbType.VarChar);
            command.Parameters["@DestinationCity"].Value = request.DestinationAddress.City;
            command.Parameters.Add("@PersonalData", SqlDbType.VarChar);
            command.Parameters["@PersonalData"].Value = request.Owner.PersonalData;
            command.Parameters.Add("@Price", SqlDbType.Money);
            command.Parameters["@Price"].Value = request.Price;
            command.Parameters.Add("@Status", SqlDbType.Int);
            command.Parameters["@Status"].Value = request.Status;
            command.Parameters.Add("@CM", SqlDbType.Int);
            command.Parameters["@CM"].Value = request.Owner.Id;
            command.Parameters.Add("@CompanyName", SqlDbType.VarChar);
            command.Parameters["@CompanyName"].Value = request.CompanyName;
            connection.Open();
            index = (int)(decimal)command.ExecuteScalar();
            connection.Close();
        }
        catch (SqlException e) { Console.WriteLine(e.ToString()); }
        if (request.CancelDate is not null)
        {
            try
            {
                using SqlConnection connection = new(Connection.GetConnectionString());
                string sql = $"UPDATE Requests SET CancelDate={request.CancelDate} WHERE id={request.Id}";
                using SqlCommand command = new(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
        }
        return index;
    }

    public void ChangeStatus(int id, RequestStatus status)
    {
        try
        {
            using SqlConnection connection = new(Connection.GetConnectionString());
            string sql = $"UPDATE Requests SET RequestStatus={(int)status} WHERE id={id}";
            using SqlCommand command = new(sql, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (SqlException e) { Console.WriteLine(e.ToString()); }
    }

    public void Delete(int id)
    {
        try
        {
            using SqlConnection connection = new(Connection.GetConnectionString());
            string sql = $"DELETE FROM Requests WHERE id={id}";
            using SqlCommand command = new(sql, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (SqlException e) { Console.WriteLine(e.ToString()); }
    }

    public List<Request> GetAll()
    {
        return ReadFromDatabase("SELECT * FROM Requests");
    }

    public List<Request> GetByCompany(string company)
    {
        return ReadFromDatabase($"SELECT * FROM Requests WHERE Companyname={company}");
    }

    public Request GetById(int id)
    {
        List<Request> result = ReadFromDatabase($"SELECT * FROM Requests WHERE Id={id}");
        if (result.Count == 0) return null!;
        return result[0];
    }

    public List<Request> GetByOwner(string ownerId)
    {
        return ReadFromDatabase($"SELECT * FROM Requests WHERE OwnerId={ownerId}");
    }

    public void Update(Request request)
    {
        try
        {
            using SqlConnection connection = new(Connection.GetConnectionString());
            var command = new SqlCommand("UPDATE Requests SET SourceAddressStreet = @SourceAddressStreet" +
                "SourceAddressStreetNumber = @SourceAddressStreetNumber, SourceAddressFlatNumber = " +
                "@SourceAddressFlatNumber, SourceAddressPostalCode = @SourceAddressPostalCode," +
                "SourceAddressCity = @SourceAddressCity, DestinationAddressStreet = @DestinationAddressStreet," +
                "DestinationAddressStreetNumber = @DestinationAddressStreetNumber, " +
                "DestinationAddressFlatNumber = @SourceAddressFlatNumber, DestinationAddressPostalCode = @DestinationAddressPostalCode" +
                "DestinationAddressCity = @DestinationAddressCity, PickupDate = @PickupDate," +
                "DeliveryDate = @DeliveryDate, CancelDate = @CancelDate, RequestStatus = @RequestStatus", connection);
            command.Parameters.AddWithValue("@SourceAddressStreet", request.SourceAddress.Street);
            command.Parameters.AddWithValue("@SourceAddressStreetNumber", request.SourceAddress.StreetNumber);
            command.Parameters.AddWithValue("@SourceAddressFlatNumber", request.SourceAddress.FlatNumber);
            command.Parameters.AddWithValue("@SourceAddressPostalCode", request.SourceAddress.PostalCode);
            command.Parameters.AddWithValue("@SourceAddressCity", request.SourceAddress.City);
            command.Parameters.AddWithValue("@DestinationAddressStreet", request.DestinationAddress.Street);
            command.Parameters.AddWithValue("@DestinationAddressStreetNumber", request.DestinationAddress.StreetNumber);
            command.Parameters.AddWithValue("@DestinationAddressFlatNumber", request.DestinationAddress.FlatNumber);
            command.Parameters.AddWithValue("@DestinationAddressPostalCode", request.DestinationAddress.PostalCode);
            command.Parameters.AddWithValue("@DestinationAddressCity", request.DestinationAddress.City);
            command.Parameters.AddWithValue("@PickupDate", request.PickupDate);
            command.Parameters.AddWithValue("@DeliveryDate", request.DeliveryDate);
            command.Parameters.AddWithValue("@CancelDate", request.CancelDate);
            command.Parameters.AddWithValue("@RequestStatus", request.Status);
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (SqlException e) { Console.WriteLine(e.ToString()); }
    }

    private List<Request> ReadFromDatabase(string query)
    {
        List<Request> result = new();
        ContactInformationRepository cmRepo = new();
        try
        {
            using SqlConnection connection = new(Connection.GetConnectionString());
            using SqlCommand command = new(query, connection);
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Package package = new()
                {
                    Length = (float)reader.GetDouble(2),
                    Width = (float)reader.GetDouble(3),
                    Height = (float)reader.GetDouble(4),
                    Weight = (float)reader.GetDouble(5)
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
                ContactInformation owner = cmRepo.GetAll().Where(cm => cm.Id == reader.GetInt32(22)).ToList().First();
                DateTime? cancel;
                if (reader.IsDBNull(18)) cancel = null;
                else cancel = reader.GetDateTime(18);
                Request request = new()
                {
                    Id = reader.GetInt32(0),
                    Package = package,
                    PickupDate = reader.GetDateTime(6),
                    DeliveryDate = reader.GetDateTime(7),
                    SourceAddress = sourceAddress,
                    DestinationAddress = destinationAddress,
                    Owner = owner,
                    CancelDate = cancel,
                    CompanyName = reader.GetString(19),
                    Price = reader.GetSqlMoney(20).Value,
                    Status = (RequestStatus)reader.GetInt32(21)
                };
                result.Add(request);
            }
        }
        catch (SqlException e) { Console.WriteLine(e.ToString()); }
        return result;
    }
}
