using Base;
using Base.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsClientApplication.Export
{
    [Export("Export", typeof(ToolStripMenuItem))]
    public class ExportCsvMenu : ToolStripMenuItem
    {

        [Import("Command_ExportCSV")]
        public ICommand Command { get; set; }

        public ExportCsvMenu() : base("Export as CSV")
        {
            
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            Command.Execute();
        }
    }
}
