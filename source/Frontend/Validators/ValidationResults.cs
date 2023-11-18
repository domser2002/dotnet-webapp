using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Validators
{
    public struct ValidationResults
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ValidationResults(string message) { Message = message; }
    }
}
