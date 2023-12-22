using Authenticator.Abstractions;
using Domain.Abstractions;
using Domain.Model;

namespace AuthenticationService.Model
{
    public class OwnAuthentication : IAuthenticationService
    {
        IUserRepository repository;
        public OwnAuthentication(IUserRepository repository)
        {
            this.repository = repository;
        }
        public User? Authenticate(IdleUser idleuser) 
        {
            foreach(User user in repository.GetAll())
            {
                if(user.Email == idleuser.Email)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
