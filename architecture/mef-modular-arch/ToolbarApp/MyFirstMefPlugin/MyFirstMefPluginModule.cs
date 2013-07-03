using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstMefPlugin
{
    [ModuleExport(typeof(MyFirstMefPluginModule))]
    public class MyFirstMefPluginModule : IModule
    {

        public MyFirstMefPluginModule()
        {

        }

        public void Initialize()
        {
            
        }
    }
}
