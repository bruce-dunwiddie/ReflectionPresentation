using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using ReflectionPresentation.Invocation;
using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Loading
{
    public static class SameDomainLoad
    {
        public static void Run(Logger log)
        {
            ShowLoadedValidatorAssemblies(log);

            ValidatorFactory factory = new ValidatorFactory();

            // note that once we load an assembly into an app domain
            // there is no way to unload that assembly from that app domain

            // we don't need to care what the type actually is,
            // what it's named, or what the implementation is,
            // only that it implements the correct interface,
            // which means the instance can be directly cast

            IValidator validator = factory.LoadValidatorLocal(
                @"../../../Build/CustomValidatorV1.dll");

            string password = "password123";

            log.LogMessage(
                password +
                ": " +
                validator.IsValid(password));

            int iterations = 20000000;
            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                bool test = validator.IsValid(password);
            }

            timer.Stop();

            log.LogMessage("Local:");
            log.LogMessage(string.Format(
                "\t{0:#,##0} iter. in {1:#,##0.00} sec. at {2:#,##0} iter/sec.",
                iterations,
                timer.Elapsed.TotalSeconds,
                iterations / timer.Elapsed.TotalSeconds));

            ShowLoadedValidatorAssemblies(log);
        }

        private static void ShowLoadedValidatorAssemblies(Logger log)
        {
            log.LogMessage("Loaded Validator Assemblies:");

            AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains("Validator"))
                .ToList()
                .ForEach(a => log.LogMessage("\t" + a.FullName));
        }
    }
}
