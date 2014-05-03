using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dgMicMute.Enumerations
{
    /// <summary>
    /// No Flags Parameter needed here, because they shouldn't be combined.
    /// </summary>
    public enum EDeviceState : uint
    {
        DeviceStateActive       = 0x00000001,
        DeviceStateUnplugged    = 0x00000002,
        DeviceStateNotPresent   = 0x00000004,
        DeviceStatemaskAll      = 0x00000007
    }
}
