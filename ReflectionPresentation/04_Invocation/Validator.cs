using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionPresentation.Invocation
{
    public class Validator : IValidator
    {
        public bool IsValid(string input)
        {
            return input.Length > 8;
        }
    }
}
