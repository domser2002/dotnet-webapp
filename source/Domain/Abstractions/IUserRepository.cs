using Domain.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        List<User> GetAll();
        void AddUser(User user);
        void AddRequest(string userID, Request request);
        void PatchByID(string userID, UserPatchModel user);
    }
}