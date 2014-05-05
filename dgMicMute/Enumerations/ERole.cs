using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dgMicMute.Enumerations
{
    /// <summary>
    /// The ERole enumeration defines constants that indicate the role that the system has assigned to an audio endpoint device.
    /// </summary>
    /// <remarks>
    /// Source: http://msdn.microsoft.com/en-us/library/windows/desktop/dd370842(v=vs.85).aspx
    /// </remarks>
    public enum ERole
    {
        EConsole = 0,
        EMultimedia = 1,
        ECommunications = 2,
        ERoleEnumCount = 3
    }
}