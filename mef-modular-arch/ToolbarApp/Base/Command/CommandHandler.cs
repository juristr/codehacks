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

        public CommandHandler(IUndoRedoStack<ICommand> undoRedoHandler)
        {
            stack = undoRedoHandler;
        }

        public void Execute(ICommand command)
        {
            command.Execute();
        }

        public void Redo(ICommand command)
        {
            throw new NotImplementedException();
        }

        public void Undo(ICommand command)
        {
            throw new NotImplementedException();
        }
    }
}
