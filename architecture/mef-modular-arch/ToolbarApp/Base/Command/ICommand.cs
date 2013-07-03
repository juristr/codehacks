using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.Command
{
    public interface ICommand
    {
        //specifies whether a command can be undone...for certain operations this might not be possible
        //bool CanUndo { get; }

        string Description { get; }

        object Context { get; set; }

        void Execute();
        void Undo();

    }
}
