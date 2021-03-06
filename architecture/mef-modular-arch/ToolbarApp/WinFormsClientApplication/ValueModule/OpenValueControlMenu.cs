﻿using Base;
using Base.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormsClientApplication.UndoRedo;
using WinFormsClientApplication.ValueModule;

namespace WinFormsClientApplication.Export
{
    [Export(typeof(ToolStripMenuItem))]
    public class OpenValueControlMenu : ToolStripMenuItem
    {

        private ValueControl Control { get; set; }

        [Import]
        public ExportFactory<ValueControl> View { get; set; }

        [Import]
        public IWindowHost WindowHost { get; set; }

        [ImportingConstructor]
        public OpenValueControlMenu()
            : base("Open Value Control View")
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
