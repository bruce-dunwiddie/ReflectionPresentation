using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ReflectionPresentation.Instantiation;
using ReflectionPresentation.ViewModels.Commands;

namespace ReflectionPresentation.ViewModels
{
    public class InstantiationViewModel : LoggedViewModel
    {
        public ICommand InvokeConstructor
        {
            get
            {
                return GetLoggedCommand((log) => ConstructorInfoInstantiation.Run(log, typeof(List<string>)));
            }
        }

        public ICommand CreateInstance
        {
            get
            {
                return GetLoggedCommand((log) => ActivatorInstantiation.Run(log, typeof(List<string>)));
            }
        }
    }
}
