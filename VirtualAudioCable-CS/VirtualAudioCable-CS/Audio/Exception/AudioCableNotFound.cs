using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAudioCable_CS.Audio.Exception
{
    class AudioCableNotFound : System.Exception
    {
        public AudioCableNotFound() : base("Could not find VB-Audio Cable") { }
    }
}
