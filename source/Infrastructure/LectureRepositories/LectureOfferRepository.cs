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

        private static async Task<HttpResponseMessage> MakeHttpPostRequest(string url, string jsonBody)
        {
            using HttpClient client = new();
            client.Timeout = TimeSpan.FromMilliseconds(ApiTimeoutMilliseconds);
            client.DefaultRequestHeaders.Add("client_id", ClientId);
            client.DefaultRequestHeaders.Add("secret_id", SecretId);
            StringContent content = new(jsonBody, Encoding.UTF8, "application/json");
            return await client.PostAsync(url, content);
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
            LectureInquiryAdapter lectureInquiry = new LectureInquiryAdapter(inquiry);
            try
            {
                string jsonBody = JsonConvert.SerializeObject(new { lectureInquiry });
                HttpResponseMessage response = await MakeHttpPostRequest(ApiEndpoint, jsonBody);
                if (response.IsSuccessStatusCode)
                {
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

        List<Offer> IOfferRepository.GetByInquiry(Inquiry inquiry)
        {
            throw new NotImplementedException();
        }

        private class PriceResponse
        {
            public decimal? Price { get; set; }
            public List<Offer> Offers { get; set; }
            // Add other properties as needed
        }
    }
}
