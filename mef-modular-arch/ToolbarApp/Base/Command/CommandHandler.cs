using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Base.Command
{
    [Export(typeof(ICommandHandler))]
    class CommandHandler : ICommandHandler
    {
        private IUndoRedoStack<ICommand> stack;

        [ImportingConstructor]
        public CommandHandler(
            [Import(typeof(IUndoRedoStack<ICommand>))]
            IUndoRedoStack<ICommand> undoRedoHandler)
        {
            stack = undoRedoHandler;
        }

        public void Execute(ICommand command)
        {
            try
            {
                command.Execute();
                stack.AddItem(command);
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
    }
}
