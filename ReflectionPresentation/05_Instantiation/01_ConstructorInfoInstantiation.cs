using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Instantiation
{
    public static class ConstructorInfoInstantiation
    {
        public static void Run(Logger log, Type typeToConstruct)
        {
            object createdObj = Create(typeToConstruct);
            
            log.LogMessage(
                typeof(ConstructorInfoInstantiation).Name +
                " created instance of " +
                TypeDisplayHelper.GetPrettyName(typeToConstruct));
        }

        private static object Create(Type typeToConstruct)
        {
            // find the default parameterless constructor
            ConstructorInfo constructor = typeToConstruct
                .GetConstructors()
                .Single(c => c.GetParameters().Length == 0);

            return constructor.Invoke(null);
        }
    }
}
