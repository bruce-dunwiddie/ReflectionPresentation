using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ReflectionPresentation.Manipulation;
using ReflectionPresentation.ViewModels.Commands;

namespace ReflectionPresentation.ViewModels
{
    public class ManipulationViewModel : LoggedViewModel
    {
        private User user = new User()
            {
                // The property or indexer 'ReflectionPresentation.Manipulation.User.ID' cannot be used
                // in this context because the set accessor is inaccessible
                // ID = Guid.NewGuid(),

                Name = "Bruce"
            };

        public ICommand DisplayPropertyValues
        {
            get
            {
                return GetLoggedCommand((log) => GetProperties.Run(log, user));
            }
        }

        public ICommand ChangePropertyValues
        {
            get
            {
                return GetLoggedCommand((log) => SetProperties.Run(log, user));
            }
        }
    }
}
