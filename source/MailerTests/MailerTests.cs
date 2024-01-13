using Domain.Model;

namespace MailerTests
{
    [TestClass]
    public class MailerTests
    {
        private static readonly Address address = new() { };
        private static readonly User user = new() { FirstName = "Zuzanna", LastName = "Wojtowicz", Auth0Id = "otter", Email = "zuzannawojtowicz42@gmail.com", CompanyName = "CourierHub",
        Address = address, Id = 0, DefaultSourceAddress = address };
        private readonly Request request = new() { Id = 0, Price = 2137, CompanyName = "Wzdrygiwacze do wygibaresow i nie tylko", Owner = new(user), 
        Package = new(), Status = RequestStatus.Idle, DestinationAddress = address, SourceAddress = address, CancelDate = new(), DeliveryDate = new(), PickupDate = new() };
        private readonly Mailer.Mailer mailer = new();

        [TestMethod]
        public void SendDeliveryFailedMail()
        {
            // Act
            mailer.SendDeliveryFailedMail(request, user, "An otter ate the package.");
        }

        [TestMethod]
        public void SendPackageDeveliredMail()
        {
            // Act
            mailer.SendPackageDeveliredMail(request);
        }

        [TestMethod]
        public void SendPackagePickedUpMail()
        {
            // Act
            mailer.SendPackagePickedUpMail(request);
        }

        [TestMethod]
        public void SendRegistrationMail()
        {
            // Act
            mailer.SendRegistrationMail(user);
        }

        [TestMethod]
        public void SendRequestAcceptedMail()
        {
            // Act
            mailer.SendRequestAcceptedMail(request, "agreement.pdf", "receipt.pdf");
        }
    }
}