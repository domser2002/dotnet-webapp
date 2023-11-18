using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Validators.Abstractions
{
    public interface IRegistrationValidator : IValidator
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // probably not a good idea, will do something about it later
        public Domain.Model.Address Address {  get; set; }
        public Domain.Model.Address DefaultSourceAddress { get; set; }
    }
}
