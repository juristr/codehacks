using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExportToGDrivePlugin
{

    [ModuleExport(typeof(ExportToGDrivePluginModule))]
    public class ExportToGDrivePluginModule : IModule
    {

        public ExportToGDrivePluginModule()
        {

        }

        public void Initialize()
        {
            //initialization stuff could come in here
        }
    }
}
