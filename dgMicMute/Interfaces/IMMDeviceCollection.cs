using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace dgMicMute.Interfaces
{
    /// <summary>
    /// The IMMDeviceCollection interface represents a collection of multimedia device resources.
    /// In the current implementation, the only device resources that the MMDevice API can create collections 
    /// of are audio endpoint devices.
    /// </summary>
    /// <remarks>
    /// Source: http://msdn.microsoft.com/en-us/library/windows/desktop/dd371396(v=vs.85).aspx
    /// </remarks>
    [Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceCollection
    {
        /// <summary>
        /// The GetCount method retrieves a count of the devices in the device collection.
        /// </summary>
        /// <param name="cDevices"></param>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/dd371397(v=vs.85).aspx </remarks>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int GetCount(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 cDevices);

        /// <summary>
        /// The Item method retrieves a pointer to the specified item in the device collection.
        /// </summary>
        /// <param name="nDevice">The device number. If the collection contains n devices, the devices are numbered 0 to n– 1.</param>
        /// <param name="pDevice">Pointer to a pointer variable into which the method writes the address of the IMMDevice interface of the specified item in the device collection. Through this method, the caller obtains a counted reference to the interface. The caller is responsible for releasing the interface, when it is no longer needed, by calling the interface's Release method. If the Item call fails, *ppDevice is NULL.</param>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/dd371398(v=vs.85).aspx </remarks>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        int Item(
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 nDevice,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IMMDevice pDevice);

    }
}
