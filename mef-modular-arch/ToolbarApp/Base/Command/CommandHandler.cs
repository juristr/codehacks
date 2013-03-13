using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Base.Command
{

    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(ICommandHandler))]
    class CommandHandler : ICommandHandler
    {
        private IUndoRedoStack<ICommand> stack;
        private IList<ICommand> ExecutedCommands { get; set; }

        [ImportingConstructor]
        public CommandHandler(
            [Import(typeof(IUndoRedoStack<ICommand>))]
            IUndoRedoStack<ICommand> undoRedoHandler)
        {
            stack = undoRedoHandler;
            ExecutedCommands = new List<ICommand>();
        }

        public void Execute(ICommand command)
        {
            try
            {
                command.Execute();
                stack.AddItem(command);
                ExecutedCommands.Add(command);
            }
            catch
            {
                throw;
            }
        }

        public void Redo()
        {
            var command = stack.Redo();
            if (command == null)
            {
                throw new ApplicationException();
            }

            command.Execute();
        }

        public void Undo()
        {
            var command = stack.Undo();
            if (command == null)
            {
                throw new ApplicationException();
            }

            command.Undo();
        }

        public void CleanUp(IEnumerable<ICommand> ExecutedCommands)
        {
            stack.CleanUp(ExecutedCommands);
        }

        public void Dispose()
        {
            stack.CleanUp(ExecutedCommands);
        }
    }
}
