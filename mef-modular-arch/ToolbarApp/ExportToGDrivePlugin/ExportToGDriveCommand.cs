﻿using Base;
using Base.Command;
using ExportToGDrivePlugin.Service;
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
    class ExportToGDriveCommand : ICommand
    {

        private IGoogleAuthService AuthService { get; set; }

        [ImportingConstructor]
        public ExportToGDriveCommand(IGoogleAuthService authService)
        {
            AuthService = authService;
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
