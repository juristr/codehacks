using Base.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Base.Diagnostics
{

    [Export]
    public partial class UndoRedoStack : Form
    {
        private BindingList<ICommand> undoStack;
        private BindingList<ICommand> redoStack;

        [Import]
        IUndoRedoStack<ICommand> Stack { get; set; }

        [Import]
        IUndoRedoStack<ICommand> CommandHander { get; set; }

        public UndoRedoStack()
        {
            InitializeComponent();
        }

        private void UndoRedoStack_Load(object sender, EventArgs e)
        {
            this.undoStack = new BindingList<ICommand>(Stack.UndoItems());
            this.redoStack = new BindingList<ICommand>(Stack.RedoItems());

            bindingSourceUndoStack.DataSource = undoStack;
            bindingSourceRedoStack.DataSource = redoStack;

            CommandHander.UndoRedoStackOperationExecuted += (s, ev) =>
                        {
                            this.undoStack.ResetBindings();
                            this.redoStack.ResetBindings();
                        };

            //Stack.ItemAdded += (s, ev) => { RefreshBindings(); };
            //Stack.ItemUndone += (s, ev) => { RefreshBindings(); };
            //Stack.ItemRedone += (s, ev) => { RefreshBindings(); };

            //CommandHander.OperationExecuted += (s, ev) =>
            //                    {
            //                        this.undoStack.ResetBindings();
            //                        this.redoStack.ResetBindings();
            //                    };
        }

        private void RefreshBindings()
        {
            this.undoStack.ResetBindings();
            this.redoStack.ResetBindings();
        }
    }
}
