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
        [Import(typeof(UndoRedoView))]
        public UserControl UndoRedoView { get; set; }

        [Import]
        public IWindowHost WindowHost { get; set; }

        [ImportingConstructor]
        public OpenUndoRedoMenu()
            : base("Open Undo/Redo Demo")
        {
            
        }

        protected override void OnClick(EventArgs e)
        {
            WindowHost.LoadWindow(UndoRedoView);
        }
    }
}
