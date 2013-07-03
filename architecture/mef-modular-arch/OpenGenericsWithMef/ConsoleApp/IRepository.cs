using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    public interface IRepository<TItem> where TItem:new()
    {

        TItem GetItem();

    }
}
