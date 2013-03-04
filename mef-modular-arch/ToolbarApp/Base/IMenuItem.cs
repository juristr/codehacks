using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base
{
    public interface IMenuItem
    {
        string Name { get; set; }
        string Title { get; set; }
        Action Handler { get; set; }
    }
}
