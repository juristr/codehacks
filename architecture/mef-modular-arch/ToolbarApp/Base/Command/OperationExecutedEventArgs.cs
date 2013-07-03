using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.Command;

namespace Base.Command
{
    public class OperationExecutionEventArgs : EventArgs
    {
        public ICommand CurrentItem { get; private set; }
        public bool HasUndoItems { get; private set; }
        public bool HasRedoItems { get; private set; }
        public ExecutionOperation Action { get; private set; }

        public OperationExecutionEventArgs(ICommand item, ExecutionOperation action, bool hasUndoItems, bool hasRedoItems)
            : base()
        {
            CurrentItem = item;
            Action = action;
            HasUndoItems = hasUndoItems;
            HasRedoItems = hasRedoItems;
        }
    }

    public enum ExecutionOperation
    {
        Execute,
        Undo,
        Redo,
        Cleanup
    }
}
