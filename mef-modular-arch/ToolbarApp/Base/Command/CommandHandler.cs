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
                RaiseOperationExecuted();
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
            RaiseOperationExecuted();
        }

        public void Undo()
        {
            var command = stack.Undo();
            if (command == null)
            {
                throw new ApplicationException();
            }

            command.Undo();
            RaiseOperationExecuted();
        }

        public event EventHandler<OperationExecutionEventArgs> OperationExecuted;
        protected void RaiseOperationExecuted()
        {
            if (OperationExecuted != null)
            {
                OperationExecuted(this, new OperationExecutionEventArgs(stack.CanUndo, stack.CanRedo));
            }
        }

        public void CleanUp(IEnumerable<ICommand> ExecutedCommands)
        {
            stack.CleanUp(ExecutedCommands);
            RaiseOperationExecuted();
        }
    }
}
