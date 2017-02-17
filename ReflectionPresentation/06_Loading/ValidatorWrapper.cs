using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Invocation;

namespace ReflectionPresentation.Loading
{
    /// <summary>
    ///     This class removes the need for the implementor of a validator to
    ///     extend from MarshalByRefObject.
    /// </summary>
    public class ValidatorWrapper : MarshalByRefObject, IValidator
    {
        private IValidator validator = null;

        public ValidatorWrapper(IValidator validator)
        {
            this.validator = validator;
        }

        public bool IsValid(string input)
        {
            return validator.IsValid(input);
        }
    }
}
