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

namespace WinFormsClientApplication.ValueModule
{
    [Export(typeof(IValueProviderExtension))]
    [Export(typeof(Control))]
    public partial class ValueControl : UserControl, IValueProviderExtension
    {
        public ValueControl()
        {
            InitializeComponent();
        }

        public string Value
        {
            get { return textBoxValue.Text; }
        }
    }
}
