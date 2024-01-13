using Domain.Model;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        List<User> GetAll();
        void AddUser(User user);
        void AddRequest(string userID, Request request);
    }
}