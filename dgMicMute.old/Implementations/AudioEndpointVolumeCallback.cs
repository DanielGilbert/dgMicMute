using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using dgMicMute.Enumerations;
using dgMicMute.Interfaces;
using dgMicMute.Structures;

namespace dgMicMute.Implementations
{
    /// <summary>
    /// This class implements the IAudioEndpointVolumeCallback interface,
    /// it is implemented in this class, because implementing it on AudioEndpointVolume 
    /// (where the functionality is really wanted), would cause the OnNotify function 
    /// to show up in the public API.
    /// 
    /// This class is inspired by some code off the internet, which origin is unknown.
    /// </summary>
    internal class AudioEndpointVolumeCallback : IAudioEndpointVolumeCallback
    {
        private readonly AudioEndpointVolume _parent;

        internal AudioEndpointVolumeCallback(AudioEndpointVolume parent)
        {
            _parent = parent;
        }

        [PreserveSig]
        public int OnNotify(IntPtr notifyData)
        {
            //Since AUDIO_VOLUME_NOTIFICATION_DATA is dynamic in length based on the
            //number of audio channels available we cannot just call PtrToStructure 
            //to get all data, thats why it is split up into two steps, first the static
            //data is marshalled into the data structure, then with some IntPtr math the
            //remaining floats are read from memory.
            //
            AudioVolumeNotificationDataStruct data = (AudioVolumeNotificationDataStruct)Marshal.PtrToStructure(notifyData, typeof(AudioVolumeNotificationDataStruct));

            //Determine offset in structure of the first float
            IntPtr Offset = Marshal.OffsetOf(typeof(AudioVolumeNotificationDataStruct), "ChannelVolume");
            //Determine offset in memory of the first float
            IntPtr FirstFloatPtr = (IntPtr)((long)notifyData + (long)Offset);

            float[] voldata = new float[data.Channels];

            //Read all floats from memory.
            for (int i = 0; i < data.Channels; i++)
            {
                voldata[i] = (float)Marshal.PtrToStructure(FirstFloatPtr, typeof(float));
            }

            //Create combined structure and Fire Event in parent class.
            AudioVolumeNotificationData notificationData = new AudioVolumeNotificationData(data.GuidEventContext, data.Muted, data.MasterVolume, voldata);
            _parent.FireNotification(notificationData);
            return 0; //S_OK
        }
    }
}
