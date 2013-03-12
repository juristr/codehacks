// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace System.ComponentModel.Composition
{
    public static partial class CompositionInitializer
    {
        internal static AggregateCatalog GetCatalog()
        {
            var catalog = new AggregateCatalog();
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var extensionPath = String.Format(@"{0}\Extensions\", baseDirectory);
            catalog.Catalogs.Add(new DirectoryCatalog(baseDirectory));
            catalog.Catalogs.Add(new DirectoryCatalog(baseDirectory, "*.exe"));
            if (Directory.Exists(extensionPath))
                catalog.Catalogs.Add(new DirectoryCatalog(extensionPath));
            return catalog;
        }
    }
}