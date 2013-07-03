using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    class Program
    {

        [Import]
        public IRepository<Order> OrderRepo { get; set; }

        public void Run()
        {
            Console.WriteLine("Invoking Order repository: " + OrderRepo.GetItem());
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            var program = new Program();

            var aggregateCatalog = new AggregateCatalog(new AssemblyCatalog(typeof(Program).Assembly));

            //aggregateCatalog.Catalogs.Add(new GenericCatalog(new GenericTypeRegistry()));
            


            var compositionContainer = new CompositionContainer(aggregateCatalog);
           
            compositionContainer.ComposeParts(program);

            program.Run();
        }
    }
}
