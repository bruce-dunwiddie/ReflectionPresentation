using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Manipulation
{
    public static class GetProperties
    {
        public static void Run(Logger log, User user)
        {
            Type typeToDisplay = typeof(User);

            PropertyInfo[] publicProperties = typeToDisplay.GetProperties(
                    BindingFlags.Instance |
                    BindingFlags.Public);

            if (publicProperties.Any())
            {
                log.LogMessage("Public Properties:");
                foreach (PropertyInfo property in publicProperties)
                {
                    ParameterInfo[] parameters = property.GetIndexParameters();
                    if (!parameters.Any())
                    {
                        log.LogMessage(
                            "\t" + 
                            TypeDisplayHelper.GetPrettyName(property.PropertyType) + 
                            " : " + 
                            property.Name +
                            " = " +
                            property.GetValue(user).ToString());
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
                    if (!parameters.Any())
                    {
                        log.LogMessage(
                            "\t" +
                            TypeDisplayHelper.GetPrettyName(property.PropertyType) +
                            " : " +
                            property.Name +
                            " = " +
                            property.GetValue(user).ToString());
                    }

                    log.LogMessage("");
                }
            }
        }
    }
}
