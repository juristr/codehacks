using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.Command
{
    interface IUndoRedoStack<TItem>
    {

        void AddItem(TItem item);
        TItem Undo();
        TItem Redo();

        IEnumerable<TItem> UndoItems();
        IEnumerable<TItem> RedoItems();

    }
}
