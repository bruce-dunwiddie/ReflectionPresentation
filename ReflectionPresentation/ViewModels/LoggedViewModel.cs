using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ReflectionPresentation.Helpers;
using ReflectionPresentation.ViewModels.Commands;

namespace ReflectionPresentation.ViewModels
{
    public abstract class LoggedViewModel : ObservableObject
    {
        private string text = "";

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                RaisePropertyChanged();
            }
        }

        private Logger GetNewLog()
        {
            Text = "";

            Logger logger = new Logger();

            // buffering log calls to reduce the number of calls to RaisePropertyChanged() which was bogging down app
            ClearBuffer();
            logger.Log += (sender, message) => AddBufferedMessage(message + "\n");

            return logger;
        }

        protected ICommand GetLoggedCommand(Action<Logger> command)
        {
            return new DelegateCommand(() =>
            {
                Logger log = GetNewLog();

                try
                {
                    command(log);
                }
                catch (Exception ex)
                {
                    log.LogMessage(ex.ToString());
                }

                FlushBuffer();
            });
        }

        private StringBuilder buffer = new StringBuilder();

        private void AddBufferedMessage(string message)
        {
            buffer.Append(message);
        }

        private void FlushBuffer()
        {
            Text += buffer.ToString();
            ClearBuffer();
        }

        private void ClearBuffer()
        {
            buffer.Length = 0;
        }
    }
}
