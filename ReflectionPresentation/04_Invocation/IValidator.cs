using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionPresentation.Invocation
{
    public interface IValidator
    {
        bool IsValid(string input);
    }
}
