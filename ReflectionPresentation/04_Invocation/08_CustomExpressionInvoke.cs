using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Invocation
{
    /// <summary>
    ///     Compiling simpler Expression trees specific to the method
    ///     signature that you're calling instead of being dynamic
    ///     increases the speed.
    /// </summary>
    public static class CustomExpressionInvoke
    {
        public static void Run(Logger log)
        {
            object validator = ValidatorFactory.GetValidator();

            string password = "password123";

            MethodInfo isValidMethod = validator
                .GetType()
                .GetMethod("IsValid");

            Func<object, string, bool> callIsValid = GetFuncFromExpression<object, string, bool>(
                validator.GetType(),
                "IsValid");

            int iterations = 10000000;
            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                bool isValid = (bool)callIsValid(
                    validator,
                    password);
            }

            timer.Stop();

            log.LogMessage(string.Format(
                "{0:#,##0} iter. in {1:#,##0.00} sec. at {2:#,##0} iter/sec.",
                iterations,
                timer.Elapsed.TotalSeconds,
                iterations / timer.Elapsed.TotalSeconds));
        }

        private static Func<TObject, TParam, TReturn> GetFuncFromExpression<TObject, TParam, TReturn>(
            Type instanceType,
            string methodName)
        {
            ParameterExpression instance = Expression.Parameter(typeof(TObject), "i");

            ParameterExpression param = Expression.Parameter(typeof(TParam), "param");

            MethodInfo method = instanceType.GetMethod(methodName);

            Expression methodExp = Expression.Call(
                Expression.Convert(instance, method.DeclaringType),
                method,
                param);

            Expression<Func<TObject, TParam, TReturn>> methodCall = Expression.Lambda<Func<TObject, TParam, TReturn>>(
                methodExp,
                instance,
                param);

            Func<TObject, TParam, TReturn> func = methodCall.Compile();

            return func;
        }
    }
}
