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

        private UndoRedoView Control { get; set; }
        
        [Import]
        public ExportFactory<UndoRedoView> View { get; set; }

        [Import]
        public IWindowHost WindowHost { get; set; }

        [ImportingConstructor]
        public OpenUndoRedoMenu()
            : base("Open Undo/Redo Demo")
        {
            
        }

        protected override void OnClick(EventArgs e)
        {
            if (Control == null || Control.IsDisposed)
            {
                var viewExportLifetimeCtx = View.CreateExport();
                Control = viewExportLifetimeCtx.Value;
            }

            WindowHost.LoadWindow(Control);
        }
    }
}
