using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Introspection
{
	public static class AssemblyDisplay
	{
        public static void Display(Logger log, Assembly assemblyToDisplay)
		{
            log.LogMessage("Name: " + assemblyToDisplay.GetName().Name);
            log.LogMessage("");
            log.LogMessage("FullName: " + assemblyToDisplay.FullName);
            log.LogMessage("");
            log.LogMessage("Version: " + assemblyToDisplay.GetName().Version);
            log.LogMessage("");
            log.LogMessage("Location: " + assemblyToDisplay.Location);
            log.LogMessage("");
            log.LogMessage("Atributes: " + assemblyToDisplay.CustomAttributes);
        }
	}
}
