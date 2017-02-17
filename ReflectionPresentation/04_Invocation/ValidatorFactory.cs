using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionPresentation.Invocation
{
    public class ValidatorFactory
    {
        public static object GetValidator()
        {
            Type dynamicType = Assembly
                .GetExecutingAssembly()
                .GetType("ReflectionPresentation.Invocation.Validator");

            object validator = Activator.CreateInstance(dynamicType);

            return validator;
        }
    }
}
