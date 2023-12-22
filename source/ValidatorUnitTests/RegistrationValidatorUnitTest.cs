using Domain.Model;
using Frontend.Validators;

namespace ValidatorUnitTests
{
    [TestClass]
    public class UnitTestRegistrationValidator
    {
        readonly int minlength = 4;
        readonly int maxlength = 5;
        public static Address ValidAddress()
        {
            return new Address()
            {
                Street="aaaa",
                StreetNumber="1a",
                FlatNumber="1a",
                PostalCode="00-000",
                City="aaaa"
            };
        }
        public static User ValidUser()
        {
            return new User()
            {
                FirstName = "aaaa",
                LastName = "aaaa",
                Email = "aa@aaa.aa",
                CompanyName = "aaaa",
                DefaultSourceAddress = ValidAddress(),
                Address = ValidAddress(),
                Inquiries = new()
            };
        }
        [TestMethod]
        [DataRow("","",true,"")] //valid user
        [DataRow("FirstName","abc", false, "First name must contain at least 4 characters.")] //first name too short
        [DataRow("FirstName","abcdef", false, "First name must contain no more than 5 characters.")] //first name too long
        [DataRow("FirstName",null, false, "First name must be specified.")] //first name null
        [DataRow("LastName", "abc", false, "Last name must contain at least 4 characters.")] //last name too short
        [DataRow("LastName", "abcdef", false, "Last name must contain no more than 5 characters.")] //last name too long
        [DataRow("LastName", null, false, "Last name must be specified.")] //last name null
        [DataRow("Email","aaa.bbb",false,"Email must contain exactly one monkey.")] //email with no monkey
        [DataRow("Email","aa@aa@aa.bb",false,"Email must contain exactly one monkey.")] //email with two monkeys
        [DataRow("Email","aa@bbb",false,"Email must contain a dot.")] //email with no dot
        [DataRow("Email", "@aa.bb", false, "Email must contain at least one character in prefix.")] //email with no letters in prefix
        [DataRow("Email", "a.a@", false, "Email must contain at least one character in domain.")] //email with no letters in domain
        [DataRow("Email",null,false,"Email must be specified.")] //email null
        [DataRow("CompanyName", "abc", false, "Company name must contain at least 4 characters.")] //company name to short
        [DataRow("CompanyName", "abcdef", false, "Company name must contain no more than 5 characters.")] //company name too long
        [DataRow("CompanyName", null, false, "Company name must be specified.")] //comapny name null
        public void ValidateUser(string fieldname,string value, bool expectedValue, string expectedMessage)
        {
            //Arrange
            User user = ValidUser();
            user[fieldname] = value;
            RegistrationValidator registrationValidator = new(minlength, maxlength);
            //Act
            var result = registrationValidator.Validate(user);
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
        [DataRow("PostalCode", "-",false, "Postal code must match the 00-000 pattern.")] //PostalCode with no number
        [DataRow("PostalCode", "11",false, "Postal code must match the 00-000 pattern.")] //PostalCode with no thinker
        [DataRow("PostalCode", "1-1-1",false, "Postal code must match the 00-000 pattern.")] //PostalCode with 2 thinkers
        [DataRow("PostalCode", "1-",false, "Postal code must match the 00-000 pattern.")] //PostalCode ending with a thinker
        [DataRow("PostalCode", "-1",false, "Postal code must match the 00-000 pattern.")] //PostalCode starting with a thinker
        [DataRow("PostalCode", "1-1@1",false, "Postal code must match the 00-000 pattern.")] //PostalCode contains monkey
        [DataRow("PostalCode", null,false, "Postal code must be specified.")] //PostalCode null
        [DataRow("City", "abc", false, "City must contain at least 4 characters.")] //city too short
        [DataRow("City", "abcdef", false, "City must contain no more than 5 characters.")] //city too long
        [DataRow("City", "abc@", false, "City can only contain letters, spaces and thinkers.")] //city with a monkey
        [DataRow("City", " aaa", false, "City cannot begin with a space.")] //city begins with a space
        [DataRow("City", "aaa ", false, "City cannot end with a space.")] //city ends with a space
        [DataRow("City", null, false, "City must be specified.")] //city null
        public void ValidateAddress(string fieldname,string value,bool expectedValue,string expectedMessage)
        {
            //Arrange
            User user=ValidUser();
            user.DefaultSourceAddress[fieldname] = value;
            RegistrationValidator registrationValidator = new(minlength, maxlength);
            //Act
            var result = registrationValidator.Validate(user);
            //Assert
            Assert.AreEqual(expectedValue, result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
    }
}