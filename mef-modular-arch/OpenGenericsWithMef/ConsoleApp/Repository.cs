using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    [Export(typeof(IRepository<>))]
    public class Repository<TItem> : IRepository<TItem> where TItem : new()
    {
        public TItem GetItem()
        {
            return new TItem();
        }
    }
}
