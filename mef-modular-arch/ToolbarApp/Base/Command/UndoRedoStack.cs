using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Base.Command
{
    [Export(typeof(IUndoRedoStack<ICommand>))]
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
            TItem item = default(TItem);
            if(CanUndo){
                item = UndoStack.Pop();
                RedoStack.Push(item);
            }
            
            return item;
        }

        public TItem Redo()
        {
            TItem item = default(TItem);
            if(CanRedo){
                item = RedoStack.Pop();
                UndoStack.Push(item);
            }

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

        public bool CanUndo
        {
            get { return UndoStack.Count > 0; }
        }

        public bool CanRedo
        {
            get { return RedoStack.Count > 0; }
        }
    }
}
