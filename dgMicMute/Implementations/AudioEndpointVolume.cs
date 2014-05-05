using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using dgMicMute.Interfaces;

namespace dgMicMute.Implementations
{
    public delegate void AudioEndpointVolumeNotificationEvent(AudioVolumeNotificationData data);

    /// <summary>
    /// A clean implementation for the IAudioEndpointVolume-Interface as a helper for the Interface itself.
    /// 
    /// Up to now, only the Muting and the VolumeCallback ist implemented.
    /// </summary>
    public class AudioEndpointVolume
    {
        private readonly IAudioEndpointVolume _audioEndpointVolume;
        private AudioEndpointVolumeCallback _callBack;
        public event AudioEndpointVolumeNotificationEvent OnVolumeNotification = delegate {};

        /// <summary>
        /// Mutes the Device, or gets it Muted State
        /// </summary>
        public bool Mute
        {
            get
            {
                bool result;
                Marshal.ThrowExceptionForHR(_audioEndpointVolume.GetMute(out result));
                return result;
            }
            set
            {
                Marshal.ThrowExceptionForHR(_audioEndpointVolume.SetMute(value, Guid.Empty));
            }
        }

        /// <summary>
        /// Gets called by the AudioEndpointVolumeCallback
        /// </summary>
        /// <param name="notificationData"></param>
        internal void FireNotification(AudioVolumeNotificationData notificationData)
        {
            OnVolumeNotification(notificationData);
        }

        public AudioEndpointVolume(IAudioEndpointVolume audioEndpointVolume)
        {
            _audioEndpointVolume = audioEndpointVolume;

            _callBack = new AudioEndpointVolumeCallback(this);
            Marshal.ThrowExceptionForHR(_audioEndpointVolume.RegisterControlChangeNotify(_callBack));
        }

        #region IDisposable

        public void Dispose()
        {
            if (_callBack == null) return;

            Marshal.ThrowExceptionForHR(_audioEndpointVolume.UnregisterControlChangeNotify(_callBack));
            _callBack = null;
        }

        ~AudioEndpointVolume()
        {
            Dispose();
        }
        #endregion
    }
}