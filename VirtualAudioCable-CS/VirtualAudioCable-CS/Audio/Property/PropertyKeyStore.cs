using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualAudioCable_CS.Audio.Struct;

namespace VirtualAudioCable_CS.Audio.Property
{
    class PropertyKeyStore
    {
        public static PropertyKey SET_PRIMARY_AUDIO_LOOPBACK_DEVICES = new PropertyKey(new Guid("24dbb0fc-9311-4b3d-9cf0-18ff155639d4"), 1);

        public static PropertyKey AUDIO_LOOPBACK_DEVICES = new PropertyKey(new Guid("24dbb0fc-9311-4b3d-9cf0-18ff155639d4"), 0);

        public static PropertyKey DEVICE_INTERFACE_FRIENDLY_NAME = new PropertyKey(new Guid("026E516E-B814-414B-83CD-856D6FEF4822"), 2);

        public static PropertyKey AUDIO_ENDPOINT_GUID = new PropertyKey(new Guid("1DA5D803-D492-4EDD-8C23-E0C0FFEE7F0E"), 4);

    }
}
