using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using dgMicMute.Interfaces;

namespace dgMicMute.Implementations
{
    public delegate void AudioEndpointVolumeNotificationDelegate(AudioVolumeNotificationData data);

    public class AudioEndpointVolume
    {
        private IAudioEndpointVolume _audioEndpointVolume;
        private AudioEndpointVolumeCallback _CallBack;
        public event AudioEndpointVolumeNotificationDelegate OnVolumeNotification = delegate {};

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

        internal void FireNotification(AudioVolumeNotificationData notificationData)
        {
            OnVolumeNotification(notificationData);
        }

        public AudioEndpointVolume(IAudioEndpointVolume audioEndpointVolume)
        {
            _audioEndpointVolume = audioEndpointVolume;

            _CallBack = new AudioEndpointVolumeCallback(this);
            Marshal.ThrowExceptionForHR(_audioEndpointVolume.RegisterControlChangeNotify(_CallBack));
        }
        #region IDisposable Members

        public void Dispose()
        {
            if (_CallBack != null)
            {
                Marshal.ThrowExceptionForHR(_audioEndpointVolume.UnregisterControlChangeNotify(_CallBack));
                _CallBack = null;
            }
        }

        ~AudioEndpointVolume()
        {
            Dispose();
        }

        #endregion
       

    }
}