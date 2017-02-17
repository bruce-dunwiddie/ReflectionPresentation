using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Types
{
    public class TypesExample
    {
        public static void ObjectGetType(Logger log)
        {
            // get a type at runtime from an instance of an object
            Type exampleClass = new TypesExample().GetType();

            log.LogMessage("new TypesExample().GetType().FullName:");
            log.LogMessage("");
            log.LogMessage("\t" + exampleClass.FullName);
        }

        public static void AssemblyGetType(Logger log)
        {
            // look at all types defined in an assembly and filter them
            Type exampleClass =
                Assembly.GetExecutingAssembly()
                .GetTypes()
                .Single(t => t.Name == "TypesExample");

            log.LogMessage("Assembly.GetExecutingAssembly()");
            log.LogMessage("\t.GetTypes()");
            log.LogMessage("\t.Single(t => t.Name == \"TypesExample\").FullName:");
            log.LogMessage("");
            log.LogMessage("\t" + exampleClass.FullName);
        }

        public static void TypeOf(Logger log)
        {
            // get a type definition at compile time
            Type exampleClass = typeof(TypesExample);

            log.LogMessage("typeof(TypesExample).FullName:");
            log.LogMessage("");
            log.LogMessage("\t" + exampleClass.FullName);
        }
    }
}
