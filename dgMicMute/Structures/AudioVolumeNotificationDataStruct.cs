using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dgMicMute.Structures
{
    internal struct AudioVolumeNotificationDataStruct
    {
        public Guid guidEventContext;
        public bool bMuted;
        public float fMasterVolume;
        public uint nChannels;
        public float ChannelVolume;
    }
}
