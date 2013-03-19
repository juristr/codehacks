using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavigationTreePlugin
{
    [ModuleExport(typeof(NavigationTreePluginModule))]
    public class NavigationTreePluginModule : IModule
    {
        public void Initialize()
        {
            
        }
    }
}
