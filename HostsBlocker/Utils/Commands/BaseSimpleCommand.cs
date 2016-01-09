using System;
using System.Windows.Input;

namespace ScreenSaver.Main.Utils.Commands
{
    public abstract class BaseSimpleCommand : ICommand
    {
        private readonly Func<bool> canExecute;

        protected BaseSimpleCommand(Func<bool> canExecute)
        {
            if (canExecute == null)
            {
                throw new ArgumentException("canExecute must be set");
            }

            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}