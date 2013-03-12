using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using Microsoft.ComponentModel.Composition.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComponentModel.Composition.Initialization.Desktop.Tests
{
    [TestClass]
    public class ExportFactoryTests
    {
        private CompositionContainer _container;

        public void SetupSingleFactory()
        {
            var catalog = new TypeCatalog(typeof (PartA), typeof(PartThatUsesAFactory));
            var ep = new ExportFactoryProvider();
            _container = new CompositionContainer(catalog, ep);
            ep.SourceProvider = _container;
        }

        public void SetupMultipleFactories()
        {
            var catalog = new TypeCatalog(typeof(PartA), typeof(PartB), typeof(PartThatUsesACollectionOfFactories));
            var ep = new ExportFactoryProvider();
            _container = new CompositionContainer(catalog, ep);
            ep.SourceProvider = _container;
        }

        [TestMethod]
        public void When_Part_is_composed_which_imports_a_factory_the_composition_succeeds()
        {
            SetupSingleFactory();
            var factoryPart = _container.GetExportedValue<PartThatUsesAFactory>();
            Assert.IsNotNull(factoryPart);
        }

        [TestMethod]
        public void When_CreateExport_is_called_ExportLifetimeContext_is_created()
        {
            SetupSingleFactory();
            var factoryPart = _container.GetExportedValue<PartThatUsesAFactory>();
            var context = factoryPart.Factory.CreateExport();
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void When_Context_Value_is_accessed_Export_is_created()
        {
            SetupSingleFactory();
            var factoryPart = _container.GetExportedValue<PartThatUsesAFactory>();
            var context = factoryPart.Factory.CreateExport();
            var export = context.Value;
            Assert.IsNotNull(export);
        }

        [TestMethod]
        public void When_Part_imports_a_collection_of_factories_they_are_supplied()
        {
            SetupMultipleFactories();
            var factoryPart = _container.GetExportedValue<PartThatUsesACollectionOfFactories>();
            Assert.AreEqual(2, factoryPart.Factories.Count());
        }

        [TestMethod]
        public void When_Factory_has_metadata_it_can_be_accessed()
        {
            SetupMultipleFactories();
            var factoryPart = _container.GetExportedValue<PartThatUsesACollectionOfFactories>();
            Assert.IsTrue(factoryPart.Factories.Any(f => f.Metadata.Name.Equals("PartA")));
            Assert.IsTrue(factoryPart.Factories.Any(f => f.Metadata.Name.Equals("PartB")));
        }
    }

    [Export]
    public class PartThatUsesAFactory
    {
        [Import]
        public ExportFactory<IPart> Factory { get; set;}
    }

    [Export]
    public class PartThatUsesACollectionOfFactories
    {
        [ImportMany]
        public ExportFactory<IPart, IPartMetadata>[] Factories { get; set; }
    }

    [Export(typeof(IPart))]
    [ExportMetadata("Name", "PartA")]
    public class PartA : IPart
    {
    }

    [Export(typeof(IPart))]
    [ExportMetadata("Name", "PartB")]
    public class PartB : IPart
    {
        
    }

    public interface IPart
    {
    }

    public interface IPartMetadata
    {
        string Name { get;}
    }


}
