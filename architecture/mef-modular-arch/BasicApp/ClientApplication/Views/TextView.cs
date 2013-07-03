using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Base;
using System.ComponentModel.Composition;

namespace ClientApplication.Views
{
    [Export(typeof(IPluginPart))]
    public partial class TextView : UserControl, IPluginPart
    {
        public TextView()
        {
            InitializeComponent();
        }

        public UserControl View
        {
            get
            {
                return this;
            }
        }
    }
}
