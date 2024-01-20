using Domain.Abstractions;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infrastructure.LectureRepositories
{
    public class LectureOfferRepository : IOfferRepository
    {
        private const string ApiEndpoint = "https://mini.currier.api.snet.com.pl/Inquires";
        private const int ApiTimeoutMilliseconds = 5000;
        private const string ClientId = "your_client_id";
        private const string SecretId = "your_secret_id";

        private async Task<HttpResponseMessage> MakeHttpPostRequest(string url, string jsonBody)
        {
            using (HttpClient client = new())
            {
                // Add client_id and secret_id to the request headers
                client.DefaultRequestHeaders.Add("client_id", ClientId);
                client.DefaultRequestHeaders.Add("secret_id", SecretId);

                StringContent content = new(jsonBody, Encoding.UTF8, "application/json");

                // Make the HTTP POST request with a timeout
                return await client.PostAsync(url, content);
            }
        }
        public void AddOffer(Offer offer)
        {
            throw new NotImplementedException();
        }
        public void Deactivate(int id)
        {
            throw new NotImplementedException();
        }

        public List<Offer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Offer GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Offer>> GetByInquiry(Inquiry inquiry)
        {
            try
            {
                // Convert Inquiry object to JSON
                string jsonBody = JsonConvert.SerializeObject(new { inquiry });

                // Make HTTP POST request with client_id and secret_id headers
                HttpResponseMessage response = await MakeHttpPostRequest(ApiEndpoint, jsonBody);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response and get the price
                    string responseBody = await response.Content.ReadAsStringAsync();
                    PriceResponse priceResponse = JsonConvert.DeserializeObject<PriceResponse>(responseBody);

                    if (priceResponse != null && priceResponse.Price.HasValue)
                    {
                        // Return the list of offers (assuming PriceResponse has an Offers property)
                        return priceResponse.Offers;
                    }
                    else
                    {
                        Console.WriteLine("Failed to parse price from the response.");
                    }
                }
                else
                {
                    Console.WriteLine($"HTTP request failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            // Return an empty list if there's an error
            return new List<Offer>();
        }
        private class PriceResponse
        {
            public decimal? Price { get; set; }
            public List<Offer> Offers { get; set; }
            // Add other properties as needed
        }
    }
}
