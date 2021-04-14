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
        public bool Muted;
        public float MasterVolume;
        public uint Channels;
        public float ChannelVolume;
    }
}
