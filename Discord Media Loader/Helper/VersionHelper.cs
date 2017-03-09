using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Media_Loader.Helper
{
    internal class VersionHelper
    {
        internal static Version CurrentVersion => Assembly.GetExecutingAssembly().GetName().Version;


    }
}
