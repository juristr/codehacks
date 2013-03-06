using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.Command
{
    class UndoRedoStack<TItem> : IUndoRedoStack<TItem>
    {
        private Stack<TItem> UndoStack { get; set; }
        private Stack<TItem> RedoStack { get; set; }

        public UndoRedoStack()
        {
            UndoStack = new Stack<TItem>();
            RedoStack = new Stack<TItem>();
        }

        public void AddItem(TItem item)
        {
            UndoStack.Push(item);
        }

        public TItem Undo()
        {
            var item =  UndoStack.Pop();
            RedoStack.Push(item);
            return item;
        }

        public TItem Redo()
        {
            var item = RedoStack.Pop();
            UndoStack.Push(item);
            return item;
        }

        public IEnumerable<TItem> UndoItems()
        {
            return UndoStack.ToList();
        }

        public IEnumerable<TItem> RedoItems()
        {
            return RedoStack.ToList();
        }

    }
}
