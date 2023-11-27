namespace Domain.Model;

public class ContactInformation : Base
{
    public string PersonalData { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
}
