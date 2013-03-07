using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.Command
{
    public interface ICommand
    {
        object Context { get; set; }
        void Execute();

        void Undo();
    }
}
