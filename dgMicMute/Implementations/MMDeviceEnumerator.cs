using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using dgMicMute.Enumerations;
using dgMicMute.Interfaces;
using dgMicMute.Internals;

namespace dgMicMute.Implementations
{
    public class MMDeviceEnumerator
    {
        private IMMDeviceEnumerator _deviceEnumerator;

        public MMDeviceEnumerator()
        {
            _deviceEnumerator = new mmDeviceEnumerator() as IMMDeviceEnumerator;
        }

        public MMDeviceCollection EnumerateAudioEndpoints(EDataFlow dataFlow, EDeviceState deviceState)
        {
            IMMDeviceCollection result;
            Marshal.ThrowExceptionForHR(_deviceEnumerator.EnumAudioEndpoints(dataFlow, deviceState, out result));
            return new MMDeviceCollection(result);
        }

        public MMDevice GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role)
        {
            IMMDevice result;
            Marshal.ThrowExceptionForHR(_deviceEnumerator.GetDefaultAudioEndpoint(dataFlow, role, out result));
            return new MMDevice(result);
        }
    }
}
