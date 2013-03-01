using Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class App : Form, IPartImportsSatisfiedNotification
    {
        private AggregateCatalog mainCatalog;
        private DirectoryCatalog directoryCatalog;
        private CompositionContainer container;

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<IPluginPart> PluginParts { get; set; }

        public App()
        {
            InitializeComponent();
            InitializePlugins();
        }

        private void InitializePlugins()
        {
            mainCatalog = new AggregateCatalog(new AssemblyCatalog(GetType().Assembly));

            container = new CompositionContainer(mainCatalog);

            //load external plugins
            var pluginsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");

            if (!Directory.Exists(pluginsDirectory))
                Directory.CreateDirectory(pluginsDirectory);

            directoryCatalog = new DirectoryCatalog(pluginsDirectory);
            mainCatalog.Catalogs.Add(directoryCatalog);

            container.ComposeParts(this);

            //continuously scan for updates
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    directoryCatalog.Refresh();
                }
            });
        }

        private delegate void OnImportsSatisfiedDelegate();

        public void OnImportsSatisfied()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new OnImportsSatisfiedDelegate(OnImportsSatisfied), null);
            }
            else
            {

                this.layout.Controls.Clear();

                foreach (var pluginPart in PluginParts)
                {
                    layout.Controls.Add(pluginPart.View);
                }
            }
        }
    }
}
