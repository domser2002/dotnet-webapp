using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Validators.Abstractions
{
    public interface IContactInformationValidator : IValidator
    {
        public string PersonalData { get; set; }
        public string Email { get; set; }
        public Domain.Model.Address Address { get; set; }
    }
}
