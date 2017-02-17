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
    ///     Using Expressions to invoke gives us the
    ///     flexibility of MethodInfo, but with a much
    ///     faster invoke.
    /// </summary>
    public static class ExpressionInvoke
    {
        public static void Run(Logger log)
        {
            object validator = ValidatorFactory.GetValidator();

            string password = "password123";

            MethodInfo isValidMethod = validator
                .GetType()
                .GetMethod("IsValid");

            Func<object, object[], object> callIsValid = GetFuncFromExpression(
                validator.GetType(),
                "IsValid");

            int iterations = 10000000;
            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                bool isValid = (bool)callIsValid(
                    validator,
                    new object[] { password });
            }

            timer.Stop();

            log.LogMessage(string.Format(
                "{0:#,##0} iter. in {1:#,##0.00} sec. at {2:#,##0} iter/sec.",
                iterations,
                timer.Elapsed.TotalSeconds,
                iterations / timer.Elapsed.TotalSeconds));
        }

        private static Func<object, object[], object> GetFuncFromExpression(
            Type instanceType,
            string methodName)
        {
            MethodInfo method = instanceType.GetMethod(methodName);

            ParameterExpression instance = Expression.Parameter(typeof(object), "i");

            ParameterExpression allParameters = Expression.Parameter(typeof(object[]), "params");

            ParameterInfo[] methodParameters = method.GetParameters();

            List<Expression> parameters = new List<Expression>();

            for (int i = 0; i < methodParameters.Length; i++)
            {
                ParameterInfo parameter = methodParameters[i];

                ConstantExpression indexExpr = Expression.Constant(i);

                BinaryExpression item = Expression.ArrayIndex(
                    allParameters,
                    indexExpr);

                UnaryExpression converted = Expression.Convert(
                    item,
                    parameter.ParameterType);

                parameters.Add(converted);
            }

            Expression methodExp = Expression.Call(
                Expression.Convert(instance, method.DeclaringType),
                method,
                parameters.ToArray());

            // http://stackoverflow.com/questions/8974837/expression-of-type-system-datetime-cannot-be-used-for-return-type-system-obje
            if (methodExp.Type.IsValueType)
            {
                methodExp = Expression.Convert(methodExp, typeof(object));
            }

            Expression<Func<object, object[], object>> methodCall = Expression.Lambda<Func<object, object[], object>>(
                methodExp,
                instance,
                allParameters);

            Func<object, object[], object> func = methodCall.Compile();

            return func;
        }
    }
}
