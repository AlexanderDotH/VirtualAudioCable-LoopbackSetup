using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualAudioCable_CS.Audio.Struct;

namespace VirtualAudioCable_CS.Audio.Property
{
    public class PropertyStoreProperty
    {
        private PropVariant propertyValue;

        internal PropertyStoreProperty(PropertyKey key, PropVariant value)
        {
            Key = key;
            propertyValue = value;
        }

        /// <summary>
        /// Property Key
        /// </summary>
        public PropertyKey Key { get; }

        /// <summary>
        /// Property Value
        /// </summary>
        public object Value => propertyValue.Value;
    }
}
