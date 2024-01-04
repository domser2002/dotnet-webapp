namespace Domain.Model;

public class User : Base
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string CompanyName { get; set; }
    public string Email { get; set; }
    public string Auth0Id { get; set; }
    public Address Address { get; set; }
    public Address DefaultSourceAddress { get; set; }
    public List<Inquiry> Inquiries { get; set; }

    public User()
    {
        Inquiries = new List<Inquiry>();
    }
    public object this[string fieldname]
    {
        set 
        {
            switch (fieldname)
            {
                case "FirstName":
                    this.FirstName = (string)value;
                    break;
                case "LastName":
                    this.LastName = (string)value;
                    break;
                case "CompanyName":
                    this.CompanyName= (string)value; 
                    break;
                case "Email":
                    this.Email = (string)value;
                    break;
                default:
                    return;
            }
        }
    }
}