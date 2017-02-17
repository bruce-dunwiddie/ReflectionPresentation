using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Introspection
{
	public static class MethodsDisplay
	{
        public static void Display(Logger log, Type typeToDisplay)
		{
			log.LogMessage("Name: " + TypeDisplayHelper.GetPrettyName(typeToDisplay));

            MethodInfo[] publicMethods = typeToDisplay.GetMethods(
                BindingFlags.Instance |
                BindingFlags.Public)
                .Where(m => !m.IsSpecialName)
                .OrderBy(m => m.Name)
                .ToArray();

            if (publicMethods.Any())
            {
                log.LogMessage("Public Methods:");
                foreach (MethodInfo method in publicMethods)
                {
                    log.LogMessage("\t" + TypeDisplayHelper.GetPrettyName(method.ReturnType) + " " + method.Name);
                    log.LogMessage("\t(");

                    ParameterInfo[] parameters = method.GetParameters();
                    log.LogMessage(
                        "\t\t" +
                        string.Join(", ", parameters.Select(p => TypeDisplayHelper.GetPrettyName(p.ParameterType))));

                    log.LogMessage("\t)");
                    log.LogMessage("");
                }
            }

            MethodInfo[] privateMethods = typeToDisplay.GetMethods(
                BindingFlags.Instance |
                BindingFlags.NonPublic)
                .Where(m => !m.IsSpecialName)
                .OrderBy(m => m.Name)
                .ToArray();

            if (privateMethods.Any())
            {
                log.LogMessage("Non Public Methods:");
                foreach (MethodInfo method in privateMethods)
                {
                    log.LogMessage("\t" + TypeDisplayHelper.GetPrettyName(method.ReturnType) + " " + method.Name);
                    log.LogMessage("\t(");

                    ParameterInfo[] parameters = method.GetParameters();
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
