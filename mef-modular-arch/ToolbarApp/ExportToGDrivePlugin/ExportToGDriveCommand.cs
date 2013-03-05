using Base;
using Base.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExportToGDrivePlugin
{
    [Export("Command_ExportGDrive", typeof(ICommand))]
    [Export(typeof(ICommand))]
    public class ExportToGDriveCommand : ICommand
    {
        public void Execute()
        {
            IValueProviderExtension valueProvider = Context as IValueProviderExtension;
            if (valueProvider != null && !String.IsNullOrEmpty(valueProvider.Value))
            {
                MessageBox.Show("Exporting value \"" + valueProvider.Value + "\" to Google Drive");
            }
            else
            {
                MessageBox.Show("Exporting to Google Drive");
            }
        }

        [Import(typeof(IValueProviderExtension))]
        public object Context { get; set; }
    }
}
