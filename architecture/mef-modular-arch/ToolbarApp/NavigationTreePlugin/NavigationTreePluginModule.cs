using Base;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using NavigationTreePlugin.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NavigationTreePlugin
{
    //[ModuleExport(typeof(NavigationTreePluginModule), InitializationMode = InitializationMode.WhenAvailable)]
    [ModuleExport(typeof(NavigationTreePluginModule), InitializationMode = InitializationMode.WhenAvailable, DependsOnModuleNames = new[] { "MyFirstMefPluginModule" })]
    public class NavigationTreePluginModule : IModule
    {
        private IWindowHost windowHost;
        private UserControl navTree;

        [ImportingConstructor]
        public NavigationTreePluginModule(
            IWindowHost windowHost, 
            [Import("navigationMenu", typeof(UserControl))] NavigationTree navTree)
        {
            this.windowHost = windowHost;
            this.navTree = navTree;
        }

        public void Initialize()
        {
            windowHost.LoadWindow(navTree, Positions.Navigation);
        }
    }
}
