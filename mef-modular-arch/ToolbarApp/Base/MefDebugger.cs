using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace Base
{
       /// <summary>
    ///     Helper class for debugging MEF
    /// </summary>
    public class MefDebugger
    {
        private readonly CompositionContainer _container;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="container">The container to debug</param>
        public MefDebugger(CompositionContainer container)
        {
            _container = container;
            _container.ExportsChanged += _ExportsChanged;
            _DebugCatalog((AggregateCatalog)container.Catalog);
        }

        private static void _ExportsChanged(object sender, ExportsChangeEventArgs args)
        {
            try
            {
                if (args.AddedExports != null)
                {
                    _ParseExports("** Added Export: ", args.AddedExports);
                }

                if (args.RemovedExports != null)
                {
                    _ParseExports("** Removed Export: ", args.RemovedExports);
                }

                if (args.ChangedContractNames != null)
                {
                    var first = true;
                    foreach (var contract in args.ChangedContractNames)
                    {
                        if (first)
                        {
                            Console.WriteLine("=== Contracts Changed ===");
                            first = false;
                        }
                        Console.WriteLine(" ==>{0}", contract);
                    }
                }                
            }
            catch(Exception ex)
            {
                Console.WriteLine("### MEF Debugger Error: {0}", ex.Message);
            }
        }

        /// <summary>
        ///     Debug the catalog
        /// </summary>
        /// <param name="srcCatalog">The source catalog</param>
        private static void _DebugCatalog(AggregateCatalog srcCatalog)
        {                        
            foreach (var catalog in srcCatalog.Catalogs)
            {    
                Console.WriteLine("--- Catalog {0} ---", catalog);
                
                foreach (var part in catalog.Parts)
                {
                    Console.WriteLine("... Part {0} ...", part);
                    
                    if (part.Metadata != null)
                    {
                        foreach (var key in part.Metadata.Keys)
                        {
                            Console.WriteLine(".... With Key {0}: {1}", key, part.Metadata[key]);                            
                        }
                    }

                    foreach (var import in part.ImportDefinitions)
                    {
                        Console.WriteLine(".... With Import {0}", import); 
                        Console.WriteLine("....... Cardinality {0}", import.Cardinality);
                        Console.WriteLine("....... Constraint {0}", import.Constraint);                       
                    } 

                    _ParseExports(".... With Exports", part.ExportDefinitions);
                }
            }
        }

        /// <summary>
        ///     Parse the exports
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="exports"></param>
        private static void _ParseExports(string tag, IEnumerable<ExportDefinition> exports)
        {
            foreach (var export in exports)
            {
                Console.WriteLine("{0} => Export {1}", tag, export);
                
                if (export.Metadata == null) continue;

                foreach (var key in export.Metadata.Keys)
                {
                    Console.WriteLine("{0} .... => Key: {1}: {2}", tag, key, export.Metadata[key]);                   
                }
            }
        }

        public void Close()
        {
            Console.WriteLine("Done Debugging.");
            _container.ExportsChanged -= _ExportsChanged;
        }
    }    
 
}