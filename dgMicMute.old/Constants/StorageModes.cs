using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dgMicMute.Constants
{
    /// <summary>
    /// The StorageMode(STGM) constants are flags that indicate conditions for creating and deleting the object and access modes for the object.
    /// </summary>
    /// <remarks>
    /// Source: http://msdn.microsoft.com/en-us/library/windows/desktop/aa380337(v=vs.85).aspx
    /// </remarks>
    public class StorageModes
    {
        public const uint StgmRead = 0;
        public const uint StgmWrite = 1;
        public const uint StgmReadWrite = 2;
    }
}
