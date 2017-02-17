using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Introspection
{
    public static class TypeDisplay
    {
        public static void Display(Logger log, Type typeToDisplay)
        {
            log.LogMessage("Name: " + typeToDisplay.Name);
            log.LogMessage("Pretty Name: " + TypeDisplayHelper.GetPrettyName(typeToDisplay));
			log.LogMessage("Namespace: " + typeToDisplay.Namespace);
			log.LogMessage("FullName: " + typeToDisplay.FullName);
			log.LogMessage("BaseType: " + typeToDisplay.BaseType.FullName);
			log.LogMessage("");

			log.LogMessage("Attributes: " + typeToDisplay.Attributes);
            log.LogMessage("");

			if (typeToDisplay.GetInterfaces().Any())
			{
				Type[] interfaces = typeToDisplay.GetInterfaces();
				log.LogMessage("Implements: " +
                    string.Join(", ", interfaces.Select(i => TypeDisplayHelper.GetPrettyName(i))));
                log.LogMessage("");
			}

			log.LogMessage("IsClass: " + typeToDisplay.IsClass);
			log.LogMessage("IsInterface: " + typeToDisplay.IsInterface);
			log.LogMessage("IsEnum: " + typeToDisplay.IsEnum);
			log.LogMessage("IsPrimitive: " + typeToDisplay.IsPrimitive);
			log.LogMessage("IsArray: " + typeToDisplay.IsArray);
            log.LogMessage("");

			log.LogMessage("IsPublic: " + typeToDisplay.IsPublic);
			log.LogMessage("IsAbstract: " + typeToDisplay.IsAbstract);
            log.LogMessage("");

			log.LogMessage("IsGenericType: " + typeToDisplay.IsGenericType);
			if (typeToDisplay.IsGenericType)
			{
				Type[] arguments = typeToDisplay.GetGenericArguments();
				log.LogMessage("Generic Arguments: " +
                    string.Join(", ", arguments.Select(a => TypeDisplayHelper.GetPrettyName(a))));
			}
		}
	}
}
