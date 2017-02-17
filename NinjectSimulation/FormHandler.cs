using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NinjectSimulation.Interfaces;

namespace NinjectSimulation
{
    public class FormHandler
    {
        private readonly IMailSender mailSender;

        public FormHandler(IMailSender mailSender)
        {
            this.mailSender = mailSender;
        }

        public void Handle(string toAddress)
        {
            mailSender.Send(toAddress, "This is non-Ninject example");
        }
    }
}
