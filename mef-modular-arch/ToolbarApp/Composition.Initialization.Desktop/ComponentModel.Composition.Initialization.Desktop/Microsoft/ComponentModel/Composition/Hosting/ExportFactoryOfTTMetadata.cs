// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.Composition;

namespace Microsoft.ComponentModel.Composition.Hosting
{
    public abstract class ExportFactoryInternal<T, TMetadata> : ExportFactoryInternal<T>
    {
        private readonly TMetadata _metadata;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public ExportFactoryInternal(Func<ExportLifetimeContext<T>> creator, TMetadata metadata)
            : base(creator)
        {
            this._metadata = metadata;
        }

        public TMetadata Metadata
        {
            get { return this._metadata; }
        }
    }
}