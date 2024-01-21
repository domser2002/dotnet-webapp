using Domain.Abstractions;
using Domain.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Infrastructure.LectureRepositories
{
    public class LectureOfferRepository
    {
        static async Task<List<Offer>> MakeApiPostRequest(string token,Inquiry inquiry)
        {
            var apiUrl = "https://mini.currier.api.snet.com.pl";
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            LectureInquiryAdapter lectureInquiry = new(inquiry);
            var payload = new
            {
                dimensions = new
                {
                    width = 1,
                    height = 1,
                    length = 1,
                    dimensionUnit = "Meters"
                },
                currency = "PLN",
                weight = 1,
                weightUnit = "Kilograms",
                source = new
                {
                    houseNumber = "1",
                    apartmentNumber = "2",
                    street = "string",
                    city = "string",
                    zipCode = "string",
                    country = "string"
                },
                destination = new
                {
                    houseNumber = "2",
                    apartmentNumber = "1",
                    street = "string",
                    city = "string",
                    zipCode = "string",
                    country = "string"
                },
                pickupDate = new DateTime(2024, 2, 19),
                deliveryDay = new DateTime(2024, 2, 24),
                deliveryInWeekend = true,
                priority = "High",
                vipPackage = true,
                isComapny = true
            };
            var jsonPayload = JsonSerializer.Serialize(payload);
            var response = await client.PostAsync($"{apiUrl}/Inquires", new StringContent(jsonPayload, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var lectureOffer = JsonSerializer.Deserialize<LectureOffer>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (lectureOffer is null) return new List<Offer>();
                return new List<Offer> { new Offer(lectureOffer,inquiry) };
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API POST Request failed: {response.StatusCode} {content}");
            }
            return new List<Offer>();
        }
        public static async Task<List<Offer>> GetByInquiry(Inquiry inquiry)
        {
            var token = await GetAccessToken();

            if (!string.IsNullOrEmpty(token))
            {
                var result = await MakeApiPostRequest(token,inquiry);
                return result;
            }
            else
            {
                Console.WriteLine("Failed to obtain access token.");
            }
            return new List<Offer>();
        }
        static async Task<string?> GetAccessToken()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://indentitymanager.snet.com.pl");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return null;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "team1a",
                ClientSecret = "4FBCF2E8-2845-4EAA-BE16-6B971798F846",
                Scope = "MiNI.Courier.API"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }

            return tokenResponse.AccessToken;
        }
    }
}
