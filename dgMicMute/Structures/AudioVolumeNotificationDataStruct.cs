using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dgMicMute.Structures
{
    /// <summary>
    /// 
    /// </summary>
    internal struct AudioVolumeNotificationDataStruct
    {
        public Guid GuidEventContext;
        public bool BMuted;
        public float FMasterVolume;
        public uint NChannels;
        public float ChannelVolume;
    }
}
