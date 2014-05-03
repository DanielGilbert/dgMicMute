using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace dgMicMute.Externals
{
    /// <summary>
    /// Specifies the FMTID/PID identifier that programmatically identifies a property.
    /// It's defined in the Windows Property System API.
    /// </summary>
    /// <remarks>
    /// Source: http://msdn.microsoft.com/en-us/library/bb773381.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct PropertyKey
    {
        /// <summary>
        /// A unique GUID for the property.
        /// </summary>
        public Guid fmtid;

        /// <summary>
        /// A property identifier (PID).
        /// </summary>
        public int pid;   
    }
}
