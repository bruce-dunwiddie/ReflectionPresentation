using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReflectionPresentation.ViewModels.Commands
{
    public class DelegateCommand : ICommand
    {
#pragma warning disable 0067

        public event EventHandler CanExecuteChanged;

#pragma warning restore 0067

        private Action action;

        public DelegateCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
