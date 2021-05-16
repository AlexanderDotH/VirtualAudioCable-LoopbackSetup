using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VirtualAudioCable_CS.Audio.Enums;
using VirtualAudioCable_CS.Audio.Interfaces;

namespace VirtualAudioCable_CS.Audio.Device
{
    [ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")] class MMDeviceEnumeratorComObject { }

    class MMDeviceEnumerator
    {
        private IMMDeviceEnumerator deviceEnumerator;

        public MMDeviceEnumerator()
        {
            this.deviceEnumerator = new MMDeviceEnumeratorComObject() as IMMDeviceEnumerator;
        }

        public MMDevice GetDefaultAudioEndpoint(DataFlow dataFlow)
        {

            int audioDirection = -1;

            switch (dataFlow)
            {
                case DataFlow.Capture:
                    audioDirection = 1;
                    break;
                case DataFlow.Render:
                    audioDirection = 0;
                    break;

                default:
                    audioDirection = 1;
                    break;
            }

            IMMDevice device = null;

            Marshal.ThrowExceptionForHR(this.deviceEnumerator.GetDefaultAudioEndpoint(audioDirection, 1, out device));

            string deviceID = string.Empty;
            device.GetId(out deviceID);

            return GetDevice(deviceID);
        }

        public MMDeviceCollection EnumerateAudioEndPoints(DataFlow dataFlow, DeviceState dwStateMask)
        {
            Marshal.ThrowExceptionForHR(deviceEnumerator.EnumAudioEndpoints(dataFlow, dwStateMask, out var result));
            return new MMDeviceCollection(result);
        }

        public MMDevice GetDevice(string id)
        {
            Marshal.ThrowExceptionForHR(((IMMDeviceEnumerator)deviceEnumerator).GetDevice(id, out var device));
            return new MMDevice(device);
        }
    }
}
