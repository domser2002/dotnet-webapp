using System;
using System.Collections.Generic;
using System.Linq;
using AuthenticationService.Model;
using Domain.Model;

namespace Authenticator.Abstractions
{
    public interface IAuthenticationService
    {
        public User? Authenticate(IdleUser user);
    }
}
