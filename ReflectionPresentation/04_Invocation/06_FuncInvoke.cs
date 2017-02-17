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
    ///     Invoking through a Func<> is much
    ///     faster than invoking dynamically through
    ///     MethodInfo, does not require
    ///     the object to implement a static Interface,
    ///     and does not require you to define a new
    ///     Delegate definition.
    ///     However, its speed is still based on
    ///     making repeated calls to the same object.
    /// </summary>
    public static class FuncInvoke
    {
        public static void Run(Logger log)
        {
            object validator = ValidatorFactory.GetValidator();

            string password = "password123";

            MethodInfo isValidMethod = validator
                .GetType()
                .GetMethod("IsValid");

            // only works if you can cache the call to a specific instance
            Func<string, bool> callIsValid = (Func<string, bool>)Delegate.CreateDelegate(
                typeof(Func<string, bool>),
                validator,
                isValidMethod);

            int iterations = 20000000;
            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                bool isValid = callIsValid(password);
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
