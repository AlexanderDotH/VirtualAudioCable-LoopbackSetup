using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAudioCable_CS.Audio.Exception
{
    class SetupErrorException : System.Exception
    {
        public SetupErrorException() : base("Due to an error, the setup cannot be completed") { }
    }
}
