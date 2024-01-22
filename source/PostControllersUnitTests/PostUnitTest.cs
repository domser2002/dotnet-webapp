using Api.Controllers;
using Domain.Model;
using Frontend.Validators;
using Infrastructure.FakeRepositories;
using Microsoft.AspNetCore.Mvc;
using ValidatorUnitTests;

namespace PostControllersUnitTests
{
    [TestClass]
    public class PostUnitTest
    {
        [TestMethod]
        public void PostUsersTest()
        {
            //Arrange
            FakeUserRepository repository = new();
            RegistrationValidator validator = new(1, 10);
            UsersController controller = new(repository, validator);
            int count_before = repository.GetAll().Count;
            //Act
            User user = UnitTestRegistrationValidator.ValidUser();
            controller.Create(user);
            int count_after = repository.GetAll().Count;    
            //Assert
            Assert.AreEqual(count_before+1, count_after);
        }
        [TestMethod]
        public void PostOffersTest()
        {
            //Arrange
            FakeOfferRepository repository = new();
            OffersController controller = new(repository);
            int count_before = repository.GetAll().Result.Count;
            //Act
            Offer offer = new();
            controller.Create(offer);
            int count_after = repository.GetAll().Result.Count;
            //Assert
            Assert.AreEqual(count_before + 1, count_after);
        }
        [TestMethod]
        public void PostContactInformationTest()
        {
            //Arrange
            FakeContactInformationRepository repository = new();
            ContactInformationValidator validator = new(1, 10);
            ContactInformationController controller = new(repository,validator);
            int count_before = repository.GetAll().Count;
            //Act
            ContactInformation contact = ContactInformationValidatorUnitTests.ValidContactInformation();
            controller.Create(contact);
            int count_after = repository.GetAll().Count;
            //Assert
            Assert.AreEqual(count_before + 1, count_after);
        }
        [TestMethod]
        public void PostRequestsTest()
        {
            //Arrange
            FakeRequestRepository repository = new();
            RequestController controller = new(repository);
            int count_before = repository.GetAll().Count;
            //Act
            Request request = new();
            controller.Create(request);
            int count_after = repository.GetAll().Count;
            //Assert
            Assert.AreEqual(count_before + 1, count_after);
        }
    }
}