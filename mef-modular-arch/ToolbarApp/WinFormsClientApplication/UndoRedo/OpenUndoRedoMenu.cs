using Base;
using Base.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormsClientApplication.UndoRedo;

namespace WinFormsClientApplication.Export
{
    [Export(typeof(ToolStripMenuItem))]
    public class OpenUndoRedoMenu : ToolStripMenuItem
    {
        private UserControl undoRedoView;

        [Import]
        public IWindowHost WindowHost { get; set; }

        [ImportingConstructor]
        public OpenUndoRedoMenu(
            [Import(typeof(UndoRedoView))]UserControl undoRedoView)
            : base("Open Undo/Redo Demo")
        {
            this.undoRedoView = undoRedoView;
        }

        protected override void OnClick(EventArgs e)
        {
            WindowHost.LoadWindow(undoRedoView);
        }
    }
}
