using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dgMicMute.Enumerations
{
    /// <summary>
    /// The EDataFlow enumeration defines constants that indicate the direction
    /// in which audio data flows between an audio endpoint device and an application.
    /// </summary>
    /// <remarks>
    /// Source: http://msdn.microsoft.com/en-us/library/windows/desktop/dd370828(v=vs.85).aspx
    /// </remarks>
    public enum EDataFlow
    {
        ERender = 0,
        ECapture = 1,
        EAll = 2 ,
        EDataFlowEnumCount =3
    }
}
