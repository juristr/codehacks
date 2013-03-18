using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Base.Command
{

    [Export(typeof(IUndoRedoStack<>))]
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
        }

        public TItem Undo()
        {
            TItem item = default(TItem);
            if (CanUndo)
            {
                item = UndoStack.Pop();
                RedoStack.Add(item);
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
        }
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
