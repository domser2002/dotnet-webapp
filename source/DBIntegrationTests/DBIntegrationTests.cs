﻿using Domain.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using Infrastructure.Repositories;
using Infrastructure;
using Infrastructure.FakeRepositories;

namespace DBIntegrationTests
{
    [TestClass]
    public class DBIntegrationTests
    {
        [TestMethod]
        public void ConnectToDatabase()
        {
            //Arrange
            using var connection = new SqlConnection(Connection.GetConnectionString());

            // Act
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to open database connection: {ex.Message}");
            }

            // Assert
            Assert.AreEqual(ConnectionState.Open, connection.State, "Database should be connected");
        }

        [TestMethod]
        public void InsertIntoUsers()
        {
            //Arrange
            DatabaseSeeding.Clear();
            UserRepository repo = new();
            SqlConnection connection = new(Connection.GetConnectionString());
            User user = new FakeUserRepository().GetAll().First();
            SqlCommand command = new("SELECT COUNT(*) FROM Users", connection);

            //Act
            connection.Open();
            repo.AddUser(user);
            int amount = (int)command.ExecuteScalar();
            connection.Close();

            //Assert
            Assert.AreEqual(1, amount, "The number of Users in the database should increase by 1");
        }

        [TestMethod]
        public void InsertIntoContactInformation()
        {
            //Arrange
            DatabaseSeeding.Clear();
            ContactInformationRepository repo = new();
            SqlConnection connection = new(Connection.GetConnectionString());
            ContactInformation contactInformation = new FakeContactInformationRepository().GetAll().First();
            SqlCommand command = new("SELECT COUNT(*) FROM ContactInformation", connection);

            //Act
            connection.Open();
            repo.AddContactInformation(contactInformation);
            int amount = (int)command.ExecuteScalar();
            connection.Close();

            //Assert
            Assert.AreEqual(1, amount, "The number of entries in the ContactInformation database should increase by 1");
        }

        [TestMethod]
        public void InsertIntoOffers()
        {
            //Arrange
            DatabaseSeeding.Clear();
            OfferRepository repo = new();
            SqlConnection connection = new SqlConnection(Connection.GetConnectionString());
            Offer offer = new FakeOfferRepository().GetAll().First();
            SqlCommand command = new("SELECT COUNT(*) FROM Offers", connection);

            //Act
            connection.Open();
            repo.AddOffer(offer);
            int amount = (int)command.ExecuteScalar();
            connection.Close();

            //Assert
            Assert.AreEqual(1, amount, "The number of offers in the database should increase by 1");
        }

        [TestMethod]
        public void InsertIntoRequests()
        {
            //Arrange
            DatabaseSeeding.Clear();
            RequestRepository repo = new();
            ContactInformation owner = new FakeContactInformationRepository().GetAll().First();
            ContactInformationRepository infoRepo = new();
            infoRepo.AddContactInformation(owner);
            owner = infoRepo.GetAll().First();
            Request request = new FakeRequestRepository().GetAll().First();
            request.Owner = owner;
            SqlConnection connection = new SqlConnection(Connection.GetConnectionString());
            SqlCommand command = new("SELECT COUNT(*) FROM Requests", connection);

            //Act
            connection.Open();
            repo.Add(request);
            int amount = (int)command.ExecuteScalar();
            connection.Close();

            //Assert
            Assert.AreEqual(1, amount, "The number of requests in the database should increase by 1");
        }


        [TestMethod]
        public void DaleteFromRequests()
        {
            //Arrange
            DatabaseSeeding.Clear();
            RequestRepository repo = new();
            int index = repo.Add(new FakeRequestRepository().GetAll().First());
            SqlConnection connection = new SqlConnection(Connection.GetConnectionString());
            SqlCommand command = new("SELECT COUNT(*) FROM Requests", connection);

            //Act
            connection.Open();
            repo.Delete(index);
            int amount = (int)command.ExecuteScalar();
            connection.Close();

            //Assert
            Assert.AreEqual(0, amount, "The number of requests in the database should decrease by 1");
        }

        [TestMethod]
        public void AssignNewRequestToUser()
        {
            //Arrange
            DatabaseSeeding.Clear();
            UserRepository userRepo = new();
            User user = new FakeUserRepository().GetAll().First();
            user.Requests = new();
            userRepo.AddUser(user);
            ContactInformationRepository infoRepo = new();
            infoRepo.AddContactInformation(new FakeContactInformationRepository().GetAll().First());
            ContactInformation owner = infoRepo.GetAll().First();
            Request request = new FakeRequestRepository().GetAll().First();
            request.Id = 0;
            request.Owner = owner;

            //Act
            new UserRepository().AddRequest(user.Auth0Id, request);
            int amount = new RequestRepository().GetByOwner(user.Auth0Id).Count;

            //Assert
            Assert.AreEqual(1, amount, "The number of requests assigned to the user should increase by 1");
        }

        [TestMethod]
        public void AssignExistingRequestToUser()
        {
            //Arrange
            DatabaseSeeding.Clear();
            UserRepository userRepo = new();
            User user = new FakeUserRepository().GetAll().First();
            user.Requests = new();
            userRepo.AddUser(user);
            ContactInformationRepository infoRepo = new();
            infoRepo.AddContactInformation(new FakeContactInformationRepository().GetAll().First());
            ContactInformation owner = infoRepo.GetAll().First();
            Request request = new FakeRequestRepository().GetAll().First();
            request.Owner = owner;
            RequestRepository requestRepo = new();
            int index = requestRepo.Add(request);
            request.Id = index;

            //Act
            new UserRepository().AddRequest(user.Auth0Id, request);
            int amount = requestRepo.GetByOwner(user.Auth0Id).Count;

            //Assert
            Assert.AreEqual(1, amount, "The number of requests assigned to the user should increase by 1");
        }

        [TestMethod]
        public void DeactivateOffer()
        {
            //Arrange
            DatabaseSeeding.Clear();
            Offer offer = new FakeOfferRepository().GetAll().First();
            offer.Active = true;
            OfferRepository repo = new();
            repo.AddOffer(offer);
            int index = repo.GetAll().First().Id;

            //Act
            repo.Deactivate(index);

            //Assert
            Assert.IsFalse(repo.GetByID(index).Active, "Offer should be deactivated");
        }

        [TestMethod]
        public void ChangeRequestStatus()
        {
            //Arrange
            DatabaseSeeding.Clear();
            RequestRepository repo = new();
            ContactInformationRepository infoRepo = new();
            infoRepo.AddContactInformation(new FakeContactInformationRepository().GetAll().First());
            ContactInformation owner = infoRepo.GetAll().First();
            Request request = new FakeRequestRepository().GetAll().First();
            request.Owner = owner;
            request.Status = RequestStatus.Idle;
            int index = repo.Add(request);

            //Act
            repo.ChangeStatus(index, RequestStatus.Cancelled);
            request = repo.GetById(index);

            //Assert
            Assert.AreEqual(RequestStatus.Cancelled, request.Status, "Request status should change.");
        }
    }
}