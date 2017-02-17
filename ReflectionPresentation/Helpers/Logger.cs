using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionPresentation.Helpers
{
    public class Logger
    {
        public event EventHandler<String> Log;

        public void LogMessage(string message)
        {
            OnLogMessage(message);
        }

        protected void OnLogMessage(string message)
        {
            if (Log != null)
            {
                Log(this, message);
            }
        }
    }
}
