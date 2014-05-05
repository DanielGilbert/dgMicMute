using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dgMicMute.Implementations
{
    /// <summary>
    /// The AudioVolumeNotificationData structure describes a change 
    /// in the volume level or muting state of an audio endpoint device.
    /// </summary>
    /// <remarks>
    /// Source: http://msdn.microsoft.com/en-us/library/windows/desktop/dd370799(v=vs.85).aspx
    /// </remarks>
    public class AudioVolumeNotificationData
    {
        /// <summary>
        /// Context value for the IAudioEndpointVolumeCallback::OnNotify method.
        /// This member is the value of the event-context GUID that was provided as an input parameter 
        /// to the IAudioEndpointVolume method call that changed the endpoint volume level or muting state.
        /// </summary>
        public Guid EventContext { get; private set; }

        /// <summary>
        /// Specifies whether the audio stream is currently muted. If Muted is TRUE, the stream is muted. If FALSE, the stream is not muted.
        /// </summary>
        public bool Muted { get; private set; }

        /// <summary>
        /// Specifies the current master volume level of the audio stream. 
        /// The volume level is normalized to the range from 0.0 to 1.0, 
        /// where 0.0 is the minimum volume level and 1.0 is the maximum level.
        /// Within this range, the relationship of the normalized volume level to the attenuation of signal amplitude 
        /// is described by a nonlinear, audio-tapered curve.
        /// </summary>
        public float MasterVolume { get; private set; }

        /// <summary>
        /// Specifies the number of channels in the audio stream, 
        /// which is also the number of elements in the <see cref="ChannelVolume"/> array.
        /// If the audio stream contains n channels, the channels are numbered from 0 to n-1.
        /// The volume level for a particular channel is contained in the array element whose index matches the channel number.
        /// </summary>
        public int Channels { get; private set; }

        /// <summary>
        /// The first element in an array of channel volumes.
        /// This element contains the current volume level of channel 0 in the audio stream.
        /// If the audio stream contains more than one channel, the volume levels for the additional channels
        /// immediately follow the <see cref="AudioVolumeNotificationData"/> structure. 
        /// The volume level for each channel is normalized to the range from 0.0 to 1.0,
        /// where 0.0 is the minimum volume level and 1.0 is the maximum level. 
        /// Within this range, the relationship of the normalized volume level to the attenuation of signal amplitude is described by a nonlinear, audio-tapered curve.
        /// </summary>
        public float[] ChannelVolume { get; private set; }

        public AudioVolumeNotificationData(Guid eventContext, bool muted, float masterVolume, float[] channelVolume)
        {
            EventContext = eventContext;
            Muted = muted;
            MasterVolume = masterVolume;
            Channels = channelVolume.Length;
            ChannelVolume = channelVolume;
        }
    }
}
