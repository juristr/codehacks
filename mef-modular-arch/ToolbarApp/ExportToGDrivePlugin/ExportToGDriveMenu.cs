using Base.Command;
using ExportToGDrivePlugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsClientApplication.Export
{
    [Export("Export", typeof(ToolStripMenuItem))]
    public class ExportToGDriveMenu : ToolStripMenuItem
    {
        [Import("Command_ExportGDrive")]
        public ICommand Command { get; set; }

        public ExportToGDriveMenu()
            : base("Export to GDrive")
        {
        }

        protected override void OnClick(EventArgs e)
        {
            Command.Execute();
        }
    }
}
