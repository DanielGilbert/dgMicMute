using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using dgMicMute.Enumerations;

namespace dgMicMute.Interfaces
{
    /// <summary>
    /// The IMMEndpoint interface represents an audio endpoint device. A client obtains a reference to an IMMEndpoint interface instance by following these steps:
    /// -- By using one of the techniques described in IMMDevice Interface, obtain a reference to the IMMDevice interface of an audio endpoint device.
    /// -- Call the IMMDevice::QueryInterface method with parameter iid set to REFIID IID_IMMEndpoint.
    /// </summary>
    /// <remarks>Source: http://msdn.microsoft.com/en-us/library/dd371414(v=vs.85).aspx </remarks>
    public interface IMMEndpoint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataFlow"></param>
        /// <returns></returns>
        [PreserveSig]
        int GetDataFlow(
            [Out] [MarshalAs(UnmanagedType.Interface)] out EDataFlow dataFlow);
    }
}