// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Microsoft.ComponentModel.Composition.Hosting;

namespace System.ComponentModel.Composition
{
    public sealed class ExportLifetimeContext<T> : ExportLifetimeContextInternal<T>
    {
        public ExportLifetimeContext(T value, Action disposeAction)
            : base(value, disposeAction)
        {
        }
    }
}

