using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Base.Command
{

    public interface ICommandHandler
    {

        void Execute(ICommand command);
        void Redo(ICommand command);
        void Undo(ICommand command);

    }
}
