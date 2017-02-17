using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NinjectSimulation.Implementations;
using NinjectSimulation.Interfaces;
using NinjectSimulation.Mocks;
using NinjectSimulation.Simulated;

namespace NinjectSimulation
{
    public class Bindings : SimulatedNinjectModule
    {
        public override void Load()
        {
            Bind<IMailSender>().To<MailSender>();
            Bind<ILogging>().To<MockLogging>();
        }
    }
}
