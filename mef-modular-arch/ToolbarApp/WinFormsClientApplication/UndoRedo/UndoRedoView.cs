using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Base.Command;
using WinFormsClientApplication.UndoRedo.Commands;

namespace WinFormsClientApplication.UndoRedo
{
    [Export(typeof(UndoRedoView))]
    partial class UndoRedoView : UserControl
    {
        private IList<string> SourceItems = new BindingList<string>
                {
                    "Paul", "Fritz", "Heinz", "Sepp"
                };
        private IList<string> DestinationItems = new BindingList<string>();

        [Import]
        public ICommandHandler CommandHandler { get; set; }

        public UndoRedoView()
        {
            InitializeComponent();

            bindingSourceList.DataSource = SourceItems;
            bindingDestinationList.DataSource = DestinationItems;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CommandHandler.OperationExecuted += (object s, OperationExecutionEventArgs ev) =>
                            {
                                buttonUndo.Enabled = ev.CanUndo;
                                buttonRedo.Enabled = ev.CanRedo;
                            };
        }

        private void buttonMoveToDest_Click(object sender, EventArgs e)
        {
            var selectedItem = listBoxSource.SelectedItem;

            var command = new MoveCommand(SourceItems, DestinationItems, selectedItem as string);
            CommandHandler.Execute(command);
        }

        private void buttonMoveToSource_Click(object sender, EventArgs e)
        {
            var selectedItem = listBoxDestination.SelectedItem;

            var command = new MoveCommand(DestinationItems, SourceItems, selectedItem as string);

            CommandHandler.Execute(command);
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            CommandHandler.Undo();
        }

        private void buttonRedo_Click(object sender, EventArgs e)
        {
            CommandHandler.Redo();
        }
    }
}
