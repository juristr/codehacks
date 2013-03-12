using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Base.Command
{

    public interface ICommandHandler : IDisposable
    {

        void Execute(ICommand command);
        void Redo();
        void Undo();

        bool CanUndo();
        bool CanRedo();

        event EventHandler<OperationExecutionEventArgs> OperationExecuted;


        void CleanUp(IEnumerable<ICommand> ExecutedCommands);
    }
}
