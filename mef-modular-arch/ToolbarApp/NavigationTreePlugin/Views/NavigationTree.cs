using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Base.Command;
using System.ComponentModel.Composition;

namespace NavigationTreePlugin.Views
{
    [Export("navigationMenu", typeof(UserControl))]
    public partial class NavigationTree : UserControl
    {

        [ImportMany]
        public IEnumerable<ToolStripMenuItem> PluginMenus { get; set; }

        public NavigationTree()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            foreach (var pluginMenu in PluginMenus)
            {
                listBoxNavigation.Items.Add(pluginMenu.Text);
            }
        }

        private void listBoxNavigation_DoubleClick(object sender, EventArgs e)
        {
            foreach (var item in PluginMenus)
            {
                if (item.Text.Equals(listBoxNavigation.SelectedItem.ToString()))
                {
                    item.PerformClick();
                }
            }
        }
    }
}
