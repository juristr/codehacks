using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.Menu
{
    public interface IMenu
    {
        string Name { get; set; }
        string Title { get; set; }
        string Toolwindow { get; set; }
        IEnumerable<IMenuItem> Items { get; set; }

        IMenuItem GetItem(string itemName);
    }
}
