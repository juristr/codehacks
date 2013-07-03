using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Base.Command
{

    public interface ICommandHandler
    {

        void Execute(ICommand command);
        void Redo(int numberOfRedos = 1);
        void Undo(int numberOfUndoes = 1);

        event EventHandler<OperationExecutionEventArgs> OperationExecuted;

        void CleanUp(IEnumerable<ICommand> ExecutedCommands);
    }
}
