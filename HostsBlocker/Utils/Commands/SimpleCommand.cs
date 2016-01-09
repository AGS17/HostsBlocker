using System;

namespace ScreenSaver.Main.Utils.Commands
{
    public class SimpleCommand : BaseSimpleCommand
    {
        private readonly Action executed;

        public SimpleCommand(Action executed, Func<bool> canExecute) : base(canExecute)
        {
            if (executed == null)
            {
                throw new ArgumentException("executed must be set");
            }

            this.executed = executed;
        }

        public SimpleCommand(Action executed)
            : this(executed, () => true)
        {
        }

        public override void Execute(object parameter)
        {
            this.executed();
        }
    }
}
