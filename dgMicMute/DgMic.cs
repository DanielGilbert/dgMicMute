using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using dgMicMute.Enumerations;
using dgMicMute.Implementations;
using dgMicMute.Interfaces;

namespace dgMicMute
{
    public delegate void VolumeNotificationEvent(AudioVolumeNotificationData data); 

    /// <summary>
    /// Starting point: http://msdn.microsoft.com/en-us/library/dd370805(v=vs.85).aspx
    /// </summary>
    public class DgMic
    {
        private MMDeviceCollection _devices;
        private int _count;
        public event VolumeNotificationEvent OnVolumeNotification = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public DgMic()
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            _devices = enumerator.EnumerateAudioEndpoints(EDataFlow.eCapture,
                EDeviceState.DeviceStateActive);

            _count = _devices.Count;

            for (int i = 0; i < _count; i++)
            {
                _devices[i].AudioEndpointVolume.OnVolumeNotification += AudioEndpointVolume_OnVolumeNotification;
            }
        }

        void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            OnVolumeNotification(data);
        }

        public void SetMicStateTo(DgMicStates state)
        {
            for (int i = 0; i < _count; i++)
            {
                try
                {
                    _devices[i].AudioEndpointVolume.Mute = state == DgMicStates.Muted;
                }
                catch
                {
                    
                }
            }
        }

        public bool AreAllMicsMuted()
        {
            var result = from p in _devices where p.AudioEndpointVolume.Mute == false select p;

            return !(result.Any());
        }
    }
}