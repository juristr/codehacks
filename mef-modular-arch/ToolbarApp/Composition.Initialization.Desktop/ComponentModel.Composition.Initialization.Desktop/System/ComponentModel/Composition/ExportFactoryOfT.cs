// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using Microsoft.ComponentModel.Composition.Hosting;
using Microsoft.Internal;
using System.ComponentModel.Composition.Primitives;

namespace System.ComponentModel.Composition
{
    public class ExportFactory<T> : ExportFactoryInternal<T>
    {
        public ExportFactory(Func<ExportLifetimeContext<T>> creator)
            :base(creator)
        {
        }
    }
}
