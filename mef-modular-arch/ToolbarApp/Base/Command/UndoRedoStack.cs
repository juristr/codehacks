using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Base.Command
{
    [Export(typeof(IPublicUndoRedoStack<ICommand>))]
    [Export(typeof(IUndoRedoStack<ICommand>))]
    class UndoRedoStack<TItem> : IUndoRedoStack<TItem>
    {
        public int _MaxStackSize = 20;
        public int MaxStackSize
        {
            get { return _MaxStackSize; }
            set { _MaxStackSize = value; }
        }

        private List<TItem> UndoStack { get; set; }
        //private Stack<TItem> UndoStack { get; set; }
        private List<TItem> RedoStack { get; set; }

        public UndoRedoStack()
        {
            UndoStack = new List<TItem>();
            RedoStack = new List<TItem>();
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
            return UndoStack.AsReadOnly();
        }

        public ReadOnlyCollection<TItem> RedoItems()
        {
            return RedoStack.AsReadOnly();
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
        public static TItem Pop<TItem>(this List<TItem> list)
        {
            var lastElement = list.Last();
            list.Remove(lastElement);
            return lastElement;
        }
    }
}
