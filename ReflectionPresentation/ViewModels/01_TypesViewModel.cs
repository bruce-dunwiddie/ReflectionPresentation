using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ReflectionPresentation.Types;
using ReflectionPresentation.ViewModels.Commands;

namespace ReflectionPresentation.ViewModels
{
    public class TypesViewModel : LoggedViewModel
    {
        public ICommand DisplayObjectGetType
        {
            get
            {
                return GetLoggedCommand(TypesExample.ObjectGetType);
            }
        }

        public ICommand DisplayAssemblyGetType
        {
            get
            {
                return GetLoggedCommand(TypesExample.AssemblyGetType);
            }
        }

        public ICommand DisplayTypeOf
        {
            get
            {
                return GetLoggedCommand(TypesExample.TypeOf);
            }
        }
    }
}
