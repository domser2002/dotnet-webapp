using Domain.Model;
using Frontend.Validators;

namespace ValidatorUnitTests
{
    [TestClass]
    public class InquiryValidatorUnitTest
    {
        readonly float minDimension = 5;
        readonly float maxDimension = 15;
        readonly float minWeight = 2;
        readonly float maxWeight = 10;
        public static Package ValidPackage()
        {
            return new Package()
            {
                Length = 10,
                Width = 10,
                Height = 10,
                Weight = 5
            };
        }
        public static Inquiry ValidInquiry()
        {
            return new Inquiry()
            {
                Package = ValidPackage(),
                PickupDate = (DateTime.Now).AddDays(1),
                DeliveryDate = (DateTime.Now).AddDays(2),
                SourceAddress = UnitTestRegistrationValidator.ValidAddress(),
                DestinationAddress = UnitTestRegistrationValidator.ValidAddress(),
                Priority = Priority.Low,
                Active = true,
                DeliveryAtWeekend = true
            };
        }
        [TestMethod]
        [DataRow("", "", true, "")] //valid inquiry
        [DataRow("PackageLength", 0, false, "Package's length must be greater than 5.")] //package length too small
        [DataRow("PackageLength", 20, false, "Package's length must be less than 15.")] //package length too big
        [DataRow("PackageLength", null, false, "Package's length must be specified.")] //package length null
        [DataRow("PackageWidth", 0, false, "Package's width must be greater than 5.")] //package width too small
        [DataRow("PackageWidth", 20, false, "Package's width must be less than 15.")] //package width too big
        [DataRow("PackageWidth", null, false, "Package's width must be specified.")] //package width null
        [DataRow("PackageHeight", 0, false, "Package's height must be greater than 5.")] //package height too small
        [DataRow("PackageHeight", 20, false, "Package's height must be less than 15.")] //package height too big
        [DataRow("PackageHeight", null, false, "Package's height must be specified.")] //package height null
        [DataRow("PackageWeight", 0, false, "Package's weight must be greater than 2.")] //package weight too small
        [DataRow("PackageWeight", 20, false, "Package's weight must be less than 10.")] //package weight too big
        [DataRow("PackageWeight", null, false, "Package's weight must be specified.")] //package weight null
        public void ValidatePackage(string fieldname, object value, bool expectedValue, string expectedMessage)
        {
            //Arrange
            Inquiry inquiry = ValidInquiry();
            InquireValidator inquireValidator = new(minDimension, maxDimension, minWeight, maxWeight, 1, 100);
            inquiry[fieldname] = value;
            //Act
            var result = inquireValidator.Validate(inquiry);
            //Assert
            Assert.AreEqual(expectedValue, result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
        [TestMethod]
        [DataRow(-1,0,false,"Pickup date must be set in the future.")] //pickup date in past
        [DataRow(0,0,false,"Pickup date must be set in the future.")] //pickup date now
        [DataRow(1,-1,false,"Delivery date must be set in the future.")] //delivery date in past
        [DataRow(1,0,false,"Delivery date must be set in the future.")] //delivery date now
        [DataRow(2,1,false,"Delivery date must be set after pickup date.")] //delivery before pickup
        [DataRow(1,2,true,"")] //valid dates
        public void ValidateDate(int pickupDaysFromNow,int deliveryDaysFromNow,bool expectedValue,string expectedMessage)
        {
            //Arrange
            Inquiry inquiry= ValidInquiry();
            inquiry.PickupDate = (DateTime.Now).AddDays(pickupDaysFromNow);
            inquiry.DeliveryDate = (DateTime.Now).AddDays(deliveryDaysFromNow);
            InquireValidator inquireValidator= new(minDimension, maxDimension,minWeight,maxWeight, 1, 100);
            //Act
            var result = inquireValidator.Validate(inquiry);
            //Assert
            Assert.AreEqual(expectedValue, result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
        [TestMethod]
        [DataRow("PickupDate","Pickup date must be specified.")]
        [DataRow("DeliveryDate","Delivery date must be specified.")]
        public void ValidateDateNull(string fieldname,string expectedMessage)
        {
            //Arrange
            Inquiry inquiry= ValidInquiry();
            inquiry[fieldname] = null;
            InquireValidator inquireValidator = new(minDimension, maxDimension, minWeight, maxWeight, 1, 100);
            //Act
            var result = (inquireValidator.Validate(inquiry));
            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
    }
}
