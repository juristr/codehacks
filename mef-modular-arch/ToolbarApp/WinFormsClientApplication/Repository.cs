using Base.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace WinFormsClientApplication
{
    [Export(typeof(IRepository<>))]
    public class Repository<TItem> : IRepository<TItem>
    {
        public TItem Get()
        {
            return default(TItem);
        }
    }
}
