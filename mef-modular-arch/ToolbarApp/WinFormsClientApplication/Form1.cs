using Base;
using Base.Command;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsClientApplication
{

    [Export(typeof(IWindowHost))]
    [Export(typeof(Form))]
    public partial class Form1 : Form, IPartImportsSatisfiedNotification, IWindowHost
    {

        [ImportMany("Export", typeof(ToolStripMenuItem))]
        public IEnumerable<ToolStripMenuItem> ExportMenuItems { get; set; }

        [ImportMany]
        public IEnumerable<ToolStripMenuItem> PluginMenus { get; set; }

        //[ImportMany(typeof(IUndoRedoStack<ICommand>))]
        //public IList<IUndoRedoStack<ICommand>> CommandStack { get; set; }

        [Import(typeof(IUndoRedoStack<ICommand>))]
        public IUndoRedoStack<ICommand> CommandStack { get; set; }

        [Import]
        public Base.Diagnostics.UndoRedoStack UndoRedoStackDiagnosticsWindow { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CommandStack.UndoRedoStackOperationExecuted += CommandStack_UndoRedoStackOperationExecuted;
        }

        void CommandStack_UndoRedoStackOperationExecuted(object sender, UndoRedoStackOperationEventArgs<ICommand> e)
        {
            undoToolStripMenuItem.Enabled = e.HasUndoItems;
            redoToolStripMenuItem.Enabled = e.HasRedoItems;
        }

        public void OnImportsSatisfied()
        {
            LoadMenus(exportToolStripMenuItem, ExportMenuItems);
            LoadMenus(pluginsToolStripMenuItem, PluginMenus);
        }

        private void LoadMenus(ToolStripMenuItem parentNode, IEnumerable<ToolStripMenuItem> menuItems)
        {
            //menu extensions
            if (menuItems.Count() > 0)
            {
                foreach (var exportMenuItem in menuItems)
                {
                    parentNode.DropDownItems.Add(exportMenuItem);
                }
            }
            else
            {
                parentNode.Enabled = false;
            }
        }


        public void LoadWindow(Control control)
        {
            var controls = flowLayoutPanelMain.Controls;
            foreach (Control ctrl in controls)
            {
                flowLayoutPanelMain.Controls.Remove(ctrl);
            }

            flowLayoutPanelMain.Controls.Clear();
            flowLayoutPanelMain.Controls.Add(control);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandStack.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandStack.Redo();
        }

        private void openUndoRedoStackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoRedoStackDiagnosticsWindow.Show();   
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controls = flowLayoutPanelMain.Controls;
            foreach (Control ctrl in controls)
            {
                flowLayoutPanelMain.Controls.Remove(ctrl);
                ctrl.Dispose();                
            }
        }
    }
}
