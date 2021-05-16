using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VirtualAudioCable_CS.Audio.Interfaces;
using VirtualAudioCable_CS.Audio.Struct;

namespace VirtualAudioCable_CS.Audio.Property
{
    public class PropertyStore
    {
        private IPropertyStore propertyStore;

        internal PropertyStore(IPropertyStore propertyStore)
        {
            this.propertyStore = propertyStore;
        }

        public PropertyStoreProperty this[PropertyKey key]
        {
            get
            {
                for (int i = 0; i < Count; i++)
                {
                    PropertyKey ikey = Get(i);
                    if ((ikey.formatId == key.formatId) && (ikey.propertyId == key.propertyId))
                    {
                        Marshal.ThrowExceptionForHR(propertyStore.GetValue(ref ikey, out var result));
                        return new PropertyStoreProperty(ikey, result);
                    }
                }
                return null;
            }
        }

        public bool Contains(PropertyKey key)
        {
            for (int i = 0; i < Count; i++)
            {
                PropertyKey ikey = Get(i);
                if ((ikey.formatId == key.formatId) && (ikey.propertyId == key.propertyId))
                {
                    return true;
                }
            }
            return false;
        }

        public int Count
        {
            get
            {
                Marshal.ThrowExceptionForHR(propertyStore.GetCount(out var result));
                return result;
            }
        }

        public PropertyKey Get(int index)
        {
            Marshal.ThrowExceptionForHR(propertyStore.GetAt(index, out var key));
            return key;
        }

        public PropVariant GetValue(int index)
        {
            PropertyKey key = Get(index);
            Marshal.ThrowExceptionForHR(propertyStore.GetValue(ref key, out var result));
            return result;
        }

        public void SetValue(PropertyKey key, string value)
        {
            IntPtr pointer = Marshal.StringToHGlobalAuto(value);
            PropVariant val = new PropVariant()
            {
                vt = (short)VarEnum.VT_LPWSTR,
                pointerValue = pointer
            };

            SetValue(key, value);
        }

        public void SetValue(PropertyKey key, PropVariant value)
        {
            Marshal.ThrowExceptionForHR(propertyStore.SetValue(ref key, ref value));
        }

        public void Commit()
        {
            Marshal.ThrowExceptionForHR(propertyStore.Commit());
        }
    }
}
