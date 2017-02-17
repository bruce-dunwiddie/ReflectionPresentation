using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Invocation
{
    public static class DynamicTypeInvoke
    {
        public static void Run(Logger log)
        {
dynamic validator = ValidatorFactory.GetValidator();

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
