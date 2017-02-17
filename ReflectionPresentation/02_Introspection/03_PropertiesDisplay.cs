using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Introspection
{
	public static class PropertiesDisplay
	{
        public static void Display(Logger log, Type typeToDisplay)
		{
            log.LogMessage("Name: " + TypeDisplayHelper.GetPrettyName(typeToDisplay));

            PropertyInfo[] publicProperties = typeToDisplay.GetProperties(
                    BindingFlags.Instance |
                    BindingFlags.Public);

            if (publicProperties.Any())
            {
                log.LogMessage("Public Properties:");
                foreach (PropertyInfo property in publicProperties)
                {
                    ParameterInfo[] parameters = property.GetIndexParameters();
                    if (parameters.Any())
                    {
                        log.LogMessage("\t" + TypeDisplayHelper.GetPrettyName(property.PropertyType) + " : " + property.Name);
                        log.LogMessage("\t[");
                        
                        log.LogMessage(
                            "\t\t" +
                            string.Join(", ", parameters.Select(p => TypeDisplayHelper.GetPrettyName(p.ParameterType))));
                        log.LogMessage("\t]");
                    }
                    else
                    {
                        log.LogMessage("\t" + TypeDisplayHelper.GetPrettyName(property.PropertyType) + " : " + property.Name);
                    }

                    log.LogMessage("");
                }
            }

            PropertyInfo[] privateProperties = typeToDisplay.GetProperties(
                    BindingFlags.Instance |
                    BindingFlags.NonPublic);

            if (privateProperties.Any())
            {
                log.LogMessage("Non Public Properties:");
                foreach (PropertyInfo property in privateProperties)
                {
                    ParameterInfo[] parameters = property.GetIndexParameters();
                    if (parameters.Any())
                    {
                        log.LogMessage("\t" + TypeDisplayHelper.GetPrettyName(property.PropertyType) + " : " + property.Name);
                        log.LogMessage("\t[");

                        log.LogMessage(
                            "\t\t" +
                            string.Join(", ", parameters.Select(p => TypeDisplayHelper.GetPrettyName(p.ParameterType))));
                        log.LogMessage("\t]");
                    }
                    else
                    {
                        log.LogMessage("\t" + TypeDisplayHelper.GetPrettyName(property.PropertyType) + " : " + property.Name);
                    }

                    log.LogMessage("");
                }
            }
		}
	}
}
