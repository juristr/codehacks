using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.Menu
{
    public interface IMenuService
    {
        IMenu GetMenu(string menuName);
    }
}
