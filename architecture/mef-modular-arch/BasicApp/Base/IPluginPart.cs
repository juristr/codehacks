using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Base
{
    public interface IPluginPart
    {

        UserControl View { get; }

    }
}
