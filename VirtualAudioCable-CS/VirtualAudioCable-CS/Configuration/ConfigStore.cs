using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAudioCable_CS.Configuration
{
    class ConfigStore
    {
        private string _defaultPlaybackDevice;
        private bool _defaultLoopbackAvailable;

        public ConfigStore(string defaultPlaybackDevice, bool defaultLoopbackAvailable)
        {
            this._defaultPlaybackDevice = defaultPlaybackDevice;
            this._defaultLoopbackAvailable = defaultLoopbackAvailable;
        }

        public string DefaultPlaybackDevice
        {
            get { return this._defaultPlaybackDevice; }
        }

        public bool DefaultLoopbackAvailable
        {
            get { return this._defaultLoopbackAvailable; }
        }
    }
}
