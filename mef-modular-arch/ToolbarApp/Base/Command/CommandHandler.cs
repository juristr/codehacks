using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Base.Command
{

    public class OperationExecutionEventArgs : EventArgs
    {
        public OperationExecutionEventArgs(bool canUndo, bool canRedo)
        {
            CanUndo = canUndo;
            CanRedo = canRedo;
        }

        public bool CanUndo { get; private set; }
        public bool CanRedo { get; private set; }
    }

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

        public bool CanUndo()
        {
            return stack.CanUndo;
        }

        public bool CanRedo()
        {
            return stack.CanRedo;
        }

        public event EventHandler<OperationExecutionEventArgs> OperationExecuted;
        protected void RaiseOperationExecuted()
        {
            if (OperationExecuted != null)
            {
                OperationExecuted(this, new OperationExecutionEventArgs(stack.CanUndo, stack.CanRedo));
            }
        }

    }
}
