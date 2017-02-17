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
    ///     MethodInfo allows you to invoke any method
    ///     by any name, with any signature.
    /// </summary>
    public static class MethodInfoInvoke
    {
        public static void Run(Logger log)
        {
            object validator = ValidatorFactory.GetValidator();

            string password = "password123";

            int iterations = 2000000;
            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                MethodInfo isValidMethod = validator
                    .GetType()
                    .GetMethod("IsValid");

                bool isValid = (bool)isValidMethod
                    .Invoke(validator, new object[] { password });
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
