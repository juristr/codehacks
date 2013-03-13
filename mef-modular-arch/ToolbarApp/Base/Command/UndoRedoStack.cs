using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Base.Command
{
    //[Export(typeof(IPublicUndoRedoStack<ICommand>))]
    [Export(typeof(IUndoRedoStack<ICommand>))]
    class UndoRedoStack<TItem> : IUndoRedoStack<TItem>
    {
        public int _MaxStackSize = 20;
        public int MaxStackSize
        {
            get { return _MaxStackSize; }
            set { _MaxStackSize = value; }
        }

        private readonly List<TItem> UndoStack;
        private readonly ReadOnlyCollection<TItem> ReadOnlyUndoStack;
        private readonly List<TItem> RedoStack;
        private readonly ReadOnlyCollection<TItem> ReadOnlyRedoStack;

        public UndoRedoStack()
        {
            UndoStack = new List<TItem>();
            RedoStack = new List<TItem>();

            ReadOnlyUndoStack = new ReadOnlyCollection<TItem>(UndoStack);
            ReadOnlyRedoStack = new ReadOnlyCollection<TItem>(RedoStack);
        }

        public void AddItem(TItem item)
        {
            if (UndoStack.Count() >= MaxStackSize)
            {
                UndoStack.RemoveAt(0);
            }

            UndoStack.Add(item);
            RaiseUndoRedoStackOperationExecuted(item, UndoStackOperation.Added);
        }

        public TItem Undo()
        {
            TItem item = default(TItem);
            if (CanUndo)
            {
                item = UndoStack.Pop();
                RedoStack.Add(item);
                RaiseUndoRedoStackOperationExecuted(item, UndoStackOperation.Undone);
            }

            return item;
        }

        public TItem Redo()
        {
            TItem item = default(TItem);
            if (CanRedo)
            {
                item = RedoStack.Pop();
                UndoStack.Add(item);
                RaiseUndoRedoStackOperationExecuted(item, UndoStackOperation.Redone);
            }

            return item;
        }

        public ReadOnlyCollection<TItem> UndoItems()
        {
            return ReadOnlyUndoStack;
        }

        public ReadOnlyCollection<TItem> RedoItems()
        {
            return ReadOnlyRedoStack;
        }

        public bool CanUndo
        {
            get { return UndoStack.Count > 0; }
        }

        public bool CanRedo
        {
            get { return RedoStack.Count > 0; }
        }

        public void CleanUp(IEnumerable<TItem> ExecutedCommands)
        {
            foreach (var cmd in ExecutedCommands)
            {
                UndoStack.Remove(cmd);
                RedoStack.Remove(cmd);
            }

            RaiseUndoRedoStackOperationExecuted(default(TItem), UndoStackOperation.Cleanup);
        }

        public event EventHandler<UndoRedoStackOperationEventArgs<TItem>> UndoRedoStackOperationExecuted;
        protected void RaiseUndoRedoStackOperationExecuted(TItem item, UndoStackOperation operation)
        {
            if (UndoRedoStackOperationExecuted != null)
            {
                UndoRedoStackOperationExecuted(this, new UndoRedoStackOperationEventArgs<TItem>(item, operation, CanUndo, CanRedo));
            }
        }

    }

    public class UndoRedoStackOperationEventArgs<TItem> : EventArgs
    {
        public TItem CurrentItem { get; private set; }
        public bool HasUndoItems { get; private set; }
        public bool HasRedoItems { get; private set; }
        public UndoStackOperation Action { get; private set; }

        public UndoRedoStackOperationEventArgs(TItem item, UndoStackOperation action, bool hasUndoItems, bool hasRedoItems)
            : base()
        {
            CurrentItem = item;
            Action = action;
            HasUndoItems = hasUndoItems;
            HasRedoItems = hasRedoItems;
        }
    }

    public enum UndoStackOperation
    {
        Added,
        Undone,
        Redone,
        Cleanup
    }

    static class ListStackExtension
    {
        public static TItem Pop<TItem>(this IList<TItem> list)
        {
            var lastElement = list.Last();
            list.Remove(lastElement);
            return lastElement;
        }
    }
}
