using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Runtime.InteropServices;
using VirtualAudioCable_CS.Audio.Device;
using VirtualAudioCable_CS.Audio.Enums;
using VirtualAudioCable_CS.Audio.Exception;
using VirtualAudioCable_CS.Audio.Property;
using VirtualAudioCable_CS.Configuration;

namespace VirtualAudioCable_CS.Audio
{
    class SetupManager
    {
        private MMDevice _virtualAudioCableDevice;
        private MMDevice _audioDevice;
        private PropertyKeyStore _propertyKeyStore;

        public SetupManager()
        {
            this._propertyKeyStore = new PropertyKeyStore();

            FindVirtualAudioCable();
            FindPrimaryAudioDevice();
        }

        public SetupManager(string virtualAudioCableId)
        {
            this._virtualAudioCableDevice = new MMDeviceEnumerator().GetDevice(virtualAudioCableId);

            FindPrimaryAudioDevice();
        }

        private void FindVirtualAudioCable()
        {
            MMDeviceEnumerator mmDeviceEnumerator = new MMDeviceEnumerator();

            foreach (MMDevice device in mmDeviceEnumerator.EnumerateAudioEndPoints(
                                        DataFlow.Render, 
                                        DeviceState.All))
            {
                if (device.PropertyStore.Contains(_propertyKeyStore.DEVICE_INTERFACE_FRIENDLY_NAME))
                {
                    string deviceName = device.PropertyStore[_propertyKeyStore.DEVICE_INTERFACE_FRIENDLY_NAME].Value.ToString();

                    if (deviceName.Contains("VB-Audio"))
                    {
                        this._virtualAudioCableDevice = device;
                        return;
                    }
                }
            }

            throw new AudioCableNotFound();
        }

        private void FindPrimaryAudioDevice()
        {
            MMDevice device = new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Capture);

            device.PrimaryDevice = true;

            this._audioDevice = device;
        }

        public void SetADAsLoopbackSource()
        {
            if (this._audioDevice != null)
            {
                if (this._audioDevice.PrimaryDevice)
                {
                    PropVariant value = new PropVariant()
                    {
                        vt = (short)VarEnum.VT_BOOL,
                        boolVal = -1
                    };

                    PropertyStore propertyStore = this._audioDevice.PropertyStore;
                    propertyStore.SetValue(_propertyKeyStore.SET_PRIMARY_AUDIO_LOOPBACK_DEVICES, value);
                    propertyStore.Commit();
                }
            }
        }

        public string GetDefaultLoopBackDevice()
        {
            if (this._audioDevice != null)
            {
                if (this._audioDevice.PropertyStore.Contains(_propertyKeyStore.AUDIO_LOOPBACK_DEVICES))
                {
                    string loopBackDevice = this._audioDevice.PropertyStore[_propertyKeyStore.AUDIO_LOOPBACK_DEVICES]
                        .Value.ToString();

                    if (loopBackDevice.Length == 0)
                    {
                        return "Auto";
                    }

                    return loopBackDevice;
                }
                else
                {
                    return "Auto";
                }
            }

            return "Auto";
        }

        public void RevertToDefaultSettings(ConfigStore configStore)
        {
            if (this._audioDevice != null &&
                this._virtualAudioCableDevice != null)
            {
                if (!GetDefaultLoopBackDevice().Equals("Auto"))
                {
                    IntPtr pointer = Marshal.StringToHGlobalAuto("");
                    PropVariant value = new PropVariant()
                    {
                        vt = (short)VarEnum.VT_LPWSTR,
                        pointerValue = pointer
                    };

                    PropertyStore store = this._audioDevice.PropertyStore;
                    store.SetValue(_propertyKeyStore.AUDIO_LOOPBACK_DEVICES, value);
                    store.Commit();
                }

                if (!configStore.DefaultLoopbackAvailable)
                {
                    PropVariant value = new PropVariant()
                    {
                        vt = (short)VarEnum.VT_BOOL,
                        boolVal = 0
                    };

                    PropertyStore propertyStore = this._audioDevice.PropertyStore;
                    propertyStore.SetValue(_propertyKeyStore.SET_PRIMARY_AUDIO_LOOPBACK_DEVICES, value);
                    propertyStore.Commit();
                }
            }
        }

        public void AssignVACAsLoopbackDevice()
        {
            if (this._audioDevice != null && this._virtualAudioCableDevice != null)
            {
                if (this._audioDevice.PrimaryDevice)
                {
                    IntPtr pointer = Marshal.StringToHGlobalAuto(this._virtualAudioCableDevice.GetID());
                    PropVariant value = new PropVariant()
                    {
                        vt = (short)VarEnum.VT_LPWSTR,
                        pointerValue = pointer
                    };

                    PropertyStore store = this._audioDevice.PropertyStore;
                    store.SetValue(_propertyKeyStore.AUDIO_LOOPBACK_DEVICES, value);
                    store.Commit();
                }
                else
                {
                    SetADAsLoopbackSource();
                }
            }
        }

        public MMDevice PrimaryDevice
        {
            get { return this._audioDevice; }
        }

        public MMDevice VirtualAudioCableDevice
        {
            get { return this._virtualAudioCableDevice; }
        }
    }
}
