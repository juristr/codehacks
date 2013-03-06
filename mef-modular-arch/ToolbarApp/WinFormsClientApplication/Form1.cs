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

    [Export(typeof(IWindowHost))]
    [Export(typeof(Form))]
    public partial class Form1 : Form, IPartImportsSatisfiedNotification, IWindowHost
    {

        [ImportMany("Export", typeof(ToolStripMenuItem))]
        public IEnumerable<ToolStripMenuItem> ExportMenuItems { get; set; }

        [ImportMany]
        public IEnumerable<ToolStripMenuItem> PluginMenus { get; set; }

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

            LoadMenus(exportToolStripMenuItem, ExportMenuItems);
            LoadMenus(pluginsToolStripMenuItem, PluginMenus);
        }

        private void LoadMenus(ToolStripMenuItem parentNode, IEnumerable<ToolStripMenuItem> menuItems)
        {
            //menu extensions
            if (menuItems.Count() > 0)
            {
                foreach (var exportMenuItem in menuItems)
                {
                    parentNode.DropDownItems.Add(exportMenuItem);
                }
            }
            else
            {
                parentNode.Enabled = false;
            }
        }


        public void LoadWindow(Control control)
        {
            flowLayoutPanelMain.Controls.Clear();
            flowLayoutPanelMain.Controls.Add(control);
        }
    }
}
