using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Manipulation
{
    public static class SetProperties
    {
        public static void Run(Logger log, User user)
        {
            // we can't set the ID because of the private access modifier

            // this line won't compile
            // user.ID = Guid.NewGuid();

            // or can we?
            Type userType = typeof(User);

            PropertyInfo idSetter = userType.GetProperty("ID");

            idSetter.SetValue(
                user,
                Guid.NewGuid());

            log.LogMessage("ID = " + user.ID.ToString());
        }
    }
}
