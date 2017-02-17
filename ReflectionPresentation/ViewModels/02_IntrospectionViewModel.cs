using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ReflectionPresentation.Introspection;
using ReflectionPresentation.ViewModels.Commands;

namespace ReflectionPresentation.ViewModels
{
    public class IntrospectionViewModel : LoggedViewModel
    {
        public ICommand DisplayType
        {
            get
            {
                return GetLoggedCommand((log) => TypeDisplay.Display(log, typeof(List<string>)));
            }
        }

        public ICommand DisplayFields
        {
            get
            {
                return GetLoggedCommand((log) => FieldsDisplay.Display(log, typeof(List<string>)));
            }
        }

        public ICommand DisplayProperties
        {
            get
            {
                return GetLoggedCommand((log) => PropertiesDisplay.Display(log, typeof(List<string>)));
            }
        }

        public ICommand DisplayMethods
        {
            get
            {
                return GetLoggedCommand((log) => MethodsDisplay.Display(log, typeof(List<string>)));
            }
        }

        public ICommand DisplayConstructors
        {
            get
            {
                return GetLoggedCommand((log) => ConstructorsDisplay.Display(log, typeof(List<string>)));
            }
        }

        public ICommand DisplayAssembly
        {
            get
            {
                return GetLoggedCommand((log) => AssemblyDisplay.Display(log, typeof(List<string>).Assembly));
            }
        }
    }
}
