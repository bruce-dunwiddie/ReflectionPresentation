using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NinjectSimulation.Interfaces;

namespace NinjectSimulation.Implementations
{
    public class MailSender : IMailSender
    {
        private readonly ILogging logging;

        public MailSender(ILogging logging)
        {
            this.logging = logging;
        }

        public void Send(string toAddress, string subject)
        {
            logging.Debug("Sending mail");
            Console.WriteLine("Sending mail to [{0}] with subject [{1}]", toAddress, subject);
        }
    }
}
