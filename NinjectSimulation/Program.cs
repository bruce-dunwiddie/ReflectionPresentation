using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using NinjectSimulation.Interfaces;
using NinjectSimulation.Simulated;

namespace NinjectSimulation
{
    /// <summary>
    ///     Simplistic simulation of Ninject type registration, lookup, and dependency injection functionality
    ///     to demonstrate reflection concepts.
    ///     
    ///     Using classes from Ninject tutorial at
    ///     http://blog.agilistic.nl/a-step-by-step-guide-to-using-ninject-for-dependancy-injection-in-c-sharp/
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new SimulatedKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var mailSender = kernel.Get<IMailSender>();

            var formHandler = new FormHandler(mailSender);
            formHandler.Handle("test@test.com");

            Console.ReadLine();
        }
    }
}
