using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Base;

namespace ExportPlugin.Views
{
    [Export(typeof(IPluginPart))]
    public partial class ExportView : UserControl, IPluginPart
    {
        public ExportView()
        {
            InitializeComponent();
        }

        public UserControl View
        {
            get { return this; }
        }
    }
}
