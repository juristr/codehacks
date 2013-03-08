using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Base.Command
{
    interface IUndoRedoStack<TItem>
    {

        void AddItem(TItem item);
        TItem Undo();
        TItem Redo();

        ReadOnlyCollection<TItem> UndoItems();
        ReadOnlyCollection<TItem> RedoItems();

        bool CanUndo { get; }
        bool CanRedo { get; }


        void CleanUp(IEnumerable<TItem> ExecutedCommands);
    }
}
