// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.Composition;

namespace Microsoft.ComponentModel.Composition.Hosting
{
    public abstract class ExportFactoryInternal<T>
    {
        private readonly Func<ExportLifetimeContext<T>> _creator;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public ExportFactoryInternal(Func<ExportLifetimeContext<T>> creator)
        {
            if (creator == null) throw new ArgumentNullException("creator");
            this._creator = creator;
        }

        public ExportLifetimeContext<T> CreateExport()
        {
            return this._creator();
        }
    }
}