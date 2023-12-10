using Domain.Model;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        List<User> GetAll();
        void AddUser(User user);
        void AddOffer(int id, int offerID);
    }
}