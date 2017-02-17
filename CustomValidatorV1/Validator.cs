using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Invocation;

namespace CustomValidator
{
    public class Validator : IValidator
    {
        public bool IsValid(string input)
        {
            return input.Length > 8;
        }
    }
}
