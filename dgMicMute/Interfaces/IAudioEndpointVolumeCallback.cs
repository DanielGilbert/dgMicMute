using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace dgMicMute.Interfaces
{
    /// <summary>
    /// The IAudioEndpointVolumeCallback interface provides notifications of changes in the volume level and muting state of an audio endpoint device.
    /// </summary>
    /// <remarks>
    /// Source: http://msdn.microsoft.com/en-us/library/windows/desktop/dd370894(v=vs.85).aspx
    /// </remarks>
    [Guid("657804FA-D6AD-4496-8A60-352752AF4F89"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioEndpointVolumeCallback
    {
        /// <summary>
        /// The OnNotify method notifies the client that the volume level or muting state of the audio endpoint device has changed.
        /// </summary>
        /// <param name="notificationData"></param>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/windows/desktop/dd370895(v=vs.85).aspx </remarks>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int OnNotify(
            [In] IntPtr notificationData);
    }
}
