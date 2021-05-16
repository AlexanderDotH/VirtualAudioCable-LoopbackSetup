using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VirtualAudioCable_CS.Audio.Interfaces;

namespace VirtualAudioCable_CS.Audio.Device
{
    public class MMDeviceCollection : IEnumerable<MMDevice>
    {
        private IMMDeviceCollection deviceCollection;

        internal MMDeviceCollection(IMMDeviceCollection deviceCollection)
        {
            this.deviceCollection = deviceCollection;
        }

        public MMDevice this[int index]
        {
            get
            {
                deviceCollection.Item(index, out var result);
                return new MMDevice(result);
            }
        }

        public IEnumerator<MMDevice> GetEnumerator()
        {
            for (int index = 0; index < Count; index++)
            {
                yield return this[index];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get
            {
                Marshal.ThrowExceptionForHR(deviceCollection.GetCount(out var result));
                return result;
            }
        }
    }
}
