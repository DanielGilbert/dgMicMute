using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dgMicMute.Enumerations
{
    /// <summary>
    /// Values that are used in activation calls to indicate the execution contexts in which an object is to be run. 
    /// These values are also used in calls to CoRegisterClassObject to indicate the set of execution contexts 
    /// in which a class object is to be made available for requests to construct instances.
    /// </summary>
    /// <remarks>
    /// Source: http://msdn.microsoft.com/en-us/library/windows/desktop/ms693716(v=vs.85).aspx
    /// </remarks>
    [Flags]
    public enum ClsCtx
    {
        InProcServer        = 0x1,
        InProcHandler       = 0x2,
        LocalServer         = 0x4,
        InprocServer16      = 0x8,
        RemoteServer        = 0x10,
        InprocHandler16     = 0x20,
        Reserved1           = 0x40,
        Reserved2           = 0x80,
        Reserved3           = 0x100,
        Reserved4           = 0x200,
        NoCodeDownload      = 0x400,
        Reserved5           = 0x800,
        NoCustomMarshal     = 0x1000,
        EnableCodeDownload  = 0x2000,
        NoFailureLog        = 0x4000,
        DisableAaa          = 0x8000,
        EnableAaa           = 0x10000,
        FromDefaultContext  = 0x20000,
        InProc              = InProcServer | InProcHandler,
        Server              = InProcServer | LocalServer | RemoteServer,
        All                 = Server | InProcHandler
    }
}
