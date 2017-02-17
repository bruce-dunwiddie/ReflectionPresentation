using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Invocation;

namespace ReflectionPresentation.Loading
{
    /// <summary>
    ///     Class definition that will be shared across app domains
    ///     Extending from MarshalByRefObject
    ///     ensures this code will only be executed inside the original AppDomain
    ///     and will be proxied if referenced by another AppDomain.
    /// </summary>
    public class ValidatorFactory : MarshalByRefObject
    {
        public IValidator LoadValidatorLocal(
            string pathToAssembly)
        {
            Console.WriteLine("Executing factory in domain: " + AppDomain.CurrentDomain.FriendlyName);

            Assembly assembly = Assembly.LoadFrom(
                pathToAssembly);

            // since custom assembly is referencing this assembly
            // we can check for equality on IValidator directly

            // making the assumption that there is only a single implementation
            // in the custom assembly

            Type customValidatorType = assembly.GetTypes().Single(
                t => t.GetInterfaces().Where(
                    i => i == typeof(IValidator)).Any());

            IValidator validator =
                (IValidator)Activator.CreateInstance(customValidatorType);

            return validator;
        }

        public IValidator LoadValidatorRemote(
            string pathToAssembly)
        {
            IValidator validator = LoadValidatorLocal(
                pathToAssembly);

            IValidator wrapper = new ValidatorWrapper(validator);

            return wrapper;
        }
    }
}
