using Base;
using Base.Command;
using Base.Diagnostics;
using MefContrib.Hosting.Generics;
using MefContrib.Hosting.Interception;
using MefContrib.Hosting.Interception.Configuration;
using Microsoft.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using MyFirstMefPlugin;
using NavigationTreePlugin;
using Prism4Winforms.Prism.MefExtensions;
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
    public class Bootstrapper : SimpleMefBootstrapper
    {

        public new Form1 Shell { get; set; }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            try
            {
                this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(GetType().Assembly));

                //verify the need for this
                this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ICommandHandler).Assembly));

                //modules loading
                this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MyFirstMefPluginModule).Assembly));
                this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(NavigationTreePluginModule).Assembly));

                //this could be discovered dynamically (in case that behavior is needed)
                //NOTE: theres is an after build copy event that copies the plugin into the bin folder of this app
                var pluginsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "plugins");

                if (!Directory.Exists(pluginsDirectory))
                    Directory.CreateDirectory(pluginsDirectory);

                var directoryCatalog = new DirectoryCatalog(pluginsDirectory);

                this.AggregateCatalog.Catalogs.Add(directoryCatalog);
            }
            catch (CompositionException ex)
            {
                Console.WriteLine("#### COMPOSITION EXCEPTION:");
                Console.WriteLine(ex.Message);
            }
        }

        protected override CompositionContainer CreateContainer()
        {
            var exportFactoryProvider = new ExportFactoryProvider();
            var container = new CompositionContainer(this.AggregateCatalog, exportFactoryProvider);
            exportFactoryProvider.SourceProvider = container;

            return container;

            //var debugger = new MefDebugger(container);
            //container.ComposeParts(this);
            //debugger.Close();
        }

        protected override Form CreateShell()
        {
            return this.Container.GetExportedValue<Form1>();
        }

        protected override void InitializeShell()
        {
            this.Shell = base.Shell as Form1;
        }

        //private AggregateCatalog mainCatalog;
        //private CompositionContainer container;

        //[Import]
        //public Form Shell { get; set; }

        //public void Initialize()
        //{
        //    try
        //    {
        //        mainCatalog = new AggregateCatalog(new AssemblyCatalog(GetType().Assembly));

        //        mainCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ICommandHandler).Assembly));

        //        var typeCatalog = new TypeCatalog(typeof(GenericContractRegistry));
        //        var cfg = new InterceptionConfiguration().AddHandler(new GenericExportHandler());
        //        var interceptingCatalog = new InterceptingCatalog(typeCatalog, cfg);
        //        mainCatalog.Catalogs.Add(interceptingCatalog);

        //        //this could be discovered dynamically (in case that behavior is needed)
        //        //NOTE: theres is an after build copy event that copies the plugin into the bin folder of this app
        //        var pluginsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "plugins");

        //        if (!Directory.Exists(pluginsDirectory))
        //            Directory.CreateDirectory(pluginsDirectory);

        //        var directoryCatalog = new DirectoryCatalog(pluginsDirectory);
                
        //        mainCatalog.Catalogs.Add(directoryCatalog);

        //        var exportFactoryProvider = new ExportFactoryProvider();
        //        container = new CompositionContainer(mainCatalog, exportFactoryProvider);
        //        container.ComposeExportedValue<IEventAggregator>(new EventAggregator());

        //        exportFactoryProvider.SourceProvider = container;
                
        //        var debugger = new MefDebugger(container);
        //        container.ComposeParts(this);
        //        debugger.Close();

        //    }
        //    catch (CompositionException ex)
        //    {
        //        Console.WriteLine("#### COMPOSITION EXCEPTION:");
        //        Console.WriteLine(ex.Message);
        //    }
            
        //    Application.Run(Shell);
        //}

    }
}
