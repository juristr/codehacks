using Base.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace WinFormsClientApplication.Export
{
    [Export("Command_ExportCSV", typeof(ICommand))]
    [Export(typeof(ICommand))]
    public class ExportCsvCommand : ICommand
    {
        public void Execute()
        {
            //normally I'd call here some service to do the job
            MessageBox.Show("Exporting as CSV");
        }
    }
}
