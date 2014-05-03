using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using dgMicMute.Constants;
using dgMicMute.Enumerations;
using dgMicMute.Externals;
using dgMicMute.Interfaces;

namespace dgMicMute.Implementations
{
    public class MMDevice
    {
        private IMMDevice _device;
        private AudioEndpointVolume _audioEndpointVolume;

        private static Guid IID_IAudioEndpointVolume = typeof (IAudioEndpointVolume).GUID;

        public AudioEndpointVolume AudioEndpointVolume
        {
            get
            {
                if (_audioEndpointVolume == null)
                    GetAudioEndpointVolume();

                return _audioEndpointVolume;
            }
        }

        public string ID
        {
            get
            {
                string result;
                Marshal.ThrowExceptionForHR(_device.GetId(out result));
                return result;
            }
        }

        public EDataFlow DataFlow
        {
            get
            {
                EDataFlow result;
                IMMEndpoint endpoint = _device as IMMEndpoint;
                endpoint.GetDataFlow(out result);
                return result;
            }
        }

        public EDeviceState DeviceState
        {
            get
            {
                EDeviceState result;
                Marshal.ThrowExceptionForHR(_device.GetState(out result));
                return result;
            }
        }

        public MMDevice(IMMDevice device)
        {
            _device = device;
        }

        private void GetAudioEndpointVolume()
        {
            object result;
            Marshal.ThrowExceptionForHR(_device.Activate(IID_IAudioEndpointVolume, ClsCtx.All, IntPtr.Zero, out result));
            _audioEndpointVolume = new AudioEndpointVolume(result as IAudioEndpointVolume);
        }
    }
}
