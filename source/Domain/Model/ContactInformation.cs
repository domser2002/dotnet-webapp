namespace Domain.Model;

public class ContactInformation : Base
{
    public string PersonalData { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
    public ContactInformation() { }
    public ContactInformation(User user)
    {
        PersonalData = user.FullName;
        Email = user.Email;
        Address = user.Address;
    }

    public object this[string fieldname]
    {
        set
        {
            switch(fieldname)
            {
                case "PersonalData":
                    this.PersonalData = (string)value; 
                    break;
                case "Email":
                    this.Email = (string)value;
                    break;
                case "Address":
                    this.Address = (Address)value;
                    break;
            }
        }
    }
}
