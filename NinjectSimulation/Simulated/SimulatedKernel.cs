using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NinjectSimulation.Simulated
{
    public class SimulatedKernel
    {
        private Dictionary<Type, Type> registrations = new Dictionary<Type, Type>();

        /// <summary>
        ///     Instantiates and aggregates all registrations from all modules in assembly.
        /// </summary>
        public void Load(Assembly assembly)
        {
            foreach (Type module in assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(SimulatedNinjectModule))))
            {
                SimulatedNinjectModule moduleInstance = (SimulatedNinjectModule) Activator.CreateInstance(module);
                moduleInstance.Load();
                foreach (KeyValuePair<Type, Type> registration in moduleInstance.Registrations)
                {
                    registrations[registration.Key] = registration.Value;
                }
            }
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public object Get(Type contract)
        {
            if (registrations.ContainsKey(contract))
            {
                return Get(registrations[contract]);
            }

            if (
                contract.IsInterface ||
                contract.IsAbstract)
            {
                return null;
            }

            // look for a default public constructor

            // does not include private constructors by default
            if (contract.GetConstructors()
                .Count(c => c.GetParameters().Count() == 0) > 0)
            {
                return Activator.CreateInstance(contract);
            }

            // there'd better only be one public constructor at this point
            ConstructorInfo constructor = contract.GetConstructors().SingleOrDefault();

            if (constructor == null)
            {
                return null;
            }

            List<object> parameters = new List<object>();

            foreach (ParameterInfo param in constructor.GetParameters())
            {
                parameters.Add(
                    Get(
                        param.ParameterType));
            }

            return constructor.Invoke(parameters.ToArray());
        }
    }
}
