﻿using Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using WinFormsClientApplication.Export;

namespace WinFormsClientApplication
{
    public class Bootstrapper : IModule
    {
        private AggregateCatalog mainCatalog;
        private CompositionContainer container;

        [Import]
        public Form Shell { get; set; }

        public void Initialize()
        {
            mainCatalog = new AggregateCatalog(new AssemblyCatalog(GetType().Assembly));

            //this could be discovered dynamically (in case that behavior is needed)
            //NOTE: theres is an after build copy event that copies the plugin into the bin folder of this app
            //var pluginsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "plugins");

            //if (!Directory.Exists(pluginsDirectory))
            //    Directory.CreateDirectory(pluginsDirectory);

            //var directoryCatalog = new DirectoryCatalog(pluginsDirectory);
            //mainCatalog.Catalogs.Add(directoryCatalog);



            container = new CompositionContainer(mainCatalog);
            var debugger = new MefDebugger(container);
            container.ComposeParts(this);
            debugger.Close();
            
            Application.Run(Shell);
        }

    }
}