using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAudioCable_CS.Audio.Native
{
    class Native
    {
        [DllImport("ole32.dll")]
        internal static extern int PropVariantClear(IntPtr pvar);
    }
}
