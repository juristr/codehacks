using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base
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
}
