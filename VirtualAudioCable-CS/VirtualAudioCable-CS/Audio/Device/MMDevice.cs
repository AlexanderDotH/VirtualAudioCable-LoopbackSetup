using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VirtualAudioCable_CS.Audio.Enums;
using VirtualAudioCable_CS.Audio.Interfaces;
using VirtualAudioCable_CS.Audio.Property;
using VirtualAudioCable_CS.Audio.Struct;

namespace VirtualAudioCable_CS.Audio.Device
{
    public class MMDevice : IDisposable
    {
        private readonly IMMDevice deviceInterface;
        private PropertyStore propertyStore;

        public bool PrimaryDevice { get; set; }

        internal MMDevice(IMMDevice deviceInterface)
        {
            this.deviceInterface = deviceInterface;
        }

        public void GetPropertyInformation(StorageAccessMode stgmAccess = StorageAccessMode.ReadWrite)
        {
            Marshal.ThrowExceptionForHR(deviceInterface.OpenPropertyStore(stgmAccess, out var propstore));
            propertyStore = new PropertyStore(propstore);
        }

        public string GetID()
        {
            string outID = string.Empty;

            Marshal.ThrowExceptionForHR(deviceInterface.GetId(out outID));

            return outID;
        }

        public PropertyStore PropertyStore
        {
            get
            {
                if (this.propertyStore == null)
                {
                    GetPropertyInformation();
                }

                return this.propertyStore;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        ~MMDevice()
        {
            Dispose();
        }
    }
}
