using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using dgMicMute.Enumerations;

namespace dgMicMute.Interfaces
{
    /// <summary>
    /// The IMMDeviceEnumerator interface provides methods for enumerating multimedia device resources.
    /// In the current implementation of the MMDevice API,
    /// the only device resources that this interface can enumerate are audio endpoint devices.
    /// </summary>
    /// <remarks>
    /// Source: http://msdn.microsoft.com/en-us/library/windows/desktop/dd371399(v=vs.85).aspx
    /// </remarks>
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceEnumerator
    {
        /// <summary>
        /// The EnumAudioEndpoints method generates a collection of audio endpoint devices that meet the specified criteria.
        /// </summary>
        /// <param name="dataFlow">The data-flow direction for the endpoint device.</param>
        /// <param name="dwStateMask">The state or states of the endpoints that are to be included in the collection. The caller should set this parameter to the bitwise OR of one or more of the following DEVICE_STATE_XXX constants</param>
        /// <param name="pDevices"></param>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/dd371400(v=vs.85).aspx </remarks>
        /// <returns></returns>
        [PreserveSig]
        int EnumAudioEndpoints(
            [In] [MarshalAs(UnmanagedType.I4)] EDataFlow dataFlow,
            [In] [MarshalAs(UnmanagedType.U4)] EDeviceState dwStateMask,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IMMDeviceCollection pDevices);

        /// <summary>
        /// The GetDefaultAudioEndpoint method retrieves the default audio endpoint for the specified data-flow direction and role.
        /// </summary>
        /// <param name="dataFlow">The data-flow direction for the endpoint device.</param>
        /// <param name="role">The role of the endpoint device.</param>
        /// <param name="pDevice"></param>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/dd371401(v=vs.85).aspx </remarks>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int GetDefaultAudioEndpoint(
            [In] [MarshalAs(UnmanagedType.I4)] EDataFlow dataFlow,
            [In] [MarshalAs(UnmanagedType.I4)] ERole role,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IMMDevice pDevice);

        /// <summary>
        /// The GetDevice method retrieves an audio endpoint device that is identified by an endpoint ID string.
        /// </summary>
        /// <param name="strId"></param>
        /// <param name="pDevice"></param>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/dd371402(v=vs.85).aspx </remarks>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int GetDevice(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string strId,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IMMDevice pDevice);

        /// <summary>
        /// The RegisterEndpointNotificationCallback method registers a client's notification callback interface.
        /// </summary>
        /// <param name="pNotify">Pointer to the IMMNotificationClient interface that the client is registering for notification callbacks.</param>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/dd371403(v=vs.85).aspx </remarks>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int RegisterEndpointNotificationCallback(
            [In] [MarshalAs(UnmanagedType.Interface)] IMMNotificationClient pNotify);

        /// <summary>
        /// The UnregisterEndpointNotificationCallback method deletes the registration of a notification interface that the client registered in a previous call to the IMMDeviceEnumerator::RegisterEndpointNotificationCallback method.
        /// </summary>
        /// <param name="pNotify">Pointer to the client's IMMNotificationClient interface. The client passed this same interface pointer to the device enumerator in a previous call to the IMMDeviceEnumerator::RegisterEndpointNotificationCallback method. For more information, see Remarks.</param>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/dd371404(v=vs.85).aspx </remarks>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int UnregisterEndpointNotificationCallback(
            [In] [MarshalAs(UnmanagedType.Interface)] IMMNotificationClient pNotify);
    }
}
