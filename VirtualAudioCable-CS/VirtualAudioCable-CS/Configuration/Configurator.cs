using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace VirtualAudioCable_CS.Configuration
{
    class Configurator
    {
        private ConfigStore configStore;

        private const string registryEntry = "SOFTWARE\\VirtualAudioCable-CS";

        public Configurator() { }

        public Configurator(ConfigStore configStore)
        {
            this.configStore = configStore;
        }

        public void Write()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(registryEntry);
            key.SetValue("DefaultPlaybackDevice", configStore.DefaultPlaybackDevice, RegistryValueKind.String);
            key.SetValue("DefaultLoopbackAvailable", configStore.DefaultLoopbackAvailable.ToString(), RegistryValueKind.String);
            key.Flush();
            key.Close();
        }

        public ConfigStore Read()
        {
            ConfigStore configStore = new ConfigStore(string.Empty, false);

            RegistryKey key = Registry.CurrentUser.OpenSubKey(registryEntry);

            object dPD = key.GetValue("DefaultPlaybackDevice");
            object dLDA = key.GetValue("DefaultLoopbackAvailable");

            if (dLDA is string && dPD is string)
            {

                string sDPD = dPD.ToString();
                bool bDLDA = Convert.ToBoolean(dLDA);

                configStore = new ConfigStore(sDPD, bDLDA);

            }

            key.Close();

            return configStore == null ? new ConfigStore("error", false) : configStore;
        }

        public bool RemoveConfiguration()
        {
            try
            {
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
                key.DeleteSubKey(registryEntry);

                return true;
            }
            catch (Exception r)
            {
                return false;
            }
        }

        public static bool isConfigurationPresent()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(registryEntry);

                return key.GetValue("DefaultPlaybackDevice") != null && 
                       key.GetValue("DefaultLoopbackAvailable") != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
