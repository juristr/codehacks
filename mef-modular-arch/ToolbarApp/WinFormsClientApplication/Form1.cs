using Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsClientApplication
{

    [Export(typeof(Form))]
    public partial class Form1 : Form, IPartImportsSatisfiedNotification
    {

        [ImportMany("Export", typeof(ToolStripMenuItem))]
        public IEnumerable<ToolStripMenuItem> ExportMenuItems { get; set; }

        [ImportMany]
        public IEnumerable<Control> ViewExtensions { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        public void OnImportsSatisfied()
        {
            //view extensions
            flowLayoutPanelMain.Controls.Clear();
            foreach (var viewExtension in ViewExtensions)
            {
                flowLayoutPanelMain.Controls.Add(viewExtension);
            }


            //menu extensions
            if (ExportMenuItems.Count() > 0)
            {
                foreach (var exportMenuItem in ExportMenuItems)
                {
                    exportToolStripMenuItem.DropDownItems.Add(exportMenuItem);
                }
            }
            else
            {
                exportToolStripMenuItem.Enabled = false;
            }
        }

    }
}
