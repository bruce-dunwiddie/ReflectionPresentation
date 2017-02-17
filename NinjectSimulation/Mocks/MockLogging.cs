using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NinjectSimulation.Interfaces;

namespace NinjectSimulation.Mocks
{
    public class MockLogging : ILogging
    {
        public void Debug(string message)
        {
            Console.WriteLine("Debug: " + message);
        }
    }
}
