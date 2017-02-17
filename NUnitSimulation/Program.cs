using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

namespace NUnitSimulation
{
    /// <summary>
    ///     Simplistic implementation of a console based NUnit test runner to demonstrate
    ///     reflection, construction, and invocation concepts.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // run without debugger attached

            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach (Type testClass in assembly.GetTypes()
                .Where(t => t.CustomAttributes.Any(
					a => a.AttributeType == typeof(TestFixtureAttribute))))
            {
                object testObject = Activator.CreateInstance(testClass);

                foreach (MethodInfo setup in testClass.GetMethods()
                    .Where(m => m.CustomAttributes.Any(
						a => a.AttributeType == typeof(OneTimeSetUpAttribute))))
                {
                    setup.Invoke(
                        obj: testObject, 
                        parameters: null);
                }

                foreach (MethodInfo test in testClass.GetMethods()
                    .Where(m => m.CustomAttributes.Any(
						a => a.AttributeType == typeof(TestAttribute))))
                {
                    Console.WriteLine("/***** " + test.Name + " *****/");

                    foreach (string description in test.CustomAttributes
                        .Where(a => a.AttributeType == typeof(DescriptionAttribute))
                        .Select(d => (string) d.ConstructorArguments[0].Value))
                    {
                        Console.WriteLine(description);
                    }

                    try
                    {
                        test.Invoke(
                            obj: testObject,
                            parameters: null);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Pass");
                        Console.ResetColor();
                    }
                    // reflection invocation wraps any exception inside it's own exception
                    catch (TargetInvocationException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Fail");
                        Console.ResetColor();

                        // unwrap to get exception that user would care about
                        Exception innerException = ex.InnerException;
                        Console.WriteLine(innerException.Message);
                        Console.WriteLine(innerException.StackTrace);
                    }

                    Console.WriteLine();
                }

                foreach (MethodInfo tearDown in testClass.GetMethods()
                    .Where(m => m.CustomAttributes.Any(
						a => a.AttributeType == typeof(OneTimeTearDownAttribute))))
                {
                    tearDown.Invoke(
                        obj: testObject,
                        parameters: null);
                }
            }

            Console.ReadLine();
        }
    }
}
