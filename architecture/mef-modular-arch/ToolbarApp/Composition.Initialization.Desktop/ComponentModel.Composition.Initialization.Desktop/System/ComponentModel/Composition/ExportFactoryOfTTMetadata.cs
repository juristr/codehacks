// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.Composition.Primitives;
using Microsoft.ComponentModel.Composition.Hosting;

namespace System.ComponentModel.Composition
{
    public class ExportFactory<T, TMetadata> : ExportFactoryInternal<T, TMetadata>
    {
        public ExportFactory(Func<ExportLifetimeContext<T>> exportLifetimeContextCreator, TMetadata metadata)
            : base(exportLifetimeContextCreator, metadata)
        {
        }
    }
}

