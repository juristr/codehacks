using Base;
using Base.Command;
using Base.Events;
using ExportToGDrivePlugin.Service;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThreadOption = Prism4Winforms.Prism.Events.ThreadOption;

namespace ExportToGDrivePlugin
{
    [Export("Command_ExportGDrive", typeof(ICommand))]
    [Export(typeof(ICommand))]
    class ExportToGDriveCommand : ICommand
    {

        private IGoogleAuthService AuthService { get; set; }

        [ImportingConstructor]
        public ExportToGDriveCommand(IGoogleAuthService authService, IEventAggregator eventAggregator)
        {
            AuthService = authService;

            eventAggregator.GetEvent<StringEvent>().Subscribe(
                (msg) => MessageBox.Show(msg),
                ThreadOption.PublisherThread,
                true,
                (x) => x.ToUpper().Contains("HI"));
        }

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


        public void Undo()
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public string Description
        {
            get { return "Export to GDrive"; }
        }
    }
}
