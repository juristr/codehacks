using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace ExportToGDrivePlugin.Service
{
    [Export(typeof(IGoogleAuthService))]
    class GoogleAuthService : IGoogleAuthService
    {
        public void Authenticate(string user, string pwd)
        {
            //do something here
        }
    }
}
