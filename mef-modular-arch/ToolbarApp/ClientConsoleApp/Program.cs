using Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;

namespace ClientConsoleApp
{
    class Program
    {
        private AggregateCatalog mainCatalog;
        private CompositionContainer container;

        [ImportMany]
        public IEnumerable<IToolWindow> ToolWindows { get; set; }

        public Program()
        {

        }

        public void Run()
        {
            mainCatalog = new AggregateCatalog(new AssemblyCatalog(GetType().Assembly));

            container = new CompositionContainer(mainCatalog);
            container.ComposeParts(this);

            //output
            foreach (var toolWin in ToolWindows)
            {
                Console.WriteLine(toolWin.Name);
            }
        }

        static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }

        [Export(typeof(IToolWindow))]
        public class SomeToolWindow : IToolWindow
        {
            public SomeToolWindow()
            {
                Name = "Some tool window";
            }

            [Import]
            public IMenuService MenuService { get; set; }
            public string Name { get; set;}
            public string Title { get; set; }
        }

        public class Application
        {
            [Import]
            public IEnumerable<IToolWindow> ToolWindows { get; set; }

            public IToolWindow GetWindow(string windowName)
            {
                return ToolWindows.Where(x => x.Name == windowName).FirstOrDefault();
            }
        }

        [Export(typeof(IMenuService))]
        public class MenuService : IMenuService
        {
            public IEnumerable<IMenu> Menus { get; set; }

            public IMenu GetMenu(string menuName)
            {
                return Menus.Where(x => x.Name == menuName).FirstOrDefault();
            }
        }
    }
}
