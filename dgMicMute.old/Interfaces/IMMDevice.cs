using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using dgMicMute.Enumerations;

namespace dgMicMute.Interfaces
{
    /// <summary>
    /// The IMMDevice interface encapsulates the generic features of a multimedia device resource.
    /// In the current implementation of the MMDevice API, the only type of device resource that an IMMDevice interface can represent is an audio endpoint device.
    /// </summary>
    /// <remarks>
    /// Source: http://msdn.microsoft.com/en-us/library/windows/desktop/dd371395(v=vs.85).aspx
    /// </remarks>
    [Guid("D666063F-1587-4E43-81F1-B948E807363F"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDevice
    {
        /// <summary>
        /// Creates a COM Object to our specified interface
        /// </summary>
        /// <param name="interfaceId">The Id of the interface. See http://msdn.microsoft.com/en-us/library/dd371405(v=vs.85).aspx for the interfaces.</param>
        /// <param name="classContext">The execution context</param>
        /// <param name="activationParams">They are ignored now.</param>
        /// <param name="instancePtr">Pointer to a Pointer(!) which contains a reference to the Interface. Caller MUST free the interface after usage.</param>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/dd371405(v=vs.85).aspx </remarks>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int Activate(
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid interfaceId,
            [In] [MarshalAs(UnmanagedType.U4)] ClsCtx classContext,
            [In, Optional] IntPtr activationParams,
            [Out] [MarshalAs(UnmanagedType.IUnknown)] out object instancePtr);

        /// <summary>
        /// Gets the Id of the Endpoint Device
        /// </summary>
        /// <param name="strId">The endpoint device ID</param>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/dd371407(v=vs.85).aspx </remarks>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int GetId(
            [Out] [MarshalAs(UnmanagedType.LPWStr)] out string strId);

        /// <summary>
        /// The GetState method retrieves the current device state.
        /// </summary>
        /// <param name="dwState">See the remarks-section for the constants to this value</param>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/dd371410(v=vs.85).aspx </remarks>
        /// <returns></returns>
        [PreserveSig]
        int GetState(
            [Out] [MarshalAs(UnmanagedType.U4)] out EDeviceState dwState);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stgmAccess"></param>
        /// <param name="properties"></param>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/dd371412(v=vs.85).aspx </remarks>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int OpenPropertyStore(
            [In]  [MarshalAs(UnmanagedType.U4)] UInt32 stgmAccess,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IPropertyStore properties);

    }
}
