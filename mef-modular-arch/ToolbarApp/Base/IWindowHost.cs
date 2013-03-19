using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Base
{
    public interface IWindowHost
    {
        void LoadWindow(Control control, Positions position = Positions.Main);
    }
}
