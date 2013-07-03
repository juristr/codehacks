using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComponentModel.Composition.Initialization.Desktop.Tests
{
    [TestClass]
    public class CompositionInitializerTests
    {
        [TestInitialize]
        public void Setup()
        {
            _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        private string _baseDirectory;

        private IEnumerable<DirectoryCatalog> GetDirectoryCatalogs()
        {
            var hostCatalog = CompositionInitializer.GetCatalog();
            return hostCatalog.Catalogs.OfType<DirectoryCatalog>();
        }

        [TestMethod]
        public void When_Catalog_is_built_then_contains_catalog_for_bin_directory_filtering_on_exe()
        {
            var catalogs = GetDirectoryCatalogs();
            Assert.IsTrue(catalogs.Any(d => d.Path == _baseDirectory && d.SearchPattern == "*.exe"));
        }

        [TestMethod]
        public void When_Catalog_is_built_then_contains_catalog_for_bin_directory_filtering_on_dll()
        {
            var catalogs = GetDirectoryCatalogs();
            Assert.IsTrue(catalogs.Any(d => d.Path == _baseDirectory && d.SearchPattern == "*.dll"));
        }

        [TestMethod]
        public void When_Catalog_is_built_then_contains_catalog_for_extensions_directory_if_present()
        {
            Directory.CreateDirectory(_baseDirectory + @"\Extensions");
            var catalogs = GetDirectoryCatalogs();
            Assert.IsTrue(catalogs.Any(d => d.Path == _baseDirectory + @"\Extensions\"));

        }

        [TestMethod]
        public void When_SatisfyImports_is_called_parts_imports_are_satisfied()
        {
            CompositionHost._container = null;
            var part = new DummyPart();
            CompositionInitializer.SatisfyImports(part);
            Assert.IsNotNull(part.Import);
        }

        public class DummyPart
        {
            [Import]
            public DummyImport Import { get; set; }
        }

        [Export]
        public class DummyImport
        {
        }


    }

}

