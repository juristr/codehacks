using Base.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Base;

namespace WinFormsClientApplication.Export
{
    [Export("Command_ExportCSV", typeof(ICommand))]
    [Export(typeof(ICommand))]
    class ExportCsvCommand : ICommand
    {
        public void Execute()
        {
            IValueProviderExtension valueProvider = Context as IValueProviderExtension;
            if (valueProvider != null && !String.IsNullOrEmpty(valueProvider.Value))
            {
                MessageBox.Show("Exporting value \"" + valueProvider.Value + "\" as CSV");
            }
            else
            {
                MessageBox.Show("Exporting as CSV");
            }
        }

        [Import(typeof(IValueProviderExtension))]
        public object Context { get; set; }


        public void Undo()
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }
    }
}
