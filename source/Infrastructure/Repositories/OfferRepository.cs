using Domain.Abstractions;
using Domain.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly string connectionString;

        public OfferRepository() { connectionString = Connection.GetConnectionString(); }
        public OfferRepository(string connection) { connectionString = connection; }

        public void AddOffer(Offer offer)
        {
            try
            {
                using SqlConnection connection = new(connectionString);
                string sql = "INSERT INTO Offers VALUES (@CompanyName, @Price, @DeliveryTime, @Begins, @Ends, @MinDimension, @MaxDimension, @MinWeight, @MaxWeight, @Active)";
                using SqlCommand command = new(sql, connection);
                command.Parameters.Add("@CompanyName", SqlDbType.VarChar);
                command.Parameters["@CompanyName"].Value = offer.CompanyName;
                command.Parameters.Add("@Price", SqlDbType.Money);
                command.Parameters["@Price"].Value = offer.Price;
                command.Parameters.Add("@DeliveryTime", SqlDbType.Int);
                command.Parameters["@DeliveryTime"].Value = offer.DeliveryTime;
                command.Parameters.Add("@Begins", SqlDbType.DateTime);
                command.Parameters["@Begins"].Value = offer.Begins;
                command.Parameters.Add("@Ends", SqlDbType.DateTime);
                command.Parameters["@Ends"].Value = offer.Ends;
                command.Parameters.Add("@MinDimension", SqlDbType.Float);
                command.Parameters["@MinDimension"].Value = offer.MinDimension;
                command.Parameters.Add("@MaxDimension", SqlDbType.Float);
                command.Parameters["@MaxDimension"].Value = offer.MaxDimension;
                command.Parameters.Add("@MinWeight", SqlDbType.Float);
                command.Parameters["@MinWeight"].Value = offer.MinWeight;
                command.Parameters.Add("@MaxWeight", SqlDbType.Float);
                command.Parameters["@MaxWeight"].Value = offer.MaxWeight;
                command.Parameters.Add("@Active", SqlDbType.Bit);
                command.Parameters["@Active"].Value = offer.Active;
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
        }

        public void Deactivate(int id)
        {
            try
            {
                using SqlConnection connection = new(connectionString);
                string sql = $"UPDATE Offers SET Active=0 WHERE id={id}";
                using SqlCommand command = new(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
        }

        public List<Offer> GetAll()
        {
            List<Offer> result = new();
            try
            {
                using SqlConnection connection = new(connectionString);
                string sql = "SELECT * FROM Offers";

                using SqlCommand command = new(sql, connection);
                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Offer offer = new()
                    {
                        Id = reader.GetInt32(0),
                        CompanyName = reader.GetString(1),
                        Price = reader.GetSqlMoney(2).Value,
                        DeliveryTime = reader.GetInt32(3),
                        Begins = reader.GetDateTime(4),
                        Ends = reader.GetDateTime(5),
                        MinDimension = (float)reader.GetDouble(6),
                        MaxDimension = (float)reader.GetDouble(7),
                        MinWeight = (float)reader.GetDouble(8),
                        MaxWeight = (float)reader.GetDouble(9),
                        Active = reader.GetBoolean(10)
                    };
                    result.Add(offer);
                }
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
            return result;
        }

        public Offer GetByID(int id)
        {
            Offer result = new();
            try
            {
                using SqlConnection connection = new(connectionString);
                string sql = $"SELECT * FROM Offers WHERE id={id}";

                using SqlCommand command = new(sql, connection);
                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                result.Id = reader.GetInt32(0);
                result.CompanyName = reader.GetString(1);
                result.Price = reader.GetSqlMoney(2).Value;
                result.DeliveryTime = reader.GetInt32(3);
                result.Begins = reader.GetDateTime(4);
                result.Ends = reader.GetDateTime(5);
                result.MinDimension = (float)reader.GetDouble(6);
                result.MaxDimension = (float)reader.GetDouble(7);
                result.MinWeight = (float)reader.GetDouble(8);
                result.MaxWeight = (float)reader.GetDouble(9);
                result.Active = reader.GetBoolean(10);
            }
            catch (SqlException e) { Console.WriteLine(e.ToString()); }
            return result;
        }

        public List<Offer> GetByInquiry(Inquiry inquiry)
        {
            List<Offer> offers = new();
            foreach (var offer in GetAll())
            {
                if (offer.MatchesInquiry(inquiry))
                    offers.Add(offer);
            }
            return offers;
        }
    }
}