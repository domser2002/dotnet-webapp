using Domain.Model;
using System.Text.RegularExpressions;

namespace Frontend.Validators
{
    public static class GenericValidators
    {
        public static ValidationResults Dimension(float? length, float min, float max, string name)
        {
            if (length is null) return new ValidationResults($"Package's {name} must be speficied.");
            if (length <= min) return new ValidationResults($"Package's {name} must be greater than {min}.");
            if (length > max) return new ValidationResults($"Package's {name} must be lesser than {max}.");
            return new ValidationResults();
        }

        public static ValidationResults Name(string? name, int min, int max, string description)
        {
            if (name is null) return new ValidationResults($"{description} must be defined.");
            if (name.Length < min) return new ValidationResults($"{description} must contain at least {min} specified.");
            if (name.Length > max) return new ValidationResults($"{description} must contain no more than {max} characters.");
            if (!name.All(char.IsLetter)) return new ValidationResults($"{description} can only contain letters.");
            return new ValidationResults();
        }

        public static ValidationResults Email(string? email) 
        {
            if(email is null) return new ValidationResults("Email must be specified.");
            if (email.Count(x => x == '@') != 1) return new ValidationResults("Email must contain exactly one monkey.");
            if (!Regex.IsMatch(email, @"/^[A-Za-z0-9_!#$%&'*+\/=?`{|}~^.-]+@[A-Za-z0-9.-]+$/gm")) return new ValidationResults($"Enter a proper email address.");
            return new ValidationResults();
        }

        public static ValidationResults Address(Address? address)
        {
            if (address is null) return new ValidationResults("Address must be specified.");
            ValidationResults temp;
            string[] StreetComponents = address.Street.Split(' ', '-');
            if (StreetComponents.Length == 0) return new ValidationResults("Street name must contain letters.");
            if (StreetComponents.Length > 10) return new ValidationResults("Street name is too long.");
            foreach (string component in StreetComponents)
            {
                temp = Name(component, 1, 100, "Street name");
                if (!temp.Success) return temp;
            }
            if (address.StreetNumber.Length < 1) return new ValidationResults("Street number must be specified.");
            if (address.StreetNumber.Length > 100) return new ValidationResults("Street number must contain no more than 100 characters.");
            if (!address.StreetNumber.All(char.IsLetterOrDigit)) return new ValidationResults("Steet number can only contain letters or digits.");
            if (address.FlatNumber.Length < 1) return new ValidationResults("Flat number must be specified.");
            if (address.FlatNumber.Length > 100) return new ValidationResults("Flat number must contain no more than 100 characters.");
            if (!address.FlatNumber.All(char.IsLetterOrDigit)) return new ValidationResults("Flat number can only contain letters or digits.");
            if (address.PostalCode.Length < 1) return new ValidationResults("PostalCode must be specified.");
            if (address.PostalCode.Length > 100) return new ValidationResults("PostalCode number must contain no more than 100 characters.");
            string[] CityComponents = address.City.Split(' ', '-');
            if (CityComponents.Length == 0) return new ValidationResults("City name must contain letters.");
            if (CityComponents.Length > 10) return new ValidationResults("City name is too long.");
            foreach (string component in CityComponents)
            {
                temp = Name(component, 1, 100, "City name");
                if (!temp.Success) return temp;
            }
            return new ValidationResults();
        }

        public static ValidationResults Date(DateTime? date, string type)
        {
            if (date is null) return new ValidationResults($"{type} date must be specified.");
            if (DateTime.Compare((DateTime)date, DateTime.Now) < 0) return new ValidationResults($"{type} date must be set in the future");
            return new ValidationResults();
        }
    }
}
