using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Invocation;
using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Loading
{
    public static class NewDomainLoad
    {
        public static void Run(Logger log)
        {
            AppDomain newAppDomain = AppDomain.CreateDomain("CustomValidator");

            Type proxyType = typeof(ValidatorFactory);

            // creates a proxy around a remote object to simulate the object's
            // definition and marshal method calls across the app domain boundaries
            ValidatorFactory proxy = (ValidatorFactory) newAppDomain.CreateInstanceFrom(
                proxyType.Assembly.Location,
                proxyType.FullName).Unwrap();

            // this call will be marshaled to the new app domain and the assembly
            // will be loaded in the new app domain and another proxy will be
            // returned for the IValidator instance
            
            IValidator validator = proxy.LoadValidatorRemote(
                @"../../../Build/CustomValidatorV1.dll");

            string password = "password123";

            log.LogMessage(
                password +
                ": " +
                validator.IsValid(password));

            int iterations = 50000;
            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                bool test = validator.IsValid(password);
            }

            timer.Stop();

            log.LogMessage("Remoted:");
            log.LogMessage(string.Format(
                "\t{0:#,##0} iter. in {1:#,##0.00} sec. at {2:#,##0} iter/sec.",
                iterations,
                timer.Elapsed.TotalSeconds,
                iterations / timer.Elapsed.TotalSeconds));

            // we can unload the new app domain, and therefore the assembly
            AppDomain.Unload(
                newAppDomain);

            AppDomain v2AppDomain = AppDomain.CreateDomain("CustomValidatorV2");

            ValidatorFactory v2proxy = (ValidatorFactory)v2AppDomain.CreateInstanceFrom(
                proxyType.Assembly.Location,
                proxyType.FullName).Unwrap();

            IValidator v2Validator = v2proxy.LoadValidatorRemote(
                @"../../../Build/CustomValidatorV2.dll");

            ShowLoadedValidatorAssemblies(log);

            log.LogMessage(
                password +
                ": " +
                v2Validator.IsValid(password));

            AppDomain.Unload(
                v2AppDomain);
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
