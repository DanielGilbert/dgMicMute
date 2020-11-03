using System;
using System.Linq;
using dgMicMute.Enumerations;
using dgMicMute.Implementations;

namespace dgMicMute
{
    public delegate void VolumeNotificationEvent(AudioVolumeNotificationData data);

    /// <summary>
    /// Starting point: http://msdn.microsoft.com/en-us/library/dd370805(v=vs.85).aspx
    /// </summary>
    public class DgMic: IDisposable
    {
        private readonly MMDeviceCollection _devices;
        private readonly int _count;
        public event VolumeNotificationEvent OnVolumeNotification = delegate { };

        public DgMic()
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            _devices = enumerator.EnumerateAudioEndpoints(EDataFlow.ECapture,
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
            // Removed this check so the current state remains the responsibility of the caller (i.e. NotifyIconViewModel).
            // This class now just does whatever it is told to do.
            //if (_oldState == state) return;

            for (int i = 0; i < _count; i++)
            {
                try
                {
                    _devices[i].AudioEndpointVolume.Mute = state == DgMicStates.Muted;
                }
                catch
                {
                    //We don't care about it beeing set or not.
                    //Sometimes, it doesn't work.
                }
            }
        }

        public bool AreAllMicsMuted()
        {
            return !((from p
                      in _devices
                      where p.AudioEndpointVolume.Mute == false
                      select p).Any());
        }

		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing && _devices != null)
				{
					for(int i = 0; i < _count; i++)
					{
                        _devices[i].AudioEndpointVolume.OnVolumeNotification -= AudioEndpointVolume_OnVolumeNotification;
                    }
				}
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}