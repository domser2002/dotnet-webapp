namespace Domain.Model;

public class User : Base
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string CompanyName { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
    public Address DefaultSourceAddress { get; set; }
    public List<int> Inquiries { get; set; }

    public User()
    {
        Inquiries = new List<int>();
    }
}