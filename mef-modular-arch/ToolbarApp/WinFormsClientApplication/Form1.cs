using Base;
using Base.Command;
using System;
using System.Collections.Generic;
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
    [Export]
    public partial class Form1 : Form, IPartImportsSatisfiedNotification, IWindowHost
    {

        [ImportMany("Export", typeof(ToolStripMenuItem))]
        public IEnumerable<ToolStripMenuItem> ExportMenuItems { get; set; }

        [Import("navigationMenu")]
        public UserControl NavigationMenu { get; set; }

        [ImportMany]
        public IEnumerable<ToolStripMenuItem> PluginMenus { get; set; }

        [Import]
        public ICommandHandler CommandHandler { get; set; }

        [Import]
        public Base.Diagnostics.UndoRedoStack UndoRedoStackDiagnosticsWindow { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CommandHandler.OperationExecuted += (object s, OperationExecutionEventArgs ev) =>
                    {
                        undoToolStripMenuItem.Enabled = ev.HasUndoItems;
                        redoToolStripMenuItem.Enabled = ev.HasRedoItems;
                    };
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


        public void LoadWindow(Control control, Positions position)
        {
            if (position == Positions.Main)
            {
                flowLayoutPanelMain.Controls.Clear();
                flowLayoutPanelMain.Controls.Add(control);
            }
            else if(position == Positions.Navigation)
            {
                flowLayoutPanelWest.Controls.Clear();
                flowLayoutPanelWest.Controls.Add(control);
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandHandler.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandHandler.Redo();
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
