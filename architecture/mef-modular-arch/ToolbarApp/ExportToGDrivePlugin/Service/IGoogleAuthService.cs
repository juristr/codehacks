using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExportToGDrivePlugin.Service
{
    /// <summary>
    /// a fake interface demoing a possible authentication service to
    /// illustrate dependency injection with AutoFac
    /// </summary>
    interface IGoogleAuthService
    {
        void Authenticate(string user, string pwd);
    }
}
