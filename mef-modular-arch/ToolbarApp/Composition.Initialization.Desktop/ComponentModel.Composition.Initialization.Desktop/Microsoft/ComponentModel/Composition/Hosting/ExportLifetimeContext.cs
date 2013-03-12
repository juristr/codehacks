// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace Microsoft.ComponentModel.Composition.Hosting
{
    public abstract class ExportLifetimeContextInternal<T> : IDisposable
    {
        private readonly T _value;
        private readonly Action _dispose;

        public ExportLifetimeContextInternal(T value, Action dispose)
        {
            this._value = value;
            this._dispose = dispose;
        }

        public T Value
        {
            get { return this._value; }
        }

        public void Dispose()
        {
            this._dispose();
        }
    }
}