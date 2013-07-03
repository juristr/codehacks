using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormsClientApplication
{
    public interface IRepository<TItem>
    {

        TItem Get();

    }
}
