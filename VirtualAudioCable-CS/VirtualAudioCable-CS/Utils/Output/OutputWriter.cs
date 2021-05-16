using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualAudioCable_CS.Utils.Output;

namespace VirtualAudioCable_CS.Utils
{
    class OutputWriter
    {

        public static void Write(string input, OutputType type)
        {
            Console.WriteLine("{0} - {1} : {2}", DateTime.Now, input, type.ToString().ToUpper());
        }

        public static void Write(string input)
        {
            Console.WriteLine("{0} - {1}", DateTime.Now, input);
        }
    }
}
