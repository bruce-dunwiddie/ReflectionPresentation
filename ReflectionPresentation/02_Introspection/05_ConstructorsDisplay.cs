using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Introspection
{
    public static class ConstructorsDisplay
    {
        public static void Display(Logger log, Type typeToDisplay)
        {
            log.LogMessage("Name: " + TypeDisplayHelper.GetPrettyName(typeToDisplay));

            ConstructorInfo[] publicConstructors = typeToDisplay.GetConstructors(
                    BindingFlags.Instance |
                    BindingFlags.Public);

            if (publicConstructors.Any())
            {
                log.LogMessage("Public Constructors:");
                foreach (ConstructorInfo constructor in publicConstructors)
                {
                    log.LogMessage("\t" + constructor.Name);
                    log.LogMessage("\t(");

                    ParameterInfo[] parameters = constructor.GetParameters();
                    log.LogMessage(
                        "\t\t" +
                        string.Join(", ", parameters.Select(p => TypeDisplayHelper.GetPrettyName(p.ParameterType))));

                    log.LogMessage("\t)");
                    log.LogMessage("");
                }
            }

            ConstructorInfo[] privateConstructors = typeToDisplay.GetConstructors(
                    BindingFlags.Instance |
                    BindingFlags.NonPublic);

            if (privateConstructors.Any())
            {
                log.LogMessage("Non Public Constructors:");
                foreach (ConstructorInfo constructor in privateConstructors)
                {
                    log.LogMessage("\t" + constructor.Name);

                    ParameterInfo[] parameters = constructor.GetParameters();
                    log.LogMessage(
                        "\t\t" +
                        string.Join(", ", parameters.Select(p => TypeDisplayHelper.GetPrettyName(p.ParameterType))));

                    log.LogMessage("\t)");
                    log.LogMessage("");
                }
            }
        }
    }
}
