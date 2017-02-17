using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Instantiation
{
    public static class ActivatorInstantiation
    {
        public static void Run(Logger log, Type typeToConstruct)
        {
            object createdObj = Create(typeToConstruct);

            log.LogMessage(
                typeof(ActivatorInstantiation).Name +
                " created instance of " +
                TypeDisplayHelper.GetPrettyName(typeToConstruct));
        }

        private static object Create(Type typeToConstruct)
        {
            return Activator.CreateInstance(typeToConstruct);

            // there's also Activator.CreateInstance<T>()
        }
    }
}
