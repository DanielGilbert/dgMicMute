using System.Linq;
using dgMicMute.Enumerations;
using dgMicMute.Implementations;

namespace dgMicMute
{
    public delegate void VolumeNotificationEvent(AudioVolumeNotificationData data);

    /// <summary>
    /// Starting point: http://msdn.microsoft.com/en-us/library/dd370805(v=vs.85).aspx
    /// </summary>
    public class DgMic
    {
        private readonly MMDeviceCollection _devices;
        private readonly int _count;
        public event VolumeNotificationEvent OnVolumeNotification = delegate { };
        private DgMicStates _oldState;

        public DgMic()
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            _devices = enumerator.EnumerateAudioEndpoints(EDataFlow.ECapture,
                EDeviceState.DeviceStateActive);
            _oldState = DgMicStates.Unmuted;
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
            if (_oldState == state) return;

            _oldState = state;

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

        public void Toggle()
        {
            SetMicStateTo(_oldState == DgMicStates.Muted ? DgMicStates.Unmuted : DgMicStates.Muted);
        }

        public bool AreAllMicsMuted()
        {
            return !((from p
                      in _devices
                      where p.AudioEndpointVolume.Mute == false
                      select p).Any());
        }
    }
}