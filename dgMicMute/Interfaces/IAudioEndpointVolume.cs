using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace dgMicMute.Interfaces
{
    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioEndpointVolume
    {
		/// <summary>
		/// Registers a client's notification callback interface.
		/// </summary>
		/// <param name="client">The <see cref="IAudioEndpointVolumeCallback"/> interface that is registering for notification callbacks.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int RegisterControlChangeNotify(
			[In] [MarshalAs(UnmanagedType.Interface)] IAudioEndpointVolumeCallback client);

		/// <summary>
		/// Deletes the registration of a client's notification callback interface.
		/// </summary>
        /// <param name="client">The <see cref="IAudioEndpointVolumeCallback"/> interface that previously registered for notification callbacks.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int UnregisterControlChangeNotify(
            [In] [MarshalAs(UnmanagedType.Interface)] IAudioEndpointVolumeCallback client);

        /// <summary>
        /// Gets a count of the channels in the audio stream.
        /// </summary>
        /// <param name="channelCount">The number of channels.</param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int GetChannelCount(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 channelCount);

		/// <summary>
		/// Sets the master volume level of the audio stream, in decibels.
		/// </summary>
		/// <param name="level">The new master volume level in decibels.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int SetMasterVolumeLevel(
			[In] [MarshalAs(UnmanagedType.R4)] float level,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

		/// <summary>
		/// Sets the master volume level, expressed as a normalized, audio-tapered value.
		/// </summary>
		/// <param name="level">The new master volume level expressed as a normalized value between 0.0 and 1.0.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int SetMasterVolumeLevelScalar(
			[In] [MarshalAs(UnmanagedType.R4)] float level,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

		/// <summary>
		/// Gets the master volume level of the audio stream, in decibels.
		/// </summary>
		/// <param name="level">The volume level in decibels.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int GetMasterVolumeLevel(
			[Out] [MarshalAs(UnmanagedType.R4)] out float level);

		/// <summary>
		/// Gets the master volume level, expressed as a normalized, audio-tapered value.
		/// </summary>
		/// <param name="level">The volume level expressed as a normalized value between 0.0 and 1.0.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int GetMasterVolumeLevelScalar(
			[Out] [MarshalAs(UnmanagedType.R4)] out float level);

		/// <summary>
		/// Sets the volume level, in decibels, of the specified channel of the audio stream.
		/// </summary>
		/// <param name="channelNumber">The channel number.</param>
		/// <param name="level">The new volume level in decibels.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int SetChannelVolumeLevel(
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 channelNumber,
			[In] [MarshalAs(UnmanagedType.R4)] float level,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

		/// <summary>
		/// Sets the normalized, audio-tapered volume level of the specified channel in the audio stream.
		/// </summary>
		/// <param name="channelNumber">The channel number.</param>
		/// <param name="level">The new master volume level expressed as a normalized value between 0.0 and 1.0.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int SetChannelVolumeLevelScalar(
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 channelNumber,
			[In] [MarshalAs(UnmanagedType.R4)] float level,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

        /// <summary>
        /// Gets the volume level, in decibels, of the specified channel in the audio stream.
        /// </summary>
        /// <param name="channelNumber">The zero-based channel number.</param>
		/// <param name="level">The volume level in decibels.</param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int GetChannelVolumeLevel(
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 channelNumber,
			[Out] [MarshalAs(UnmanagedType.R4)] out float level);

        /// <summary>
        /// Gets the normalized, audio-tapered volume level of the specified channel of the audio stream.
        /// </summary>
        /// <param name="channelNumber">The zero-based channel number.</param>
		/// <param name="level">The volume level expressed as a normalized value between 0.0 and 1.0.</param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int GetChannelVolumeLevelScalar(
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 channelNumber,
			[Out] [MarshalAs(UnmanagedType.R4)] out float level);

		/// <summary>
		/// Sets the muting state of the audio stream.
		/// </summary>
		/// <param name="isMuted">True to mute the stream, or false to unmute the stream.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int SetMute(
			[In] [MarshalAs(UnmanagedType.Bool)] Boolean isMuted,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

        /// <summary>
        /// Gets the muting state of the audio stream.
        /// </summary>
        /// <param name="isMuted">The muting state. True if the stream is muted, false otherwise.</param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int GetMute(
			[Out] [MarshalAs(UnmanagedType.Bool)] out Boolean isMuted);

		/// <summary>
		/// Gets information about the current step in the volume range.
		/// </summary>
		/// <param name="step">The current zero-based step index.</param>
		/// <param name="stepCount">The total number of steps in the volume range.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int GetVolumeStepInfo(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 step,
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 stepCount);

		/// <summary>
		/// Increases the volume level by one step.
		/// </summary>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int VolumeStepUp(
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

		/// <summary>
		/// Decreases the volume level by one step.
		/// </summary>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int VolumeStepDown(
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

		/// <summary>
		/// Queries the audio endpoint device for its hardware-supported functions.
		/// </summary>
		/// <param name="hardwareSupportMask">A hardware support mask that indicates the capabilities of the endpoint.</param>
		/// <returns>HRESULT</returns>
		[PreserveSig]
		int QueryHardwareSupport(
			[Out] [MarshalAs(UnmanagedType.U4)] out UInt32 hardwareSupportMask);

        /// <summary>
        /// Gets the volume range of the audio stream, in decibels.
        /// </summary>
		/// <param name="volumeMin">The minimum volume level in decibels.</param>
		/// <param name="volumeMax">The maximum volume level in decibels.</param>
		/// <param name="volumeStep">The volume increment level in decibels.</param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int GetVolumeRange(
            [Out] [MarshalAs(UnmanagedType.R4)] out float volumeMin,
			[Out] [MarshalAs(UnmanagedType.R4)] out float volumeMax,
			[Out] [MarshalAs(UnmanagedType.R4)] out float volumeStep);
    
    }
}
