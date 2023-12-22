using Domain.Model;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Frontend.Validators
{
    public static class GenericValidators
    {
        public static ValidationResults Dimension(float? length, float min, float max, string name)
        {
            if (length is null) return new ValidationResults($"Package's {name} must be specified.");
            if (length <= min) return new ValidationResults($"Package's {name} must be greater than {min}.");
            if (length > max) return new ValidationResults($"Package's {name} must be less than {max}.");
            return new ValidationResults();
        }

        public static ValidationResults String(string? name, int min, int max, string description)
        {
            if (name is null) return new ValidationResults($"{description} must be specified.");
            if (name.Length < min) return new ValidationResults($"{description} must contain at least {min} characters.");
            if (name.Length > max) return new ValidationResults($"{description} must contain no more than {max} characters.");
            return new ValidationResults();
        }

        public static ValidationResults Code(string? name, string description)
        {
            if (name is null) return new ValidationResults($"{description} must be specified.");
            if (!name.All(char.IsLetterOrDigit)) return new ValidationResults($"{description} can only contain letters and numbers.");
            if (name.All(char.IsLetter)) return new ValidationResults($"{description} must contain at least one number.");
            return new ValidationResults();
        }

        public static ValidationResults Name(string? name, int min, int max, string description)
        {
            ValidationResults result = String(name, min, max, description);
            if (!result.Success) return result;
            if (!name!.All(char.IsLetter)) return new ValidationResults($"{description} can only contain letters.");
            return result;
        }

        public static ValidationResults Email(string? email) 
        {
            if(email is null) return new ValidationResults("Email must be specified.");
            if (email.Count(x => x == '@') != 1) return new ValidationResults("Email must contain exactly one monkey.");
            if (email[0] == '@') return new ValidationResults("Email must contain at least one character in prefix.");
            if (email[email.Length - 1] == '@') return new ValidationResults("Email must contain at least one character in domain.");
            string[] mailParts = email.Split('@');
            if (!mailParts[1].Contains('.')) return new ValidationResults("Email must contain a dot.");
            return new ValidationResults();
        }

        public static ValidationResults Address(Address? address, int min, int max)
        {
            if (address is null) return new ValidationResults("Address must be specified.");
            ValidationResults temp;
            if (address.Street is null) return new ValidationResults("Street must be specified.");
            temp = String(address.Street, min, max, "Street");
            if (!temp.Success) return temp;
            if (address.Street[0] == ' ') return new ValidationResults("Street cannot begin with a space.");
            if (address.Street[address.Street.Length - 1] == ' ') return new ValidationResults("Street cannot end with a space.");
            string[] StreetComponents = address.Street.Split(' ', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0');
            if (StreetComponents.Length == 0) return new ValidationResults("Street must contain letters.");
            foreach (string component in StreetComponents) if (!component.All(char.IsLetter)) return new ValidationResults("Street can only contain letters, numbers and spaces.");
            temp = Code(address.StreetNumber, "Street number");
            if (!temp.Success) return temp;
            temp = Code(address.FlatNumber, "Flat number");
            if (!temp.Success) return temp;
            if (address.PostalCode is null) return new ValidationResults("Postal code must be specified.");
            if (!Regex.IsMatch(address.PostalCode, "^[0-9][0-9]-[0-9][0-9][0-9]$")) return new ValidationResults("Postal code must match the 00-000 pattern.");
            temp = String(address.City, min, max, "City");
            if (!temp.Success) return temp;
            if (address.City[0] == ' ') return new ValidationResults("City cannot begin with a space.");
            if (address.City[address.City.Length - 1] == ' ') return new ValidationResults("City cannot end with a space.");
            string[] CityComponents = address.City.Split(' ', '-');
            if (CityComponents.Length == 0) return new ValidationResults("City must contain letters.");
            foreach (string component in CityComponents) if (!component.All(char.IsLetter)) return new ValidationResults("City can only contain letters, spaces and thinkers.");
            return new ValidationResults();
        }

        public static ValidationResults Date(DateTime? date, string type)
        {
            if (date is null) return new ValidationResults($"{type} date must be specified.");
            if (DateTime.Compare((DateTime)date, DateTime.Now) < 0) return new ValidationResults($"{type} date must be set in the future.");
            return new ValidationResults();
        }
    }
}
