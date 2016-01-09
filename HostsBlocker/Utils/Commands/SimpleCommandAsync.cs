using System;
using System.Threading.Tasks;

namespace ScreenSaver.Main.Utils.Commands
{
    public class SimpleCommandAsync : BaseSimpleCommand
    {
        private readonly Func<Task> executed;

        public SimpleCommandAsync(Func<Task> executed, Func<bool> canExecute) : base(canExecute)
        {
            if (executed == null)
            {
                throw new ArgumentException("executed must be set");
            }

            this.executed = executed;
        }

        public SimpleCommandAsync(Func<Task> executed)
            : this(executed, () => true)
        {
        }

        public async override void Execute(object parameter)
        {
            await executed();
        }
    }
}