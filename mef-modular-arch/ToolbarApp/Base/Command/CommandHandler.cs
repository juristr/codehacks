using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Base.Command
{
    [Export(typeof(ICommandHandler))]
    public class CommandHandler : ICommandHandler
    {

        public void Execute(ICommand command)
        {
            throw new NotImplementedException();
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
