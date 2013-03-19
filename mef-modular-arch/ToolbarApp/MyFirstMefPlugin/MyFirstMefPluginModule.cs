using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstMefPlugin
{
    //[ModuleExport(typeof(MyFirstMefPluginModule))]
    [ModuleExport(typeof(MyFirstMefPluginModule), InitializationMode = InitializationMode.OnDemand)]
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
