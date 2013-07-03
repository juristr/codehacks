using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComponentModel.Composition.Initialization.Desktop.Tests
{
    [TestClass]
    public class CompositionHostTests
    {
        [TestInitialize]
        public void Setup()
        {
            CompositionHost._container = null;
        }

        [TestMethod]
        public void When_initialize_is_called_passing_in_catalogs_then_container_is_created_with_those_catalogs()
        {
            var catalog = new AssemblyCatalog(typeof (CompositionHostTests).Assembly);
            CompositionHost.Initialize(catalog);
            var container = CompositionHost._container;
            var aggregate = (AggregateCatalog) container.Catalog;
            Assert.AreEqual(catalog, aggregate.Catalogs.First());
        }

        [TestMethod]
        public void When_initialize_called_passing_in_a_container_is_set_to_that_container()
        {
            var catalog = new AssemblyCatalog(typeof(CompositionHostTests).Assembly);
            var container = new CompositionContainer(catalog);
            CompositionHost.Initialize(container);
            var overridenContainer = CompositionHost._container;
            Assert.AreEqual(container, overridenContainer);
        }
    }
}
