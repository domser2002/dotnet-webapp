using Domain.Model;
using Frontend.Validators;

namespace ValidatorUnitTests
{
    [TestClass]
    public class ContactInformationValidatorUnitTests
    {
        readonly int minStringLength = 4;
        readonly int maxStringLength = 5;
        public static ContactInformation ValidContactInformation()
        {
            return new ContactInformation()
            {
                PersonalData="aaaa",
                Email="aa@aaa.aa",
                Address = UnitTestRegistrationValidator.ValidAddress()
            };
        }
        [TestMethod]
        [DataRow("","",true,"")]
        [DataRow("PersonalData","aaa",false,"Personal data must contain at least 4 characters.")] //personal data too short
        [DataRow("PersonalData","aaaaaa",false,"Personal data must contain no more than 5 characters.")] //personal data too long
        [DataRow("PersonalData",null,false,"Personal data must be specified.")] //personal data null
        [DataRow("Email", "aaa.bbb", false, "Email must contain exactly one monkey.")] //email with no monkey
        [DataRow("Email", "aa@aa@aa.bb", false, "Email must contain exactly one monkey.")] //email with two monkeys
        [DataRow("Email", "aa@bbb", false, "Email must contain a dot.")] //email with no dot
        [DataRow("Email", "@aa.bb", false, "Email must contain at least one character in prefix.")] //email with no letters in prefix
        [DataRow("Email", "a.a@", false, "Email must contain at least one character in domain.")] //email with no letters in domain
        [DataRow("Email", null, false, "Email must be specified.")] //email null
        public void ValidateContactInformation(string fieldname,string value,bool expectedValue,string expectedMessage)
        {
            //Arrange
            ContactInformation contactInformation = ValidContactInformation();
            contactInformation[fieldname] = value;
            ContactInformationValidator contactInformationValidator=new(minStringLength,maxStringLength);
            //Act
            var result = contactInformationValidator.Validate(contactInformation);
            //Assert
            Assert.AreEqual(expectedValue, result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
        [TestMethod]
        [DataRow("Street", "aaa", false, "Street must contain at least 4 characters.")] //street too short
        [DataRow("Street", "aaaaaa", false, "Street must contain no more than 5 characters.")] //street too long
        [DataRow("Street", "a@aa", false, "Street can only contain letters, numbers and spaces.")] //street contains monkey
        [DataRow("Street", " aaa", false, "Street cannot begin with a space.")] //street begins with a space
        [DataRow("Street", "aaa ", false, "Street cannot end with a space.")] //street ends with a space
        [DataRow("Street", null, false, "Street must be specified.")] //street null
        [DataRow("StreetNumber", "a", false, "Street number must contain at least one number.")] //StreetNumber with no number
        [DataRow("StreetNumber", "1@aa", false, "Street number can only contain letters and numbers.")] //StreetNumber contains monkey
        [DataRow("StreetNumber", null, false, "Street number must be specified.")] //StreetNumber null
        [DataRow("FlatNumber", "a", false, "Flat number must contain at least one number.")] //FlatNumber with no number
        [DataRow("FlatNumber", "1@aa", false, "Flat number can only contain letters and numbers.")] //FlatNumber contains monkey
        [DataRow("FlatNumber", null, false, "Flat number must be specified.")] //FlatNumber null
        [DataRow("PostalCode", "-", false, "Postal code must match the 00-000 pattern.")] //PostalCode with no number
        [DataRow("PostalCode", "11", false, "Postal code must match the 00-000 pattern.")] //PostalCode with no thinker
        [DataRow("PostalCode", "1-1-1", false, "Postal code must match the 00-000 pattern.")] //PostalCode with 2 thinkers
        [DataRow("PostalCode", "1-", false, "Postal code must match the 00-000 pattern.")] //PostalCode ending with a thinker
        [DataRow("PostalCode", "-1", false, "Postal code must match the 00-000 pattern.")] //PostalCode starting with a thinker
        [DataRow("PostalCode", "1-1@1", false, "Postal code must match the 00-000 pattern.")] //PostalCode contains monkey
        [DataRow("PostalCode", null, false, "Postal code must be specified.")] //PostalCode null
        [DataRow("City", "abc", false, "City must contain at least 4 characters.")] //city too short
        [DataRow("City", "abcdef", false, "City must contain no more than 5 characters.")] //city too long
        [DataRow("City", "abc@", false, "City can only contain letters, spaces and thinkers.")] //city with a monkey
        [DataRow("City", " aaa", false, "City cannot begin with a space.")] //city begins with a space
        [DataRow("City", "aaa ", false, "City cannot end with a space.")] //city ends with a space
        [DataRow("City", null, false, "City must be specified.")] //city null
        public void ValidateAddress(string fieldname, string value, bool expectedValue, string expectedMessage)
        {
            //Arrange
            ContactInformation contactInformation = ValidContactInformation();
            contactInformation.Address[fieldname] = value;
            ContactInformationValidator contactInformationValidator = new(minStringLength, maxStringLength);
            //Act
            var result = contactInformationValidator.Validate(contactInformation);
            //Assert
            Assert.AreEqual(expectedValue, result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
    }
}
