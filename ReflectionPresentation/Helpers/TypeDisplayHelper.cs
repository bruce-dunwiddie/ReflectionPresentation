using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionPresentation.Helpers
{
    public class TypeDisplayHelper
    {
        public static string GetPrettyName(Type type)
        {
            if (type.IsGenericType)
            {
                Type[] genericTypes = type.GetGenericArguments();

                string name = type.Name;

                // trim `1
                if (name.IndexOf("`") > -1)
                {
                    name = name.Substring(0, name.LastIndexOf("`"));
                }

                name = name.Replace("+", ".")
                    + "<"
                    // recursion
                    + String.Join(", ", genericTypes.Select(t =>
                    {
                        if (t.IsGenericParameter)
                        {
                            return t.Name;
                        }
                        else
                        {
                            return GetPrettyName(t);
                        }
                    }).ToArray())
                    + ">";

                return name;
            }

            return type.Name.Replace("+", ".");
        }
    }
}
