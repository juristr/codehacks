using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Base.Command
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(ICommandExecutionContext))]
    class CommandExecutionContext : ICommandExecutionContext
    {
        private ICommandHandler CommandHandler { get; set; }
        private IList<ICommand> ExecutedCommands { get; set; }

        [ImportingConstructor]
        public CommandExecutionContext(ICommandHandler commandHandler)
        {
            this.CommandHandler = commandHandler;
            ExecutedCommands = new List<ICommand>();
        }

        public void Execute(ICommand command)
        {
            try
            {
                CommandHandler.Execute(command);
                ExecutedCommands.Add(command);
            }
            catch (Exception e)
            {
                //log??
                Console.Error.WriteLine("ExecutionContext: " + e.Message);
            }
        }

        public void Dispose()
        {
            CommandHandler.CleanUp(ExecutedCommands);
        }
    }
}
