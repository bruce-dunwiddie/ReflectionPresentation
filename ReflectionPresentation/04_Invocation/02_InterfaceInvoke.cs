using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Invocation
{
    /// <summary>
    ///     Invoking through an Interface contract is much
    ///     faster than invoking dynamically through
    ///     MethodInfo. However, its speed is based on
    ///     making repeated calls to the same object.
    /// </summary>
    public static class InterfaceInvoke
    {
        public static void Run(Logger log)
        {
            // this could be a dynamically loaded type
            // or a static implementation
            IValidator validator = (IValidator)ValidatorFactory.GetValidator();

            string password = "password123";

            int iterations = 20000000;
            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                bool isValid = validator.IsValid(password);
            }

            timer.Stop();

            log.LogMessage(string.Format(
                "{0:#,##0} iter. in {1:#,##0.00} sec. at {2:#,##0} iter/sec.",
                iterations,
                timer.Elapsed.TotalSeconds,
                iterations / timer.Elapsed.TotalSeconds));
        }
    }
}
