using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NinjectSimulation.Interfaces;

namespace NinjectSimulation.Mocks
{
    public class MockMailSender : IMailSender
    {
        public void Send(string toAddress, string subject)
        {
            Console.WriteLine("Mocking mail to [{0}] with subject [{1}]", toAddress, subject);
        }
    }
}
