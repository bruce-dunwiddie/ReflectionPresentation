using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjectSimulation.Simulated
{
    public abstract class SimulatedNinjectModule
    {
        public SimulatedNinjectModule()
        {
            Registrations = new Dictionary<Type, Type>();
        }

        public Dictionary<Type, Type> Registrations { get; set; }

        public abstract void Load();

        public void Bind(
            Type contract,
            Type implementation)
        {
            if (!contract.IsAssignableFrom(implementation))
            {
                throw new Exception("Unable to bind type " + contract + " to type " + implementation + ".");
            }

            Registrations.Add(
                contract,
                implementation);
        }

        public SimulatedBindableContract Bind<T>()
        {
            return new SimulatedBindableContract(
                typeof(T),
                this);
        }        
    }
}
