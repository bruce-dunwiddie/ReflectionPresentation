using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjectSimulation.Simulated
{
    public class SimulatedBindableContract
    {
        private Type contract;
        private SimulatedNinjectModule module;

        public SimulatedBindableContract(
            Type contract,
            SimulatedNinjectModule module)
        {
            this.contract = contract;
            this.module = module;
        }

        public void To<T>()
        {
            module.Bind(
                contract,
                typeof(T));
        }
    }
}
