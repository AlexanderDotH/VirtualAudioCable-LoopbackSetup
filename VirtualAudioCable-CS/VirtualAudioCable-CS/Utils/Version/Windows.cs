using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAudioCable_CS.Utils.Version
{
    class Windows
    {
        public static string GetWindowsDisplayVersion()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");

            object dV = key.GetValue("DisplayVersion");

            if (dV != null)
            {
                if (dV is string)
                {
                    return dV.ToString();
                }
            }

            return string.Empty;
        }
    }
}
