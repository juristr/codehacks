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
                RaiseOperationExecuted(command, ExecutionOperation.Execute);
            }
            catch
            {
                throw;
            }
        }

        public void Redo(int numberOfRedos = 1)
        {
            for (int i = 0; i < numberOfRedos; i++)
            {
                var command = stack.Redo();
                if (command == null)
                {
                    throw new ApplicationException();
                }

                command.Execute();
                RaiseOperationExecuted(command, ExecutionOperation.Redo);
            }
        }

        public void Undo(int numberOfUndos = 1)
        {
            for (int i = 0; i < numberOfUndos; i++)
            {
                var command = stack.Undo();
                if (command == null)
                {
                    throw new ApplicationException();
                }

                command.Undo();
                RaiseOperationExecuted(command, ExecutionOperation.Undo);
            }
        }

        public event EventHandler<OperationExecutionEventArgs> OperationExecuted;
        protected void RaiseOperationExecuted(ICommand item, ExecutionOperation operation)
        {
            if (OperationExecuted != null)
            {
                OperationExecuted(this, new OperationExecutionEventArgs(item, operation, stack.CanUndo, stack.CanRedo));
            }
        }

        public void CleanUp(IEnumerable<ICommand> ExecutedCommands)
        {
            stack.CleanUp(ExecutedCommands);
            RaiseOperationExecuted(null, ExecutionOperation.Cleanup);
        }
    }

}
