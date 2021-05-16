using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VirtualAudioCable_CS.Audio.Enums;

namespace VirtualAudioCable_CS.Audio.Interfaces
{
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IMMDeviceEnumerator
    {
        int EnumAudioEndpoints(DataFlow dataFlow, DeviceState stateMask,
            out IMMDeviceCollection devices);

        int GetDefaultAudioEndpoint(int dataFlow, int role, out IMMDevice endpoint);

        int GetDevice(string id, out IMMDevice deviceName);
    }
}
