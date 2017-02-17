using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ReflectionPresentation.Loading;
using ReflectionPresentation.ViewModels.Commands;

namespace ReflectionPresentation.ViewModels
{
    public class LoadingViewModel : LoggedViewModel
    {
        public ICommand LoadInSameDomain
        {
            get
            {
                return GetLoggedCommand(SameDomainLoad.Run);
            }
        }

        public ICommand LoadInNewDomain
        {
            get
            {
                return GetLoggedCommand(NewDomainLoad.Run);
            }
        }
    }
}
