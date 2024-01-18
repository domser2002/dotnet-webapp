using Domain.Model;

namespace Domain.Abstractions
{
    public interface IContactInformationRepository
    {
        List<ContactInformation> GetAll();  
        int AddContactInformation(ContactInformation contactInformation);
    }
}
