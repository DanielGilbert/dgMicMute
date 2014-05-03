using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dgMicMute.Implementations
{
    public class AudioVolumeNotificationData
    {
        private Guid _EventContext;
        private bool _Muted;
        private float _MasterVolume;
        private int _Channels;
        private float[] _ChannelVolume;

        public Guid EventContext
        {
            get
            {
                return _EventContext;
            }
        }

        public bool Muted
        {
            get
            {
                return _Muted;
            }
        }

        public float MasterVolume
        {
            get
            {
                return _MasterVolume;
            }
        }
        public int Channels
        {
            get
            {
                return _Channels;
            }
        }

        public float[] ChannelVolume
        {
            get
            {
                return _ChannelVolume;
            }
        }
        public AudioVolumeNotificationData(Guid eventContext, bool muted, float masterVolume, float[] channelVolume)
        {
            _EventContext = eventContext;
            _Muted = muted;
            _MasterVolume = masterVolume;
            _Channels = channelVolume.Length;
            _ChannelVolume = channelVolume;
        }
    }
}
