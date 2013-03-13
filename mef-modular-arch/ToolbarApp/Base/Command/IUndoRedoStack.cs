using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Base.Command
{
    public interface IPublicUndoRedoStack<TItem>
    {
        TItem Undo();
        TItem Redo();
    }

    interface IUndoRedoStack<TItem> : IPublicUndoRedoStack<TItem>
    {

        void AddItem(TItem item);

        ReadOnlyCollection<TItem> UndoItems();
        ReadOnlyCollection<TItem> RedoItems();

        bool CanUndo { get; }
        bool CanRedo { get; }

        void CleanUp(IEnumerable<TItem> ExecutedCommands);

        event EventHandler<UndoRedoStackOperationEventArgs<TItem>> UndoRedoStackOperationExecuted;

    }
}
