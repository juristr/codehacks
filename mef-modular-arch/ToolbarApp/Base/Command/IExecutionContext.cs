using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.Command
{
    public interface IExecutionContext : IDisposable
    {

        void Execute(ICommand command);

    }
}
