using Domain.Model;

namespace Domain.Abstractions
{
    public interface IContactInformationRepository
    {
        List<ContactInformation> GetAll();  
        void AddContactInformation(ContactInformation contactInformation);
    }
}
