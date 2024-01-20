﻿using Domain.Abstractions;
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
        private const string ClientId = "team1a";
        private const string SecretId = "4FBCF2E8-2845-4EAA-BE16-6B971798F846";

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

        public static async Task<List<Offer>> GetByInquiry(Inquiry inquiry)
        {
            LectureInquiryAdapter lectureInquiry = new LectureInquiryAdapter(inquiry);
            try
            {
                string jsonBody = JsonConvert.SerializeObject(new { lectureInquiry });
                HttpResponseMessage response = await MakeHttpPostRequest(ApiEndpoint, jsonBody);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    LectureOffer lectureoffer = JsonConvert.DeserializeObject<LectureOffer>(responseBody);

                    if (lectureoffer != null)
                    {
                        // Return the list of offers (assuming PriceResponse has an Offers property)
                        return new List<Offer> { new Offer(lectureoffer, inquiry) };
                    }
                    else
                    {
                        Console.WriteLine("Failed to parse the response.");
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
            return new List<Offer>();
        }

        List<Offer> IOfferRepository.GetByInquiry(Inquiry inquiry)
        {
            List<Offer> offers = GetByInquiry(inquiry).Result;
            Thread.Sleep(ApiTimeoutMilliseconds+1000);
            return offers;
        }
    }
}
