using Api.Controllers;
using Frontend.Validators;
using Infrastructure.FakeRepositories;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace GetControllersUnitTest
{
    [TestClass]
    public class GetUnitTest
    {
        [TestMethod]
        public void GetAllUsersTest()
        {
            //Arrange
            FakeUserRepository repository = new();
            RegistrationValidator validator = new(1, 10);
            UsersController controller = new(repository, validator);
            //Act
            List<User> repository_output = repository.GetAll();
            var tmp = controller.Get();
            var okObjectResult = tmp.Result as OkObjectResult;
            List<User>? controller_output = okObjectResult?.Value as List<User>;
            //Assert
            Assert.IsNotNull(controller_output);
            CollectionAssert.AreEqual(repository_output, controller_output);
        }
        [TestMethod]
        public void GetUserByIDTest()
        {
            //Arrange
            FakeUserRepository repository = new();
            RegistrationValidator validator = new(1, 10);
            UsersController controller = new(repository, validator);
            //Act
            User? repository_output = repository.GetById("1");
            var tmp = controller.GetByID("1");
            var okObjectResult = tmp.Result as OkObjectResult;
            User? controller_output = okObjectResult?.Value as User;
            //Assert
            Assert.AreEqual(repository_output, controller_output);
        }
        [TestMethod]
        public void GetAllOffersTest()
        {
            //Arrange
            FakeOfferRepository repository = new();
            OffersController controller = new(repository);
            //Act
            List<Offer> repository_output = repository.GetAll().Result;
            var tmp = controller.Get().Result;
            var okObjectResult = tmp.Result as OkObjectResult;
            List<Offer>? controller_output = okObjectResult?.Value as List<Offer>;
            //Assert
            Assert.IsNotNull(controller_output);
            CollectionAssert.AreEqual(repository_output, controller_output);
        }
        [TestMethod]
        public void GetOfferByIDTest()
        {
            //Arrange
            FakeOfferRepository repository = new();
            OffersController controller = new(repository);
            //Act
            Offer? repository_output = repository.GetByID(1);
            var tmp = controller.GetByID(1);
            var okObjectResult = tmp.Result as OkObjectResult;
            Offer? controller_output = okObjectResult?.Value as Offer;
            //Assert
            Assert.AreEqual(repository_output, controller_output);
        }
        [TestMethod]
        public void GetAllContactInformationTest()
        {
            //Arrange
            FakeContactInformationRepository repository = new();
            ContactInformationValidator validator = new(1, 10);
            ContactInformationController controller = new(repository, validator);
            //Act
            List<ContactInformation> repository_output = repository.GetAll();
            var tmp = controller.Get();
            var okObjectResult = tmp.Result as OkObjectResult;
            List<ContactInformation>? controller_output = okObjectResult?.Value as List<ContactInformation>;
            //Assert
            Assert.IsNotNull(controller_output);
            CollectionAssert.AreEqual(repository_output, controller_output);
        }
        [TestMethod]
        public void GetAllRequestsTest()
        {
            //Arrange
            FakeRequestRepository repository = new();
            RequestController controller = new(repository);
            //Act
            List<Request> repository_output = repository.GetAll();
            var tmp = controller.Get();
            var okObjectResult = tmp.Result as OkObjectResult;
            List<Request>? controller_output = okObjectResult?.Value as List<Request>;
            //Assert
            Assert.IsNotNull(controller_output);
            CollectionAssert.AreEqual(repository_output, controller_output);
        }
        [TestMethod]
        public void GetRequestByIDTest()
        {
            //Arrange
            FakeRequestRepository repository = new();
            RequestController controller = new(repository);
            //Act
            Request? repository_output = repository.GetById(1);
            var tmp = controller.GetByID(1);
            var okObjectResult = tmp.Result as OkObjectResult;
            Request? controller_output = okObjectResult?.Value as Request;
            //Assert
            Assert.AreEqual(repository_output, controller_output);
        }
        [TestMethod]
        public void GetRequestsByCompanyTest()
        {
            //Arrange
            FakeRequestRepository repository = new();
            RequestController controller = new(repository);
            //Act
            List<Request> repository_output = repository.GetByCompany("Najlepsza firma");
            var tmp = controller.GetByCompany("Najlepsza firma");
            var okObjectResult = tmp.Result as OkObjectResult;
            List<Request>? controller_output = okObjectResult?.Value as List<Request>;
            //Assert
            CollectionAssert.AreEqual(repository_output, controller_output);
        }
        [TestMethod]
        public void GetRequestsByUserIdTest()
        {
            //Arrange
            FakeRequestRepository repository = new();
            RequestController controller = new(repository);
            //Act
            List<Request> repository_output = repository.GetByOwner("1");
            var tmp = controller.GetByUserID("1");
            var okObjectResult = tmp.Result as OkObjectResult;
            List<Request>? controller_output = okObjectResult?.Value as List<Request>;
            //Assert
            CollectionAssert.AreEqual(repository_output, controller_output);
        }
    }
}