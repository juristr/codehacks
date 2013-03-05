﻿using Base.Command;
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
            MessageBox.Show("Exporting to Google Drive");
        }
    }
}
